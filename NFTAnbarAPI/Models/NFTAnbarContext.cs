using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NFTAnbarAPI.Models
{
    public partial class NFTAnbarContext : DbContext
    {
        public NFTAnbarContext()
        {
        }

        public NFTAnbarContext(DbContextOptions<NFTAnbarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Ndepo> Ndepo { get; set; }
        public virtual DbSet<NdepoType> NdepoType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=NFTAnbar;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cdate)
                    .HasColumnName("CDate")
                    .HasColumnType("date");

                entity.Property(e => e.CuserId).HasColumnName("CUserID");

                entity.Property(e => e.DaDate).HasColumnType("date");

                entity.Property(e => e.DaUserId).HasColumnName("DaUserID");

                entity.Property(e => e.Ddate)
                    .HasColumnName("DDate")
                    .HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Ndepo>(entity =>
            {
                entity.ToTable("NDepo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cdate)
                    .HasColumnName("CDate")
                    .HasColumnType("date");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CuserId).HasColumnName("CUserID");

                entity.Property(e => e.DaDate).HasColumnType("date");

                entity.Property(e => e.DaUserId).HasColumnName("DaUserID");

                entity.Property(e => e.Ddate)
                    .HasColumnName("DDate")
                    .HasColumnType("date");

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Gcode).HasColumnName("GCode");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NdepoTypeId).HasColumnName("NDepoTypeID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Ndepo)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_NDepo_City");

                entity.HasOne(d => d.NdepoType)
                    .WithMany(p => p.Ndepo)
                    .HasForeignKey(d => d.NdepoTypeId)
                    .HasConstraintName("FK_NDepo_NDepoType");
            });

            modelBuilder.Entity<NdepoType>(entity =>
            {
                entity.ToTable("NDepoType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cdate)
                    .HasColumnName("CDate")
                    .HasColumnType("date");

                entity.Property(e => e.CuserId).HasColumnName("CUserID");

                entity.Property(e => e.DaDate).HasColumnType("date");

                entity.Property(e => e.DaUserId).HasColumnName("DaUserID");

                entity.Property(e => e.Ddate)
                    .HasColumnName("DDate")
                    .HasColumnType("date");

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Gcode).HasColumnName("GCode");

                entity.Property(e => e.Gkey).HasColumnName("GKey");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
