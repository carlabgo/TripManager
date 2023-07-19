using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Infrastructure.Dtos.Vehicle
{
    public class DtoFilterVehicle
    {
        public string? LicensePlate { get; set; }
        public string? Brand { get; set; }
        public long? TypeId { get; set; }
        [Required]
        public int Page { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}
