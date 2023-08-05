﻿// <auto-generated />
using System;
using HumanResources.Homework.WpfApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HumanResources.Homework.WpfApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HumanResources.Homework.WpfApp.Models.Domains.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Comments")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("FireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Position")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("money");

                    b.Property<int>("WorkTimeId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("WorkTimeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HumanResources.Homework.WpfApp.Models.Domains.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"));

                    b.Property<string>("Exception")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Logged")
                        .HasColumnType("datetime2");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("HumanResources.Homework.WpfApp.Models.Domains.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Name = "admin",
                            Password = "v5yAvVi7npxHRy0dS22MSWzH2R8b+KYsklVTUuw5b0KZiajWhq2IFCDxmRRFPQwd"
                        },
                        new
                        {
                            UserId = 2,
                            Name = "user1",
                            Password = "UH+aIdqpFJthTzntI/4GAPF6VcJKgO/Rk/RLR/2qRgOYRyrKzaFFFWi8F0ByM1iL"
                        });
                });

            modelBuilder.Entity("HumanResources.Homework.WpfApp.Models.Domains.WorkTime", b =>
                {
                    b.Property<int>("WorkTimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkTimeId"));

                    b.Property<string>("WorkTimeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("WorkTimeId");

                    b.HasIndex("WorkTimeName")
                        .IsUnique();

                    b.ToTable("WorkTimes");

                    b.HasData(
                        new
                        {
                            WorkTimeId = 1,
                            WorkTimeName = "Full-Time"
                        },
                        new
                        {
                            WorkTimeId = 2,
                            WorkTimeName = "Part-Time"
                        },
                        new
                        {
                            WorkTimeId = 3,
                            WorkTimeName = "Freelance"
                        });
                });

            modelBuilder.Entity("HumanResources.Homework.WpfApp.Models.Domains.Employee", b =>
                {
                    b.HasOne("HumanResources.Homework.WpfApp.Models.Domains.WorkTime", "WorkTime")
                        .WithMany("Employees")
                        .HasForeignKey("WorkTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkTime");
                });

            modelBuilder.Entity("HumanResources.Homework.WpfApp.Models.Domains.WorkTime", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
