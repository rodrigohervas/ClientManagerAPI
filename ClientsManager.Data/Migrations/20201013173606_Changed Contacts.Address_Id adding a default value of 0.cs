using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class ChangedContactsAddress_Idaddingadefaultvalueof0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Address_Id",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start_DateTime",
                table: "BillableActivities",
                type: "DATETIME2 (7)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 13, 13, 36, 5, 866, DateTimeKind.Local).AddTicks(7981),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2 (7)",
                oldDefaultValue: new DateTime(2020, 10, 13, 12, 21, 9, 632, DateTimeKind.Local).AddTicks(2443));

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
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address_Id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address_Id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Address_Id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Address_Id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Address_Id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Address_Id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 7,
                column: "Address_Id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 8,
                column: "Address_Id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 9,
                column: "Address_Id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 10,
                column: "Address_Id",
                value: 2);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Address_Id",
                table: "Contacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start_DateTime",
                table: "BillableActivities",
                type: "DATETIME2 (7)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 13, 12, 21, 9, 632, DateTimeKind.Local).AddTicks(2443),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2 (7)",
                oldDefaultValue: new DateTime(2020, 10, 13, 13, 36, 5, 866, DateTimeKind.Local).AddTicks(7981));

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
        }
    }
}
