﻿// <auto-generated />
using DataBase.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataBase.Model.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("DataBase.Model.EntertainmentCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("ElectroCharge")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("EntertainmentCenters");
                });

            modelBuilder.Entity("DataBase.Model.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("DataBase.Model.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntertainmentCenterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Floor")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntertainmentCenterId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("DataBase.Model.EntertainmentCenter", b =>
                {
                    b.HasOne("DataBase.Model.Owner", "Owner")
                        .WithMany("EntertainmentCenters")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DataBase.Model.Service", b =>
                {
                    b.HasOne("DataBase.Model.EntertainmentCenter", "EntertainmentCenter")
                        .WithMany("Services")
                        .HasForeignKey("EntertainmentCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntertainmentCenter");
                });

            modelBuilder.Entity("DataBase.Model.EntertainmentCenter", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("DataBase.Model.Owner", b =>
                {
                    b.Navigation("EntertainmentCenters");
                });
#pragma warning restore 612, 618
        }
    }
}
