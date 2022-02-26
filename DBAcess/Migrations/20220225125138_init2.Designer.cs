﻿// <auto-generated />
using System;
using DBAcess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DBAcess.Migrations
{
    [DbContext(typeof(ShopDb))]
    [Migration("20220225125138_init2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DBAcess.Entityes.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HeadID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeadID")
                        .IsUnique()
                        .HasFilter("[HeadID] IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("DBAcess.Entityes.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DBAcess.Entityes.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Product")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DBAcess.Entityes.Department", b =>
                {
                    b.HasOne("DBAcess.Entityes.Employee", "Head")
                        .WithOne("HeadedDepartment")
                        .HasForeignKey("DBAcess.Entityes.Department", "HeadID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Head");
                });

            modelBuilder.Entity("DBAcess.Entityes.Employee", b =>
                {
                    b.HasOne("DBAcess.Entityes.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DBAcess.Entityes.Order", b =>
                {
                    b.HasOne("DBAcess.Entityes.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DBAcess.Entityes.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("DBAcess.Entityes.Employee", b =>
                {
                    b.Navigation("HeadedDepartment");
                });
#pragma warning restore 612, 618
        }
    }
}
