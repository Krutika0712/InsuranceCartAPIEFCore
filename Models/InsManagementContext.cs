using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InsuranceCartAPIEFCore.Models
{
    public partial class InsManagementContext : DbContext
    {
        public InsManagementContext()
        {
        }

        public InsManagementContext(DbContextOptions<InsManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<InsPolicy> InsPolicies { get; set; } = null!;
        public virtual DbSet<Logintbl> Logintbls { get; set; } = null!;
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=BSPUNL-021546;Database=InsManagement; Trusted_Connection=True;encrypt=false");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Country)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InsPolicy>(entity =>
            {
                entity.HasKey(e => e.PolicyId)
                    .HasName("PK__InsPolic__2E1339A4E555B656");

                entity.ToTable("InsPolicy");

                entity.Property(e => e.Category)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.PolicyName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Logintbl>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Logintbl__AB6E6165853CB962");

                entity.ToTable("Logintbl");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pwd");
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.HasKey(e => e.ApplyId)
                    .HasName("PK__Transact__F0687FB1F90AF84F");

                entity.Property(e => e.AppliedDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TransactionDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Transacti__Custo__286302EC");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.TransactionDetails)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK__Transacti__Polic__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
