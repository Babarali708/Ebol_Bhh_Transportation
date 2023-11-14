﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Transportation.Models;

#nullable disable

namespace Transportation.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Transportation.Models.BillToRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AgreedValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ChargePerAnimal")
                        .HasColumnType("int");

                    b.Property<int?>("ChargeValue")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IsActive")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransporterRecordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BillToRecord");
                });

            modelBuilder.Entity("Transportation.Models.ReceiviedOrderRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BasicCharges")
                        .HasColumnType("int");

                    b.Property<string>("By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarrierPerAgentOrDriver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ChargesForExcessValue")
                        .HasColumnType("int");

                    b.Property<string>("Check")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IsActive")
                        .HasColumnType("int");

                    b.Property<int?>("Layovers")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Other")
                        .HasColumnType("int");

                    b.Property<string>("Payment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceivedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceivingDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TotalCharges")
                        .HasColumnType("int");

                    b.Property<int?>("TransporterRecordId")
                        .HasColumnType("int");

                    b.Property<int?>("VetFees")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ReceiviedOrderRecord");
                });

            modelBuilder.Entity("Transportation.Models.TransporterRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AttendentId")
                        .HasColumnType("int");

                    b.Property<string>("BarnFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BarnTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromCityAndState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IsActive")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("No")
                        .HasColumnType("int");

                    b.Property<string>("PhoneFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToCityAndState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TractorDriver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TractorNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrailerDriver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrailerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransporterRecord");
                });

            modelBuilder.Entity("Transportation.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(355)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Contact = "00000000000",
                            CreatedAt = new DateTime(2023, 9, 27, 17, 22, 44, 717, DateTimeKind.Local).AddTicks(752),
                            Email = "superadmin@yopmail.com",
                            FirstName = "Super",
                            IsActive = 1,
                            LastName = "Admin",
                            Password = "123",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            Contact = "00000000000",
                            CreatedAt = new DateTime(2023, 9, 27, 17, 22, 44, 717, DateTimeKind.Local).AddTicks(765),
                            Email = "admin@yopmail.com",
                            FirstName = "Admin",
                            IsActive = 1,
                            LastName = "Admin",
                            Password = "123",
                            Role = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}