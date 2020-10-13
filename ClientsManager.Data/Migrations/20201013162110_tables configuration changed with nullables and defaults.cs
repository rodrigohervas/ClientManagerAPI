using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class tablesconfigurationchangedwithnullablesanddefaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillableActivities_Employees_Employee_Id",
                table: "BillableActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Addresses_Address_Id",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeType_Id",
                table: "Employees");

            migrationBuilder.AlterColumn<decimal>(
                name: "TrustFund",
                table: "LegalCases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LegalCases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Contacts",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(320)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(320)");

            migrationBuilder.AlterColumn<string>(
                name: "Cellphone",
                table: "Contacts",
                type: "nvarchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start_DateTime",
                table: "BillableActivities",
                type: "DATETIME2 (7)",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 13, 12, 21, 9, 632, DateTimeKind.Local).AddTicks(2443),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2 (7)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "BillableActivities",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BillableActivities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Addresses",
                type: "nvarchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "StateProvince",
                table: "Addresses",
                type: "nvarchar(75)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "nvarchar(75)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)");

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
                name: "FK_BillableActivities_Employees_Employee_Id",
                table: "BillableActivities",
                column: "Employee_Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Addresses_Address_Id",
                table: "Contacts",
                column: "Address_Id",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeType_Id",
                table: "Employees",
                column: "EmployeeType_Id",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillableActivities_Employees_Employee_Id",
                table: "BillableActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Addresses_Address_Id",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeType_Id",
                table: "Employees");

            migrationBuilder.AlterColumn<decimal>(
                name: "TrustFund",
                table: "LegalCases",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LegalCases",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Contacts",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(320)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(320)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cellphone",
                table: "Contacts",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start_DateTime",
                table: "BillableActivities",
                type: "DATETIME2 (7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2 (7)",
                oldDefaultValue: new DateTime(2020, 10, 13, 12, 21, 9, 632, DateTimeKind.Local).AddTicks(2443));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "BillableActivities",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BillableActivities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Addresses",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StateProvince",
                table: "Addresses",
                type: "nvarchar(75)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "nvarchar(75)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillableActivities_Employees_Employee_Id",
                table: "BillableActivities",
                column: "Employee_Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Addresses_Address_Id",
                table: "Contacts",
                column: "Address_Id",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeType_Id",
                table: "Employees",
                column: "EmployeeType_Id",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
