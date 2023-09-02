using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterCases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Reservation",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "houseId",
                table: "Reservation",
                newName: "HouseId");

            migrationBuilder.RenameColumn(
                name: "endDate",
                table: "Reservation",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "discountCode",
                table: "Reservation",
                newName: "DiscountCode");

            migrationBuilder.RenameColumn(
                name: "clientPhoneNumber",
                table: "Reservation",
                newName: "ClientPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "clientName",
                table: "Reservation",
                newName: "ClientName");

            migrationBuilder.RenameColumn(
                name: "clientLastname",
                table: "Reservation",
                newName: "ClientLastName");

            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "Reservation",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "clientAge",
                table: "Reservation",
                newName: "ClientAge");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reservation",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "available",
                table: "House",
                newName: "Available");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "House",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "phoneNumber",
                table: "Clients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Clients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Clients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Clients",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clients",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Reservation",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Reservation",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "HouseId",
                table: "Reservation",
                newName: "houseId");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Reservation",
                newName: "endDate");

            migrationBuilder.RenameColumn(
                name: "DiscountCode",
                table: "Reservation",
                newName: "discountCode");

            migrationBuilder.RenameColumn(
                name: "ClientPhoneNumber",
                table: "Reservation",
                newName: "clientPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "Reservation",
                newName: "clientName");

            migrationBuilder.RenameColumn(
                name: "ClientLastName",
                table: "Reservation",
                newName: "clientLastname");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Reservation",
                newName: "clientId");

            migrationBuilder.RenameColumn(
                name: "ClientAge",
                table: "Reservation",
                newName: "clientAge");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservation",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "House",
                newName: "available");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "House",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Clients",
                newName: "phoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Clients",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Clients",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clients",
                newName: "id");

            migrationBuilder.AlterColumn<short>(
                name: "clientId",
                table: "Reservation",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
