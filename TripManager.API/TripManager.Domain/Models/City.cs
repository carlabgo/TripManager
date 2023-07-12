using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Domain.Models
{
    [Table("City")]
    public class  City : BaseModel
    {
        [Required]
        [Column("Name")]
        public string Name { get; set; }
        [Required]
        [Column("Country")]
        public string Country { get; set; }

    }
}
