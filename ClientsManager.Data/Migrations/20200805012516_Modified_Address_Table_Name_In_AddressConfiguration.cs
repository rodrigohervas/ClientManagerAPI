using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class Modified_Address_Table_Name_In_AddressConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Clients_Client_Id",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Address_Address_Id",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Address_Client_Id",
                table: "Addresses",
                newName: "IX_Addresses_Client_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Clients_Client_Id",
                table: "Addresses",
                column: "Client_Id",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Addresses_Address_Id",
                table: "Contacts",
                column: "Address_Id",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Clients_Client_Id",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Addresses_Address_Id",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_Client_Id",
                table: "Address",
                newName: "IX_Address_Client_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Clients_Client_Id",
                table: "Address",
                column: "Client_Id",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Address_Address_Id",
                table: "Contacts",
                column: "Address_Id",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
