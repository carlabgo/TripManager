using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Domain.Models
{
    [Table("Vehicle")]
    public class Vehicle : BaseModel
    {
        [Required]
        [Column("LicensePlate")]
        public string LicensePlate { get; set; }
        [Required]
        [Column("Brand")]
        public string Brand { get; set; }
        [Required]
        [Column("TypeId")]
        public long TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        public VehicleType Type { get; set; }
    }
}
