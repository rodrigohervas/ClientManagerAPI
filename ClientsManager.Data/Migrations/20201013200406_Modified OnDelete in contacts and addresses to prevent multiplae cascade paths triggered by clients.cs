using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class ModifiedOnDeleteincontactsandaddressestopreventmultiplaecascadepathstriggeredbyclients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Clients_Client_Id",
                table: "Addresses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start_DateTime",
                table: "BillableActivities",
                type: "DATETIME2 (7)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 13, 16, 4, 5, 673, DateTimeKind.Local).AddTicks(8560),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2 (7)",
                oldDefaultValue: new DateTime(2020, 10, 13, 15, 51, 40, 19, DateTimeKind.Local).AddTicks(551));

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 200.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 200.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 300.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 100.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 250.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 450.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 400.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 350.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 250.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 500.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 100.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 150.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 300.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LegalCases",
                keyColumn: "Id",
                keyValue: 1,
                column: "TrustFund",
                value: 5000.00m);

            migrationBuilder.UpdateData(
                table: "LegalCases",
                keyColumn: "Id",
                keyValue: 2,
                column: "TrustFund",
                value: 8000.00m);

            migrationBuilder.UpdateData(
                table: "LegalCases",
                keyColumn: "Id",
                keyValue: 3,
                column: "TrustFund",
                value: 4500.00m);

            migrationBuilder.UpdateData(
                table: "LegalCases",
                keyColumn: "Id",
                keyValue: 4,
                column: "TrustFund",
                value: 3500.00m);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Clients_Client_Id",
                table: "Addresses",
                column: "Client_Id",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Clients_Client_Id",
                table: "Addresses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start_DateTime",
                table: "BillableActivities",
                type: "DATETIME2 (7)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 13, 15, 51, 40, 19, DateTimeKind.Local).AddTicks(551),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2 (7)",
                oldDefaultValue: new DateTime(2020, 10, 13, 16, 4, 5, 673, DateTimeKind.Local).AddTicks(8560));

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 200.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 200.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 300.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 100.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 250.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 450.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 400.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 350.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 250.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 500.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 100.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 150.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BillableActivities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Price", "Start_DateTime" },
                values: new object[] { 300.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "LegalCases",
                keyColumn: "Id",
                keyValue: 1,
                column: "TrustFund",
                value: 5000.00m);

            migrationBuilder.UpdateData(
                table: "LegalCases",
                keyColumn: "Id",
                keyValue: 2,
                column: "TrustFund",
                value: 8000.00m);

            migrationBuilder.UpdateData(
                table: "LegalCases",
                keyColumn: "Id",
                keyValue: 3,
                column: "TrustFund",
                value: 4500.00m);

            migrationBuilder.UpdateData(
                table: "LegalCases",
                keyColumn: "Id",
                keyValue: 4,
                column: "TrustFund",
                value: 3500.00m);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Clients_Client_Id",
                table: "Addresses",
                column: "Client_Id",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
