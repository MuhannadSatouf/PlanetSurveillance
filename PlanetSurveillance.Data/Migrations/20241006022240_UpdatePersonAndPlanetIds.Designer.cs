﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanetSurveillance.Data;

#nullable disable

namespace PlanetSurveillance.Data.Migrations
{
    [DbContext(typeof(PlanetSurveillanceDbContext))]
    [Migration("20241006022240_UpdatePersonAndPlanetIds")]
    partial class UpdatePersonAndPlanetIds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PlanetSurveillance.Data.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("BirthYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Created")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Edited")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EyeColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Films")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HairColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Height")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Homeworld")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SWAPIId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkinColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Starships")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vehicles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PlanetSurveillance.Data.Models.Planet", b =>
                {
                    b.Property<int>("PlanetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlanetId"));

                    b.Property<string>("Climate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Created")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diameter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Edited")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Films")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gravity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrbitalPeriod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Population")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Residents")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RotationPeriod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SWAPIId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurfaceWater")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terrain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlanetId");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("PlanetSurveillance.Data.Models.Visit", b =>
                {
                    b.Property<int>("VisitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisitId"));

                    b.Property<DateTime>("DateOfVisit")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("PlanetId")
                        .HasColumnType("int");

                    b.HasKey("VisitId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PlanetId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("PlanetSurveillance.Data.Models.Visit", b =>
                {
                    b.HasOne("PlanetSurveillance.Data.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlanetSurveillance.Data.Models.Planet", "Planet")
                        .WithMany()
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Planet");
                });
#pragma warning restore 612, 618
        }
    }
}
