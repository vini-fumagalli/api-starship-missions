using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class StarshipMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MISSIONS_STARSHIP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StarshipName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Goal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crew = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MISSIONS_STARSHIP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STARSHIPS",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostInCredidts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAtmospheringSpeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passengers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HyperdriveRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mglt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STARSHIPS", x => x.Name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MISSIONS_STARSHIP");

            migrationBuilder.DropTable(
                name: "STARSHIPS");
        }
    }
}
