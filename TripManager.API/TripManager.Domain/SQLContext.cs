using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripManager.Cross.Enums;
using TripManager.Domain.Models;

namespace TripManager.Domain
{
    public class SQLContext : DbContext
    {
        public SQLContext()
        { 
        }
        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {
        }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            AddInitialDataSets(builder);
            
        }
        private static void AddInitialDataSets(ModelBuilder builder)
        {
            InitialVehicleTypes(builder);
        }

        public static void InitialVehicleTypes(ModelBuilder builder)
        {
            builder.Entity<VehicleType>().HasData(
                new VehicleType
                {
                    Id = (long)EnumTypes.Car,
                    Name = CETypes.Car
                },
                new VehicleType
                {
                    Id = (long)EnumTypes.Truck,
                    Name = CETypes.Truck
                },
                new VehicleType
                {
                    Id = (long)EnumTypes.Van,
                    Name = CETypes.Van
                },
                new VehicleType
                {
                    Id = (long)EnumTypes.Motorcycle,
                    Name = CETypes.Motorcycle
                },
                new VehicleType
                {
                    Id = (long)EnumTypes.Motorhome,
                    Name = CETypes.Motorhome
                }
                );
        }
    }
}
