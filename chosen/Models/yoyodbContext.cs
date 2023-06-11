using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace chosen.Models
{
    public partial class yoyodbContext : DbContext
    {
        public yoyodbContext()
        {
        }

        public yoyodbContext(DbContextOptions<yoyodbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MemberInfo> MemberInfos { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=yoyodb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberInfo>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("MemberInfo");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EncryptedResetCode)
                    .HasMaxLength(200)
                    .HasColumnName("encryptedResetCode");

                entity.Property(e => e.MemberName).HasMaxLength(50);

                entity.Property(e => e.NewPassword)
                    .HasMaxLength(50)
                    .HasColumnName("newPassword");

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.RegisterDate).HasColumnType("date");

                entity.Property(e => e.ResetDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.MerchantOrderNo);

                entity.ToTable("Payment");

                entity.Property(e => e.MerchantOrderNo).HasMaxLength(50);

                entity.Property(e => e.Amt).HasMaxLength(50);

                entity.Property(e => e.BankCode).HasMaxLength(50);

                entity.Property(e => e.Card4No).HasMaxLength(50);

                entity.Property(e => e.Card6No).HasMaxLength(50);

                entity.Property(e => e.CodeNo).HasMaxLength(50);

                entity.Property(e => e.Exp).HasMaxLength(50);

                entity.Property(e => e.ExpireDate).HasMaxLength(50);

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP");

                entity.Property(e => e.ItemDesc).HasMaxLength(200);

                entity.Property(e => e.PayTime).HasMaxLength(50);

                entity.Property(e => e.PaymentType).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasComment("foreign key from registerinfo");

                entity.Property(e => e.TradeNo).HasMaxLength(50);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Payment_MemberId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
