namespace WEBSITESLK
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelImage : DbContext
    {
        public ModelImage()
            : base("name=ModelImage")
        {
        }

        public virtual DbSet<image> images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<image>()
                .Property(e => e.image1)
                .IsUnicode(false);
        }
    }
}
