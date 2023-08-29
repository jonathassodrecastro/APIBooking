using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APIBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    lastname = table.Column<string>(type: "varchar(50)", nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    available = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.id);
                });

            migrationBuilder.CreateTable(
            name: "Reservation",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                 .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                clientId = table.Column<short>(type: "integer", nullable: false),
                clientName = table.Column<string>(type: "varchar(50)", nullable: false),
                clientLastname = table.Column<string>(type: "varchar(50)", nullable: false),
                clientAge = table.Column<short>(type: "smallint", nullable: false),
                clientPhoneNumber = table.Column<string>(type: "varchar(30)", nullable: false),
                startDate = table.Column<DateTime>(type: "date", nullable: false),
                endDate = table.Column<DateTime>(type: "date", nullable: false),
                houseId = table.Column<short>(type: "smallint", nullable: false),
                discountCode = table.Column<string>(type: "varchar(10)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reservation", x => x.id);
                table.ForeignKey(
                    name: "FK_Reservation_House_houseId",
                    column: x => x.houseId,
                    principalTable: "House",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                            name: "FK_Reservation_Clients_clientId", // Nome da chave estrangeira
                            column: x => x.clientId,
                            principalTable: "Clients", // Nome da tabela relacionada
                            principalColumn: "id", // Coluna referenciada na tabela relacionada
                            onDelete: ReferentialAction.Cascade
                        );
            });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "House");

            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
