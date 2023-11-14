﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Transportation.Models;

#nullable disable

namespace Transportation.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230922065605_initial_03")]
    partial class initial_03
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("ChargePerAnimal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChargeValue")
                        .HasColumnType("nvarchar(max)");

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
                            CreatedAt = new DateTime(2023, 9, 22, 11, 56, 5, 120, DateTimeKind.Local).AddTicks(9039),
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
                            CreatedAt = new DateTime(2023, 9, 22, 11, 56, 5, 120, DateTimeKind.Local).AddTicks(9056),
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
