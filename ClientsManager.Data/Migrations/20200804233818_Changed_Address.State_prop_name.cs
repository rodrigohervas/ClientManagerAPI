using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class Changed_AddressState_prop_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Address",
                newName: "StateProvince");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Address",
                type: "nvarchar(15)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 1,
                column: "ZipCode",
                value: "20811");

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 2,
                column: "ZipCode",
                value: "20812");

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 3,
                column: "ZipCode",
                value: "20813");

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 4,
                column: "ZipCode",
                value: "20814");

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 5,
                column: "ZipCode",
                value: "20815");

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Client_Id", "Country", "StateProvince", "StreetNumber", "ZipCode" },
                values: new object[] { 6, "Madrid", 2, "Spain", "Madrid", "Calle Lopez Masquez, 34", "28032" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "StateProvince",
                table: "Address",
                newName: "State");
        }
    }
}
