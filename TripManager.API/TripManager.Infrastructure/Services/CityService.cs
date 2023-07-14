﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TripManager.Domain;
using TripManager.Domain.Models;
using TripManager.Infrastructure.Dtos;
using TripManager.Infrastructure.OperationResponse;

namespace TripManager.Infrastructure.Services
{
    public class CityService : BaseService
    {
        public CityService(SQLContext contextSql, IMapper mapper): base(mapper, contextSql)
        {

        }
        public async Task<OperationResponse<DtoGetCity>> GetById(long id)
        {
            var city = await _contextSql
                .Cities
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (city == null)
            {
                return new OperationResponse<DtoGetCity>(null, false, "No se encuentra la ciudad que buscas.");
            }

            var dto = new DtoGetCity()
            {
                Id = city.Id,
                Country = city.Country,
                Name = city.Name,
            };

            return new OperationResponse<DtoGetCity>(dto);
        }
        
        public async Task<OperationResponse<long>> CreateOrUpdateCity(DtoAddCity request)
        {   
            if (request.Id == 0)
            {
                var city = await _contextSql
                    .Cities
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Name == request.Name && x.Country == request.Country)
                    .ConfigureAwait(false);

                if (city != null)
                {
                    return new OperationResponse<long>(0, false, "Ya existe una ciudad con ese nombre y ese pais.");
                }
                var newCity = new City()
                { 
                    Country = request.Country,
                    Name = request.Name,
                };

                await _contextSql .Cities.AddAsync(newCity);
                await _contextSql.SaveChangesAsync();

                return new OperationResponse<long>(newCity.Id);

            } else
            {
                var city = await _contextSql
                    .Cities
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (city == null) 
                {
                    return new OperationResponse<long>(0, false, "No se encuentra la ciudad que buscas.");
                }

                city.Name = request.Name;
                city.Country= request.Country;
                _contextSql.Cities.Update(city);
                await _contextSql.SaveChangesAsync();

                return new OperationResponse<long>(city.Id);
            }
            
        }

        public async Task<OperationResponse<DtoResponseData<List<DtoGetCity>>>> ListAll()
        {
            var list = (await _contextSql
                .Cities
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(c => new DtoGetCity()
                {
                    Country = c.Country,
                    Name = c.Name,
                    Id = c.Id
                }).ToList();

            return new OperationResponse<DtoResponseData<List<DtoGetCity>>>(new DtoResponseData<List<DtoGetCity>>() { Data = list });
        }
        public async Task<OperationResponse<bool>> Delete(long id)
        {
            var city = await _contextSql.Cities.FirstOrDefaultAsync(x => x.Id == id && x.Active == true);

            if (city == null)
            {
                return new OperationResponse<bool>(false, false, "No se encuentra la ciudad que intentas eliminar.");
            }

            city.Active = false;

            return new OperationResponse<bool>(true);
        }
    }
}
