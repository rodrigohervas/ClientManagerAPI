using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeType_Id = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeTypes_EmployeeType_Id",
                        column: x => x.EmployeeType_Id,
                        principalTable: "EmployeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeFrames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Start_DateTime = table.Column<DateTime>(type: "DATETIME2 (7)", nullable: false),
                    Finish_DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeFrames_Employees_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Worker" });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { 2, "Manager" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmployeeType_Id", "Name", "Position" },
                values: new object[,]
                {
                    { 1, 1, "John Smith", "Junior Lawyer" },
                    { 2, 1, "Jane Doe", "Junior Lawyer" },
                    { 3, 1, "Michael Jones", "Junior Lawyer" },
                    { 4, 1, "Eliza Deer", "Junior Lawyer" },
                    { 5, 2, "Peter Granger", "Partner" },
                    { 6, 2, "Mary Osterfitz", "Partner" }
                });

            migrationBuilder.InsertData(
                table: "TimeFrames",
                columns: new[] { "Id", "Description", "Employee_Id", "Finish_DateTime", "Price", "Start_DateTime", "Title" },
                values: new object[,]
                {
                    { 1, "this is the timeframe 1", 1, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 120.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "timeframe 1" },
                    { 2, "this is the timeframe 2", 1, new DateTime(2020, 6, 21, 17, 30, 0, 0, DateTimeKind.Unspecified), 400m, new DateTime(2020, 6, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), "timeframe 2" },
                    { 3, "this is the timeframe 3", 2, new DateTime(2020, 6, 23, 13, 30, 0, 0, DateTimeKind.Unspecified), 300.50m, new DateTime(2020, 6, 23, 8, 15, 0, 0, DateTimeKind.Unspecified), "timeframe 3" },
                    { 4, "this is the timeframe 4", 3, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 120.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "timeframe 4" },
                    { 5, "this is the timeframe 5", 3, new DateTime(2020, 6, 21, 17, 30, 0, 0, DateTimeKind.Unspecified), 400m, new DateTime(2020, 6, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), "timeframe 5" },
                    { 6, "this is the timeframe 6", 4, new DateTime(2020, 6, 23, 13, 30, 0, 0, DateTimeKind.Unspecified), 300.50m, new DateTime(2020, 6, 23, 8, 15, 0, 0, DateTimeKind.Unspecified), "timeframe 6" },
                    { 7, "this is the timeframe 7", 5, new DateTime(2020, 6, 23, 13, 30, 0, 0, DateTimeKind.Unspecified), 300.50m, new DateTime(2020, 6, 23, 8, 15, 0, 0, DateTimeKind.Unspecified), "timeframe 7" },
                    { 8, "this is the timeframe 8", 5, new DateTime(2020, 6, 23, 13, 30, 0, 0, DateTimeKind.Unspecified), 300.50m, new DateTime(2020, 6, 23, 8, 15, 0, 0, DateTimeKind.Unspecified), "timeframe 8" },
                    { 9, "this is the timeframe 9", 6, new DateTime(2020, 6, 23, 13, 30, 0, 0, DateTimeKind.Unspecified), 300.50m, new DateTime(2020, 6, 23, 8, 15, 0, 0, DateTimeKind.Unspecified), "timeframe 9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeType_Id",
                table: "Employees",
                column: "EmployeeType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TimeFrames_Employee_Id",
                table: "TimeFrames",
                column: "Employee_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeFrames");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");
        }
    }
}
