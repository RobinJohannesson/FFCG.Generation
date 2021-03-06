﻿// <auto-generated />
using FFCG.Weather.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FFCG.Weather.API.Migrations
{
    [DbContext(typeof(WeatherContext))]
    [Migration("20180306183413_temperature_reading")]
    partial class temperature_reading
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FFCG.Weather.API.Import.Controllers.TemperatureReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("StationId");

                    b.Property<double>("Temperature");

                    b.HasKey("Id");

                    b.HasIndex("StationId");

                    b.ToTable("TemperatureReadings");
                });

            modelBuilder.Entity("FFCG.Weather.Models.WeatherStation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Altitude");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("FFCG.Weather.API.Import.Controllers.TemperatureReading", b =>
                {
                    b.HasOne("FFCG.Weather.Models.WeatherStation", "Station")
                        .WithMany()
                        .HasForeignKey("StationId");
                });
#pragma warning restore 612, 618
        }
    }
}
