using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FintexClient.Models
{
    public partial class CreditContext : DbContext
    {
        public CreditContext()
        {
        }

        public CreditContext(DbContextOptions<CreditContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Credit> Credits { get; set; }
        public virtual DbSet<CreditOffer> CreditOffers { get; set; }
        //public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SQL6006.site4now.net;Initial Catalog=DB_A46300_TestDB;User Id=DB_A46300_TestDB_admin;Password=qwerty99");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .HasColumnName("Account_number")
                    .HasMaxLength(250);

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fio)
                    .HasColumnName("FIO")
                    .HasMaxLength(100);

                entity.Property(e => e.IsBlocked).HasColumnName("Is_Blocked");

                entity.Property(e => e.Pass).HasMaxLength(20);
            });

            modelBuilder.Entity<Credit>(entity =>
            {
                entity.Property(e => e.Duration).HasColumnType("datetime");

                entity.Property(e => e.PercentValue).HasColumnName("Percent_Value");

                entity.Property(e => e.PeriodOfPayment)
                    .HasColumnName("Period_of_Payment")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Credit)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Credit_ToClient");
            });

            modelBuilder.Entity<CreditOffer>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Conditions).HasMaxLength(255);


            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });
        }
    }
 }


