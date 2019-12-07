﻿// <auto-generated />
using HybridAi.TestTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HybridAi.TestTask.Data.Migrations
{
    [DbContext(typeof(IpDbContext))]
    [Migration("20191202173618_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HybridAi.TestTask.Data.Models.CityBlockIpv4", b =>
                {
                    b.Property<string>("Network")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<short>("AccuracyRadius")
                        .HasColumnType("smallint");

                    b.Property<int>("CityLocationGeonameId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAnonymousProxy")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSatelliteProvider")
                        .HasColumnType("boolean");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("PostalCode")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<int>("RegistredCountryGeonameId")
                        .HasColumnType("integer");

                    b.Property<int>("RepresentedCountryGeonameId")
                        .HasColumnType("integer");

                    b.HasKey("Network");

                    b.HasIndex("CityLocationGeonameId");

                    b.ToTable("CityBlocksIpv4","dbo");
                });

            modelBuilder.Entity("HybridAi.TestTask.Data.Models.CityBlockIpv6", b =>
                {
                    b.Property<string>("Network")
                        .HasColumnType("varchar(39)")
                        .HasMaxLength(39);

                    b.Property<short>("AccuracyRadius")
                        .HasColumnType("smallint");

                    b.Property<int>("CityLocationGeonameId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAnonymousProxy")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSatelliteProvider")
                        .HasColumnType("boolean");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("PostalCode")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<int>("RegistredCountryGeonameId")
                        .HasColumnType("integer");

                    b.Property<int>("RepresentedCountryGeonameId")
                        .HasColumnType("integer");

                    b.HasKey("Network");

                    b.HasIndex("CityLocationGeonameId");

                    b.ToTable("CityBlocksIpv6","dbo");
                });

            modelBuilder.Entity("HybridAi.TestTask.Data.Models.CityLocation", b =>
                {
                    b.Property<int>("GeonameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int4")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ContinentCode")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<string>("ContinentName")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<string>("CopuntryIsoCode")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<string>("CountryName")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<bool>("IsInEuropeanUnion")
                        .HasColumnType("boolean");

                    b.Property<string>("LocaleCode")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<string>("MetroCode")
                        .HasColumnType("character varying(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Subdivision1IsoCode")
                        .HasColumnType("character varying(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Subdivision1Name")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Subdivision2IsoCode")
                        .HasColumnType("character varying(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Subdivision2Name")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("TimeZone")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.HasKey("GeonameId");

                    b.ToTable("CityLocations","dbo");
                });

            modelBuilder.Entity("HybridAi.TestTask.Data.Models.CityBlockIpv4", b =>
                {
                    b.HasOne("HybridAi.TestTask.Data.Models.CityLocation", "CityLocation")
                        .WithMany("CityBlockIpv4Collection")
                        .HasForeignKey("CityLocationGeonameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HybridAi.TestTask.Data.Models.CityBlockIpv6", b =>
                {
                    b.HasOne("HybridAi.TestTask.Data.Models.CityLocation", "CityLocation")
                        .WithMany("CityBlockIpv6Collection")
                        .HasForeignKey("CityLocationGeonameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}