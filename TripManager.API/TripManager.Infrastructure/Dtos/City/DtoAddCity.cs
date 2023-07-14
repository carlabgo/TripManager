using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Infrastructure.Dtos
{
    public class DtoAddCity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
