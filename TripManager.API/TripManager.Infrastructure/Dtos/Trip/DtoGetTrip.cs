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
    public class DtoGetTrip
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Vehicle { get; set; }

        public DateTime TripDate { get; set; }
    }
}
