using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripManager.Domain.Models;

namespace TripManager.Infrastructure.Dtos.Trip
{
    public class DtoAddTrip
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public long CityId { get; set; }

        public long VehicleId { get; set; }

        public DateTime TripDate { get; set; }

    }
}
