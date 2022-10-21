﻿// <auto-generated />
using System;
using DigitalGoods.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DigitalGoods.Core.Entities.ActivationCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Activated")
                        .HasColumnType("bit");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("ActivationCodes");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BankAccountTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.BankAccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BankAccountTypes");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("BankAccountId")
                        .HasColumnType("int");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Price")
                        .HasColumnType("real");

                    b.Property<int?>("SourceId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("SourceId");

                    b.HasIndex("UserId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActivationCodeId")
                        .HasColumnType("int");

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivationCodeId");

                    b.HasIndex("BuyerId");

                    b.HasIndex("OfferId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("SourceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("SourceId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.ActivationCode", b =>
                {
                    b.HasOne("DigitalGoods.Core.Entities.Offer", "Offer")
                        .WithMany("ActivationCodes")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.BankAccount", b =>
                {
                    b.HasOne("DigitalGoods.Core.Entities.BankAccountType", "Type")
                        .WithMany()
                        .HasForeignKey("BankAccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DigitalGoods.Core.Entities.User", "User")
                        .WithMany("BankAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Media", b =>
                {
                    b.HasOne("DigitalGoods.Core.Entities.Offer", "Offer")
                        .WithMany("Medias")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Offer", b =>
                {
                    b.HasOne("DigitalGoods.Core.Entities.BankAccount", "BankAccount")
                        .WithMany()
                        .HasForeignKey("BankAccountId");

                    b.HasOne("DigitalGoods.Core.Entities.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId");

                    b.HasOne("DigitalGoods.Core.Entities.User", "User")
                        .WithMany("Offers")
                        .HasForeignKey("UserId");

                    b.Navigation("BankAccount");

                    b.Navigation("Source");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Sale", b =>
                {
                    b.HasOne("DigitalGoods.Core.Entities.ActivationCode", "ActivationCode")
                        .WithMany()
                        .HasForeignKey("ActivationCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DigitalGoods.Core.Entities.User", "Buyer")
                        .WithMany("Purchases")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DigitalGoods.Core.Entities.Offer", "Offer")
                        .WithMany("Sales")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ActivationCode");

                    b.Navigation("Buyer");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Source", b =>
                {
                    b.HasOne("DigitalGoods.Core.Entities.Source", "Parent")
                        .WithMany("Childs")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Tag", b =>
                {
                    b.HasOne("DigitalGoods.Core.Entities.Offer", null)
                        .WithMany("Tags")
                        .HasForeignKey("OfferId");

                    b.HasOne("DigitalGoods.Core.Entities.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Offer", b =>
                {
                    b.Navigation("ActivationCodes");

                    b.Navigation("Medias");

                    b.Navigation("Sales");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.Source", b =>
                {
                    b.Navigation("Childs");
                });

            modelBuilder.Entity("DigitalGoods.Core.Entities.User", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("Offers");

                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}