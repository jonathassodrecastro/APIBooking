using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterClientAndReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "clientId",
                table: "Reservation",
                type: "varchar(14)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "clientId",
                table: "Reservation",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)");
        }
    }
}
