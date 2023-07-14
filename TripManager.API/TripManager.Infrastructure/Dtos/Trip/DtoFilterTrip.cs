using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Infrastructure.Dtos.Trip
{
    public class DtoFilterTrip
    {
        public string Name { get; set; }

        public long CityId { get; set; }

        public long VehicleId { get; set; }

        public DateTime TripDate { get; set; }
        [Required]
        public int Page { get; set; }

        [Required]
        public int PageSize { get; set; }

    }
}
