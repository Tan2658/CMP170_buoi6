using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace buoi6.Models
{
    public partial class StaffContextDB : DbContext
    {
        public StaffContextDB()
            : base("name=StaffContextDB")
        {
        }

        public virtual DbSet<Nhanvien> Nhanvien { get; set; }
        public virtual DbSet<Phongban> Phongban { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.MaNV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.MaPB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Phongban>()
                .Property(e => e.MaPB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Phongban>()
                .HasMany(e => e.Nhanvien)
                .WithRequired(e => e.Phongban)
                .WillCascadeOnDelete(false);
        }
    }
}
