﻿using AutoMapper;
using TripManager.Domain.Models;
using TripManager.Domain;
using TripManager.Infrastructure.OperationResponse;
using TripManager.Infrastructure.Dtos.Trip;
using Microsoft.EntityFrameworkCore;
using TripManager.Infrastructure.Dtos;
using Azure;
using Newtonsoft.Json;

namespace TripManager.Infrastructure.Services
{
    public class TripService : BaseService
    {
        public TripService(SQLContext contextSql, IMapper mapper) : base(mapper, contextSql)
        {

        }
        public async Task<OperationResponse<DtoGetTrip>> GetById(long id)
        {
            var trip = await _contextSql
                .Trips
                .Include(x => x.City)
                .Include(x => x.Vehicle)
                .Include(x => x.Vehicle.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (trip == null)
            {
                return new OperationResponse<DtoGetTrip>(null, false, "No se encuentra el viaje que buscas.");
            }

            var dto = new DtoGetTrip()
            {
               City = trip.City.Name,
               Description= trip.Description,
               Name= trip.Name,
               TripDate= trip.TripDate,
               Vehicle = trip.Vehicle.Type.Name
            };

            return new OperationResponse<DtoGetTrip>(dto);
        }
        public async Task<OperationResponse<long>> CreateOrUpdateTrip(DtoAddTrip request)
        {
            if (request.Id == 0)
            {
                if (request.TripDate > DateTime.Now.AddDays(10))
                {
                    return new OperationResponse<long>(0, false, "La fecha del viaje debe estar dentro de los próximos 10 días.");
                }
                var trip = await _contextSql
                    .Trips
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Name == request.Name)
                    .ConfigureAwait(false);

                if (trip != null)
                {
                    return new OperationResponse<long>(0, false, "Ya existe una viaje con ese nombre");
                }
                var city = _contextSql.Cities.FirstOrDefault(x => x.Id == request.CityId);

                if (city == null) 
                {
                    return new OperationResponse<long>(0, false, "La ciudad no es valida.");
                }
                var weatherForecast = await GetWeatherForecast(request.TripDate, city.Name);

                if (weatherForecast?.Rainy ?? false)
                {
                    return new OperationResponse<long>(0, false, "El pronóstico indica lluvia. Por favor, reprograma o cancela el viaje.");
                }
                var newTrip = new Trip()
                {
                    Name = request.Name,
                    Description= request.Description,
                    TripDate= request.TripDate,
                    VehicleId= request.VehicleId,
                    CityId= request.CityId,
                    IsRainy= request.IsRainy,
                    Active = true
                };

                await _contextSql.Trips.AddAsync(newTrip);
                await _contextSql.SaveChangesAsync();

                return new OperationResponse<long>(newTrip.Id);

            }
            else
            {
                var trip = await _contextSql
                    .Trips
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (trip == null)
                {
                    return new OperationResponse<long>(0, false, "No se encuentra el viaje que buscas.");
                }

                if (request.NewTripDate.HasValue)
                {
                    var city = _contextSql.Cities.FirstOrDefault(x => x.Id == request.CityId);

                    if (city == null)
                    {
                        return new OperationResponse<long>(0, false, "La ciudad no es valida.");
                    }

                    var weatherForecast = await GetWeatherForecast(request.NewTripDate.Value, city.Name);

                    if (weatherForecast?.Rainy ?? false)
                    {
                        return new OperationResponse<long>(0, false, "El pronóstico para la nueva fecha indica lluvia. Por favor, ingresa otra fecha o cancela el viaje.");
                    }

                    trip.TripDate = request.NewTripDate.Value;
                }

                trip.Name = request.Name;
                trip.TripDate = request.TripDate;
                trip.VehicleId = request.VehicleId;
                trip.CityId = request.CityId;
                trip.IsRainy = request.IsRainy;
                trip.Description= request.Description;

                _contextSql.Trips.Update(trip);
                await _contextSql.SaveChangesAsync();

                return new OperationResponse<long>(trip.Id);
            }

        }
        #region Conexion con api del clima
        private async Task<WeatherForecast> GetWeatherForecast(DateTime date, string city)
        {
            string apiKey = "bf95850c68ea06df9ece21d2ceed495c"; 
            string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?appid={apiKey}";

            // Realizar la solicitud a la API de OpenWeatherMap
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{apiUrl}&q={city}&units=metric");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);

                    // Obtener el pronóstico para la fecha específica
                    var forecast = weatherData?.Forecasts?.FirstOrDefault(f => f.Date.Date == date.Date);
                    return forecast;
                }
            }

            return null;
        }
        public class WeatherData
        {
            [JsonProperty("list")]
            public List<WeatherForecast> Forecasts { get; set; }
        }

        public class WeatherForecast
        {
            [JsonProperty("dt_txt")]
            public DateTime Date { get; set; }

            [JsonProperty("weather")]
            public List<WeatherDescription> WeatherDescriptions { get; set; }

            public bool Rainy => WeatherDescriptions?.Any(w => w.Main.ToLower() == "rain") ?? false;
        }

        public class WeatherDescription
        {
            [JsonProperty("main")]
            public string Main { get; set; }
        }
        #endregion

        public async Task<OperationResponse<DtoResponsePagination<DtoListTrip>>> ListTrips(DtoFilterTrip filter)
        {
            var query = _contextSql
                .Trips
                .Include(x => x.Vehicle)
                .Include(x => x.Vehicle.Type)
                .Include(x => x.City)
                .AsNoTracking()
                .Where(p => 
                ((filter.VehicleId == null || filter.VehicleId == 0) ? true : p.VehicleId == filter.VehicleId) &&
                ((filter.CityId == null || filter.CityId == 0) ? true : p.CityId == filter.CityId) &&
                ((filter.TripDate == null) ? true : p.TripDate == filter.TripDate));

            var count = await query.CountAsync().ConfigureAwait(false);

            var list = (await query.OrderBy(p => p.TripDate)
                .Skip(filter.Page * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(d => new DtoListTrip() 
                { 
                    TripDate = d.TripDate,
                    Id= d.Id,
                    Name= d.Name,
                    Description= d.Description,
                    City = d.City.Name,
                    Vehicle = d.Vehicle.Type.Name
                });

            return new OperationResponse<DtoResponsePagination<DtoListTrip>>(new DtoResponsePagination<DtoListTrip>()
            {
                Data = list,
                TotalCount = count,
                PageSize = filter.PageSize,
            });
        }
        public async Task<OperationResponse<bool>> Delete(long id)
        {
            var trip = await _contextSql.Trips.FirstOrDefaultAsync(x => x.Id == id && x.Active == true);

            if (trip == null)
            {
                return new OperationResponse<bool>(false, false, "No se encuentra el viaje que intentas eliminar.");
            }

            trip.Active = false;

            return new OperationResponse<bool>(true);
        }
    }
}
