using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace chosen.Models
{
    public partial class FinalProjectContext : DbContext
    {
        public FinalProjectContext()
        {
        }

        public FinalProjectContext(DbContextOptions<FinalProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BreakingNews> BreakingNews { get; set; } = null!;
        public virtual DbSet<Carousel> Carousels { get; set; } = null!;
        public virtual DbSet<Commodity> Commodities { get; set; } = null!;
        public virtual DbSet<Customerservice> Customerservices { get; set; } = null!;
        public virtual DbSet<DrawRecord> DrawRecords { get; set; } = null!;
        public virtual DbSet<Factory> Factories { get; set; } = null!;
        public virtual DbSet<ImageStore> ImageStores { get; set; } = null!;
        public virtual DbSet<LineBot> LineBots { get; set; } = null!;
        public virtual DbSet<MemberInfo> MemberInfos { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<QuestionHistory> QuestionHistories { get; set; } = null!;
        public virtual DbSet<QuestionReport> QuestionReports { get; set; } = null!;
        public virtual DbSet<RawardItem> RawardItems { get; set; } = null!;
        public virtual DbSet<RawardLib> RawardLibs { get; set; } = null!;
        public virtual DbSet<ShowRaward> ShowRawards { get; set; } = null!;
        public virtual DbSet<ShowRawardItem> ShowRawardItems { get; set; } = null!;
        public virtual DbSet<TempStorage> TempStorages { get; set; } = null!;
        public virtual DbSet<TradeHistory> TradeHistories { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("yoyoFinalProject"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BreakingNews>(entity =>
            {
                entity.Property(e => e.BreakingNewsId).HasColumnName("BreakingNewsID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.IndexDescription).HasColumnType("ntext");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Carousel>(entity =>
            {
                entity.ToTable("Carousel");

                entity.Property(e => e.CarouselId).HasColumnName("CarouselID");

                entity.Property(e => e.PictureName).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(50);
            });

            modelBuilder.Entity<Commodity>(entity =>
            {
                entity.ToTable("Commodity");

                entity.Property(e => e.CommodityId).HasColumnName("CommodityID");

                entity.Property(e => e.CommodityName).HasMaxLength(50);

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.TempStorageId).HasColumnName("TempStorageID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Commodities)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commodity_MemberInfo");

                entity.HasOne(d => d.TempStorage)
                    .WithMany(p => p.Commodities)
                    .HasForeignKey(d => d.TempStorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commodity_TempStorage");
            });

            modelBuilder.Entity<Customerservice>(entity =>
            {
                entity.ToTable("Customerservice");

                entity.Property(e => e.CustomerserviceId).HasColumnName("CustomerserviceID");

                entity.Property(e => e.Class).HasMaxLength(50);

                entity.Property(e => e.QuestionTitle).HasMaxLength(50);
            });

            modelBuilder.Entity<DrawRecord>(entity =>
            {
                entity.HasKey(e => e.DrawId);

                entity.ToTable("Draw_Record");

                entity.Property(e => e.DrawId).HasColumnName("Draw_id");

                entity.Property(e => e.DrawTime).HasColumnType("datetime");

                entity.Property(e => e.FactoryId).HasColumnName("Factory_id");

                entity.Property(e => e.MemberId).HasColumnName("Member_id");

                entity.Property(e => e.SettlementTime).HasColumnType("datetime");

                entity.Property(e => e.ShowRawardId).HasColumnName("ShowRaward_id");

                entity.HasOne(d => d.Factory)
                    .WithMany(p => p.DrawRecords)
                    .HasForeignKey(d => d.FactoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Draw_Record_Factory");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.DrawRecords)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Draw_Record_MemberInfo");

                entity.HasOne(d => d.ShowRaward)
                    .WithMany(p => p.DrawRecords)
                    .HasForeignKey(d => d.ShowRawardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Draw_Record_ShowRaward");
            });

            modelBuilder.Entity<Factory>(entity =>
            {
                entity.ToTable("Factory");

                entity.Property(e => e.FactoryId).HasColumnName("Factory_id");

                entity.Property(e => e.ContactPerson).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<ImageStore>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK_Image_Store_1");

                entity.ToTable("Image_Store");

                entity.Property(e => e.ImageId).HasColumnName("Image_id");

                entity.Property(e => e.ImageName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Series).HasMaxLength(50);
            });

            modelBuilder.Entity<LineBot>(entity =>
            {
                entity.ToTable("LineBot");

                entity.Property(e => e.LineBotId).HasColumnName("LineBot_id");

                entity.Property(e => e.MessageType).HasMaxLength(50);

                entity.Property(e => e.SendTime).HasColumnType("datetime");
            });

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

            modelBuilder.Entity<QuestionHistory>(entity =>
            {
                entity.ToTable("QuestionHistory");

                entity.Property(e => e.QuestionHistoryId).HasColumnName("QuestionHistoryID");

                entity.Property(e => e.DateTimeSecond).HasColumnType("datetime");

                entity.Property(e => e.QuestionReportId).HasColumnName("QuestionReportID");

                entity.HasOne(d => d.QuestionReport)
                    .WithMany(p => p.QuestionHistories)
                    .HasForeignKey(d => d.QuestionReportId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_QuestionHistory_QuestionReport");
            });

            modelBuilder.Entity<QuestionReport>(entity =>
            {
                entity.ToTable("QuestionReport");

                entity.Property(e => e.QuestionReportId).HasColumnName("QuestionReportID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.QuestionTitle).HasMaxLength(50);

                entity.Property(e => e.QuestionType).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);
            });

            modelBuilder.Entity<RawardItem>(entity =>
            {
                entity.ToTable("Raward_items");

                entity.Property(e => e.RawardItemId).HasColumnName("Raward_item_id");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RawardId).HasColumnName("Raward_id");

                entity.Property(e => e.RawardLevel)
                    .HasMaxLength(50)
                    .HasColumnName("Raward_level");

                entity.HasOne(d => d.Raward)
                    .WithMany(p => p.RawardItems)
                    .HasForeignKey(d => d.RawardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raward_items_Raward_items");
            });

            modelBuilder.Entity<RawardLib>(entity =>
            {
                entity.HasKey(e => e.RawardId);

                entity.ToTable("Raward_lib");

                entity.Property(e => e.RawardId).HasColumnName("Raward_id");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Series).HasMaxLength(50);
            });

            modelBuilder.Entity<ShowRaward>(entity =>
            {
                entity.ToTable("ShowRaward");

                entity.Property(e => e.ShowRawardId).HasColumnName("ShowRaward_id");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FactoryId).HasColumnName("Factory_id");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Introduction).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Series).HasMaxLength(50);

                entity.HasOne(d => d.Factory)
                    .WithMany(p => p.ShowRawards)
                    .HasForeignKey(d => d.FactoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShowRaward_Factory");
            });

            modelBuilder.Entity<ShowRawardItem>(entity =>
            {
                entity.ToTable("ShowRaward_items");

                entity.Property(e => e.ShowRawardItemId).HasColumnName("ShowRaward_item_id");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RawardLevel)
                    .HasMaxLength(50)
                    .HasColumnName("Raward_level");

                entity.Property(e => e.ShowRawardId).HasColumnName("ShowRaward_id");

                entity.HasOne(d => d.ShowRaward)
                    .WithMany(p => p.ShowRawardItems)
                    .HasForeignKey(d => d.ShowRawardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShowRaward_items_ShowRaward");
            });

            modelBuilder.Entity<TempStorage>(entity =>
            {
                entity.ToTable("TempStorage");

                entity.Property(e => e.TempStorageId).HasColumnName("TempStorageID");

                entity.Property(e => e.AwardDate).HasColumnType("datetime");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.PrizeId).HasColumnName("Prize_ID");

                entity.Property(e => e.PrizeName).HasMaxLength(50);

                entity.Property(e => e.PrizePicture).HasMaxLength(50);

                entity.Property(e => e.PrizePoolId).HasColumnName("PrizePool_ID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.TempStorages)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TempStorage_MemberInfo");

                entity.HasOne(d => d.Prize)
                    .WithMany(p => p.TempStorages)
                    .HasForeignKey(d => d.PrizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TempStorage_ShowRaward_items");

                entity.HasOne(d => d.PrizePool)
                    .WithMany(p => p.TempStorages)
                    .HasForeignKey(d => d.PrizePoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TempStorage_ShowRaward");
            });

            modelBuilder.Entity<TradeHistory>(entity =>
            {
                entity.ToTable("TradeHistory");

                entity.Property(e => e.TradeTime).HasColumnType("datetime");

                entity.HasOne(d => d.Commodity)
                    .WithMany(p => p.TradeHistories)
                    .HasForeignKey(d => d.CommodityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TradeHistory_Commodity");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("wishlist");

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.PraductId).HasColumnName("PraductID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_wishlist_MemberInfo");

                entity.HasOne(d => d.Praduct)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.PraductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_wishlist_ShowRaward");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
