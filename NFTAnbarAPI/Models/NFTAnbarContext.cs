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

        public virtual DbSet<Barname> Barname { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Contractor> Contractor { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Havaleh> Havaleh { get; set; }
        public virtual DbSet<Naftkesh> Naftkesh { get; set; }
        public virtual DbSet<Ndepo> Ndepo { get; set; }
        public virtual DbSet<NdepoType> NdepoType { get; set; }
        public virtual DbSet<NdepoWorkShift> NdepoWorkShift { get; set; }
        public virtual DbSet<Permit> Permit { get; set; }
        public virtual DbSet<PermitType> PermitType { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SendType> SendType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=NFTAnbar;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barname>(entity =>
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

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

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

            modelBuilder.Entity<Contractor>(entity =>
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

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
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

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Gcode).HasColumnName("GCode");

                entity.Property(e => e.Gkey).HasColumnName("GKey");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Havaleh>(entity =>
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

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");
            });

            modelBuilder.Entity<Naftkesh>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cdate)
                    .HasColumnName("CDate")
                    .HasColumnType("date");

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.CuserId).HasColumnName("CUserID");

                entity.Property(e => e.DaDate).HasColumnType("date");

                entity.Property(e => e.DaUserId).HasColumnName("DaUserID");

                entity.Property(e => e.Ddate)
                    .HasColumnName("DDate")
                    .HasColumnType("date");

                entity.Property(e => e.DriverName).HasMaxLength(50);

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PlateNumber).HasMaxLength(50);

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.Naftkesh)
                    .HasForeignKey(d => d.ContractorId)
                    .HasConstraintName("FK_Naftkesh_Contractor");
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

            modelBuilder.Entity<NdepoWorkShift>(entity =>
            {
                entity.ToTable("NDepoWorkShift");

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

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Permit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarnameId).HasColumnName("BarnameID");

                entity.Property(e => e.Cdate)
                    .HasColumnName("CDate")
                    .HasColumnType("date");

                entity.Property(e => e.CompanyStationId).HasColumnName("CompanyStationID");

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.CuserId).HasColumnName("CUserID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DaDate).HasColumnType("date");

                entity.Property(e => e.DaUserId).HasColumnName("DaUserID");

                entity.Property(e => e.Ddate)
                    .HasColumnName("DDate")
                    .HasColumnType("date");

                entity.Property(e => e.DirectForwardRequestId).HasColumnName("DirectForwardRequestID");

                entity.Property(e => e.DischargeTankId).HasColumnName("DischargeTankID");

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.HavalehId).HasColumnName("HavalehID");

                entity.Property(e => e.LoadingTankId).HasColumnName("LoadingTankID");

                entity.Property(e => e.LocalCustomerLogisticProgramId).HasColumnName("LocalCustomerLogisticProgramID");

                entity.Property(e => e.LocalCustomerQuotaId).HasColumnName("LocalCustomerQuotaID");

                entity.Property(e => e.LocalCustomerSellDraftId).HasColumnName("LocalCustomerSellDraftID");

                entity.Property(e => e.LogisticDetailId).HasColumnName("LogisticDetailID");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.NdepoWorkShiftId).HasColumnName("NDepoWorkShiftID");

                entity.Property(e => e.OrgLocationId).HasColumnName("OrgLocationID");

                entity.Property(e => e.Owid).HasColumnName("OWID");

                entity.Property(e => e.PermitTypeId).HasColumnName("PermitTypeID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SendTypeId).HasColumnName("SendTypeID");

                entity.Property(e => e.TransportNaftkeshId).HasColumnName("TransportNaftkeshID");

                entity.Property(e => e.UcdoneStatusId).HasColumnName("UCDoneStatusID");

                entity.HasOne(d => d.Barname)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.BarnameId)
                    .HasConstraintName("FK_Permit_Barname");

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.ContractorId)
                    .HasConstraintName("FK_Permit_Contractor");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Permit_Customer");

                entity.HasOne(d => d.Havaleh)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.HavalehId)
                    .HasConstraintName("FK_Permit_Havaleh");

                entity.HasOne(d => d.NdepoWorkShift)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.NdepoWorkShiftId)
                    .HasConstraintName("FK_Permit_NDepoWorkShift");

                entity.HasOne(d => d.OrgLocation)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.OrgLocationId)
                    .HasConstraintName("FK_Permit_City");

                entity.HasOne(d => d.PermitType)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.PermitTypeId)
                    .HasConstraintName("FK_Permit_PermitType");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Permit_Product");

                entity.HasOne(d => d.SendType)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.SendTypeId)
                    .HasConstraintName("FK_Permit_SendType");

                entity.HasOne(d => d.TransportNaftkesh)
                    .WithMany(p => p.Permit)
                    .HasForeignKey(d => d.TransportNaftkeshId)
                    .HasConstraintName("FK_Permit_Naftkesh");
            });

            modelBuilder.Entity<PermitType>(entity =>
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

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
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

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

                entity.Property(e => e.Mdate)
                    .HasColumnName("MDate")
                    .HasColumnType("date");

                entity.Property(e => e.MuserId).HasColumnName("MUserID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SendType>(entity =>
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

                entity.Property(e => e.DuserId).HasColumnName("DUserID");

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
