namespace WEBSITESLK.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Car : DbContext
    {
        public Car()
            : base("name=Car")
        {
        }

        public virtual DbSet<CarInfo1> CarInfo1 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarInfo1>()
                .Property(e => e.VIN)
                .IsUnicode(false);

            modelBuilder.Entity<CarInfo1>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<CarInfo1>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<CarInfo1>()
                .Property(e => e.StoreLoc)
                .IsUnicode(false);

            modelBuilder.Entity<CarInfo1>()
                .Property(e => e.Owner)
                .IsUnicode(false);
        }
    }
}
