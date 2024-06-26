﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using jcBENCH.MVC.DAL;

#nullable disable

namespace jcBENCH.MVC.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20240606012440_RemovedCreated")]
    partial class RemovedCreated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("jcBENCH.MVC.DAL.Objects.Results", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("BenchmarkName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BenchmarkResult")
                        .HasColumnType("integer");

                    b.Property<string>("CPUArchitecture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CPUName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OperatingSystem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("BenchmarkResults");
                });
#pragma warning restore 612, 618
        }
    }
}
