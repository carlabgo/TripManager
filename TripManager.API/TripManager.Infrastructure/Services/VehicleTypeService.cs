using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripManager.Domain;
using TripManager.Infrastructure.Dtos.Vehicle;
using TripManager.Infrastructure.Dtos;
using TripManager.Infrastructure.OperationResponse;

namespace TripManager.Infrastructure.Services
{
    public class VehicleTypeService : BaseService
    {
        public VehicleTypeService(SQLContext contextSql, IMapper mapper) : base(mapper, contextSql)
        {
            
        }
        public async Task<OperationResponse<DtoResponseData<List<DtoVehicleType>>>> ListAll()
        {
            var list = (await _contextSql
                .VehicleTypes
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(v => new DtoVehicleType()
                {
                    Id = v.Id,
                    Name = v.Name,

                }).ToList();

            return new OperationResponse<DtoResponseData<List<DtoVehicleType>>>(new DtoResponseData<List<DtoVehicleType>>() { Data = list });
        }


    }
}
