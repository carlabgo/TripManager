using AutoMapper;
using TripManager.Domain.Models;
using TripManager.Domain;
using TripManager.Infrastructure.Dtos;
using TripManager.Infrastructure.OperationResponse;
using Microsoft.EntityFrameworkCore;
using TripManager.Infrastructure.Dtos.Vehicle;
using TripManager.Infrastructure.Dtos.Trip;

namespace TripManager.Infrastructure.Services
{
    public class VehicleService : BaseService
    {
        public VehicleService(SQLContext contextSql, IMapper mapper) : base(mapper, contextSql)
        {

        }
        public async Task<OperationResponse<DtoGetVehicle>> GetById(long id)
        {
            var vehicle = await _contextSql
                .Vehicles
                .Include(x => x.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (vehicle == null)
            {
                return new OperationResponse<DtoGetVehicle>(null, false, "No se encuentra el vehiculo que buscas.");
            }

            var dto = new DtoGetVehicle()
            {
                Id = id,
                Brand = vehicle.Brand,
                LicensePlate= vehicle.LicensePlate,
                Type = vehicle.Type.Name
            };

            return new OperationResponse<DtoGetVehicle>(dto);
        }

        public async Task<OperationResponse<long>> CreateOrUpdateVehicle(DtoAddVehicle request)
        {
            if (request.Id == 0)
            {
                var vehicle = await _contextSql
                    .Vehicles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.LicensePlate == request.LicensePlate)
                    .ConfigureAwait(false);

                if (vehicle != null)
                {
                    return new OperationResponse<long>(0, false, "Ya existe un vehiculo con esa patente.");
                }
                var newVehicle = new Vehicle()
                {
                  Active= true,
                  Brand = request.Brand,
                  LicensePlate= request.LicensePlate,
                  TypeId = request.TypeId
                };

                await _contextSql.Vehicles.AddAsync(newVehicle);
                await _contextSql.SaveChangesAsync();

                return new OperationResponse<long>(newVehicle.Id);

            }
            else
            {
                var vehicle = await _contextSql
                    .Vehicles
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (vehicle == null)
                {
                    return new OperationResponse<long>(0, false, "No se encuentra el vehiculo que buscas.");
                }

                vehicle.LicensePlate = request.LicensePlate;
                vehicle.Brand = request.Brand;
                vehicle.TypeId = request.TypeId;

                _contextSql.Vehicles.Update(vehicle);
                await _contextSql.SaveChangesAsync();

                return new OperationResponse<long>(vehicle.Id);
            }

        }

        public async Task<OperationResponse<DtoResponseData<List<DtoGetVehicle>>>> ListAll()
        {
            var list = (await _contextSql
                .Vehicles
                .Include(x => x.Type)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(v => new DtoGetVehicle()
                {
                    Id = v.Id,
                    Brand= v.Brand,
                    Type = v.Type.Name,
                    LicensePlate= v.LicensePlate,
                }).ToList();

            return new OperationResponse<DtoResponseData<List<DtoGetVehicle>>>(new DtoResponseData<List<DtoGetVehicle>>() { Data = list });
        }

        public async Task<OperationResponse<bool>> Delete(long id)
        {
            var vehicle = await _contextSql.Vehicles.FirstOrDefaultAsync(x => x.Id == id && x.Active == true);

            if (vehicle == null)
            {
                return new OperationResponse<bool>(false, false, "No se encuentra el vehiculo que intentas eliminar.");
            }

            vehicle.Active = false;
            await _contextSql.SaveChangesAsync();
            return new OperationResponse<bool>(true);
        }
        public async Task<OperationResponse<DtoResponsePagination<DtoGetVehicle>>> ListVehicles(DtoFilterVehicle filter)
        {
            var query = _contextSql
                .Vehicles
                .Include(x => x.Type)
                .AsNoTracking()
                .Where(p =>
                (p.LicensePlate.ToLower().Contains(filter.LicensePlate ?? "")) &&
                (p.Brand.ToLower().Contains(filter.Brand ?? "")) &&
             ((filter.TypeId == null || filter.TypeId == 0) ? true : p.TypeId == filter.TypeId) && p.Active == true);

            var count = await query.CountAsync().ConfigureAwait(false);

            var list = (await query.OrderBy(p => p.LicensePlate)
                .Skip(filter.Page * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(d => new DtoGetVehicle()
                {
                   LicensePlate= d.LicensePlate,
                   Brand= d.Brand,
                   Type = d.Type.Name,
                   Id = d.Id,
                });

            return new OperationResponse<DtoResponsePagination<DtoGetVehicle>>(new DtoResponsePagination<DtoGetVehicle>()
            {
                Data = list,
                TotalCount = count,
                PageSize = filter.PageSize,
            });
        }
    }
}
