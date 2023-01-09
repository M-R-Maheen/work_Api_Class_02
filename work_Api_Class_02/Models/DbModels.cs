using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace work_Api_Class_02.Models
{
    public class VehicleType
    {

        public VehicleType()
        {
            Vehicles = new List<Vehicle>();
        }
        public int VehicleTypeId { get; set; }
        [Required, StringLength(50)]

        public string TypeName { get; set; }
        [StringLength(50)]

        public string SuitableFor { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

    }
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [Required, StringLength(30)]

        public string Model { get; set; }
        [Required, StringLength(30)]

        public int Capacity { get; set; }
        [Required, ForeignKey("VehicleType")]


        public int VehicleTypeId { get; set; }
        // nev
        public string MainFeature { get; set; }

        public VehicleType VehicleType { get; set; }
    }

    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

    }
}
