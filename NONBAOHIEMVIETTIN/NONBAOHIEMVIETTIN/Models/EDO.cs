namespace NONBAOHIEMVIETTIN.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EDO : DbContext
    {
        public EDO()
            : base("name=EDOentityes")
        {
        }

        public virtual DbSet<accounts> accounts { get; set; }
        public virtual DbSet<permission> permission { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accounts>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<accounts>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<accounts>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<accounts>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<accounts>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<permission>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<permission>()
                .HasMany(e => e.accounts)
                .WithOptional(e => e.permission)
                .HasForeignKey(e => e.idpermission);
        }
    }
}
