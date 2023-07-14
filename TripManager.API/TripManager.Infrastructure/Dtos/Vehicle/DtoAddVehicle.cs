using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Infrastructure.Dtos.Vehicle
{
    public class DtoAddVehicle
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public long TypeId { get; set; }
    }
}
