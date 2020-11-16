﻿// <auto-generated />
using System;
using Government_Helping_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Government_Helping_System.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201116171913_employeeidadded")]
    partial class employeeidadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Government_Helping_System.Models.Citizen", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date_of_Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Citizens");
                });

            modelBuilder.Entity("Government_Helping_System.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("post")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("Government_Helping_System.Models.PhotoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("querieId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("querieId");

                    b.ToTable("photoModels");
                });

            modelBuilder.Entity("Government_Helping_System.Models.Querie", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CitizenId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Main_Emp_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Query_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("textfilepath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zipcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("queries");
                });

            modelBuilder.Entity("Government_Helping_System.Models.PhotoModel", b =>
                {
                    b.HasOne("Government_Helping_System.Models.Querie", "querie")
                        .WithMany("ProofPhotos")
                        .HasForeignKey("querieId");

                    b.Navigation("querie");
                });

            modelBuilder.Entity("Government_Helping_System.Models.Querie", b =>
                {
                    b.HasOne("Government_Helping_System.Models.Citizen", "Citizen")
                        .WithMany("queries")
                        .HasForeignKey("CitizenId");

                    b.HasOne("Government_Helping_System.Models.Employee", "Employee")
                        .WithMany("queries")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Citizen");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Government_Helping_System.Models.Citizen", b =>
                {
                    b.Navigation("queries");
                });

            modelBuilder.Entity("Government_Helping_System.Models.Employee", b =>
                {
                    b.Navigation("queries");
                });

            modelBuilder.Entity("Government_Helping_System.Models.Querie", b =>
                {
                    b.Navigation("ProofPhotos");
                });
#pragma warning restore 612, 618
        }
    }
}
