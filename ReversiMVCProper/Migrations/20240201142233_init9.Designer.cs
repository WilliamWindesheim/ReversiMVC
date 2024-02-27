﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReversiMVCProper.Data;

namespace ReversiMVCProper.Migrations
{
    [DbContext(typeof(ReversiDbContext))]
    [Migration("20240201142233_init9")]
    partial class init9
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReversiMVCProper.Models.Spel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AanDeBeurt")
                        .HasColumnType("int");

                    b.Property<string>("Bord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Omschrijving")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speler1Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speler2Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Winnaar")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Spel");
                });

            modelBuilder.Entity("ReversiMVCProper.Models.Speler", b =>
                {
                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AantalGelijk")
                        .HasColumnType("int");

                    b.Property<int>("AantalGewonnen")
                        .HasColumnType("int");

                    b.Property<int>("AantalVerloren")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.ToTable("Spelers");
                });
#pragma warning restore 612, 618
        }
    }
}
