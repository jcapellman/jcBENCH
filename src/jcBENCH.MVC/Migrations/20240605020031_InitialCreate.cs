using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace jcBENCH.MVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benchmarks",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benchmarks", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "BenchmarkResults",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BenchmarkID = table.Column<string>(type: "text", nullable: false),
                    PlatformID = table.Column<string>(type: "text", nullable: false),
                    CPUManufacturer = table.Column<string>(type: "text", nullable: false),
                    CPUName = table.Column<string>(type: "text", nullable: false),
                    OperatingSystem = table.Column<string>(type: "text", nullable: false),
                    CPUFrequency = table.Column<string>(type: "text", nullable: false),
                    CPUArchitecture = table.Column<string>(type: "text", nullable: false),
                    BenchmarkResult = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchmarkResults", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BenchmarkResults_Benchmarks_BenchmarkID",
                        column: x => x.BenchmarkID,
                        principalTable: "Benchmarks",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenchmarkResults_Platforms_PlatformID",
                        column: x => x.PlatformID,
                        principalTable: "Platforms",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkResults_BenchmarkID",
                table: "BenchmarkResults",
                column: "BenchmarkID");

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkResults_PlatformID",
                table: "BenchmarkResults",
                column: "PlatformID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenchmarkResults");

            migrationBuilder.DropTable(
                name: "Benchmarks");

            migrationBuilder.DropTable(
                name: "Platforms");
        }
    }
}
