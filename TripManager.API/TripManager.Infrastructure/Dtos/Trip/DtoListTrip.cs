using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Infrastructure.Dtos.Trip
{
    public class DtoListTrip
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Vehicle { get; set; }

        public DateTime TripDate { get; set; }

    }
}
