using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jcBENCH.MVC.Migrations
{
    /// <inheritdoc />
    public partial class CleanUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenchmarkResults_Benchmarks_BenchmarkID",
                table: "BenchmarkResults");

            migrationBuilder.DropForeignKey(
                name: "FK_BenchmarkResults_Platforms_PlatformID",
                table: "BenchmarkResults");

            migrationBuilder.DropTable(
                name: "Benchmarks");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_BenchmarkResults_BenchmarkID",
                table: "BenchmarkResults");

            migrationBuilder.DropIndex(
                name: "IX_BenchmarkResults_PlatformID",
                table: "BenchmarkResults");

            migrationBuilder.DropColumn(
                name: "BenchmarkID",
                table: "BenchmarkResults");

            migrationBuilder.DropColumn(
                name: "CPUFrequency",
                table: "BenchmarkResults");

            migrationBuilder.DropColumn(
                name: "CPUManufacturer",
                table: "BenchmarkResults");

            migrationBuilder.RenameColumn(
                name: "PlatformID",
                table: "BenchmarkResults",
                newName: "BenchmarkName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BenchmarkName",
                table: "BenchmarkResults",
                newName: "PlatformID");

            migrationBuilder.AddColumn<string>(
                name: "BenchmarkID",
                table: "BenchmarkResults",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CPUFrequency",
                table: "BenchmarkResults",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CPUManufacturer",
                table: "BenchmarkResults",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkResults_BenchmarkID",
                table: "BenchmarkResults",
                column: "BenchmarkID");

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkResults_PlatformID",
                table: "BenchmarkResults",
                column: "PlatformID");

            migrationBuilder.AddForeignKey(
                name: "FK_BenchmarkResults_Benchmarks_BenchmarkID",
                table: "BenchmarkResults",
                column: "BenchmarkID",
                principalTable: "Benchmarks",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BenchmarkResults_Platforms_PlatformID",
                table: "BenchmarkResults",
                column: "PlatformID",
                principalTable: "Platforms",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
