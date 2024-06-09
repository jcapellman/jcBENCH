using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jcBENCH.MVC.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BenchmarkAPIVersion",
                table: "BenchmarkResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BenchmarkThreadingModel",
                table: "BenchmarkResults",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BenchmarkAPIVersion",
                table: "BenchmarkResults");

            migrationBuilder.DropColumn(
                name: "BenchmarkThreadingModel",
                table: "BenchmarkResults");
        }
    }
}
