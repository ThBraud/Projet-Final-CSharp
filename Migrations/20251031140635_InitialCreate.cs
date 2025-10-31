using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_Finale.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id_client = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id_client);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id_car = table.Column<Guid>(type: "uuid", nullable: false),
                    id_client = table.Column<Guid>(type: "uuid", nullable: true),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Years = table.Column<int>(type: "integer", nullable: false),
                    PreTaxPrices = table.Column<float>(type: "real", nullable: false),
                    PriceIncludingTax = table.Column<float>(type: "real", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    IsSelling = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id_car);
                    table.ForeignKey(
                        name: "FK_Cars_Customers_id_client",
                        column: x => x.id_client,
                        principalTable: "Customers",
                        principalColumn: "id_client");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_id_client",
                table: "Cars",
                column: "id_client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
