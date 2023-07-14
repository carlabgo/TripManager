using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Infrastructure.Dtos.Vehicle
{
    public class DtoGetVehicle
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
