using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Domain.Models
{
    [Table("Trip")]
    public class Trip : BaseModel
    {
        [Required]
        [Column("Name")]
        public string Name { get; set; }
        [Required]
        [Column("Description")]
        public string Description { get; set; }
        [Required]
        [Column("CityId")]
        public long CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }
        [Required]
        [Column("VehicleId")]
        public long VehicleId { get; set; }
        [ForeignKey(nameof(VehicleId))]
        public Vehicle Vehicle { get; set; }
        [Required]
        [Column("TripDate")]
        public DateTime TripDate { get; set; }
        [Required]
        [Column("IsRainy")]
        public bool IsRainy { get; set; }
         
    }
}
