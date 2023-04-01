using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coverlet.Experiment.Api.Migrations;

/// <inheritdoc />
public partial class Forecasts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Forecasts",
            columns: table => new
            {
                Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                TemperatureC = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Forecasts", x => x.Date);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Forecasts");
    }
}
