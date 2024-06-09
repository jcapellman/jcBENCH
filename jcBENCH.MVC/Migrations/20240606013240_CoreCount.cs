using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jcBENCH.MVC.Migrations
{
    /// <inheritdoc />
    public partial class CoreCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CPUCoreCount",
                table: "BenchmarkResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPUCoreCount",
                table: "BenchmarkResults");
        }
    }
}
