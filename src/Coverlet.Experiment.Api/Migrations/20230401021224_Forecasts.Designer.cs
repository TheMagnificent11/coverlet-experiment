﻿// <auto-generated />
using System;
using Coverlet.Experiment.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coverlet.Experiment.Api.Migrations
{
    [DbContext(typeof(WeatherDbContext))]
    [Migration("20230401021224_Forecasts")]
    partial class Forecasts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Coverlet.Experiment.Domain.WeatherForecast", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("INTEGER");

                    b.HasKey("Date");

                    b.ToTable("Forecasts");
                });
#pragma warning restore 612, 618
        }
    }
}
