using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Infrastructure.Dtos.City
{
    public class DtoFilterCity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        [Required]
        public int Page { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}
