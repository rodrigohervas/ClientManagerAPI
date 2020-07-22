using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class Changed_TimeFrame_To_BillableActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeFrames");

            migrationBuilder.CreateTable(
                name: "BillableActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Case_Id = table.Column<int>(type: "int", nullable: false),
                    Employee_Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Start_DateTime = table.Column<DateTime>(type: "DATETIME2 (7)", nullable: false),
                    Finish_DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillableActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillableActivities_Employees_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BillableActivities",
                columns: new[] { "Id", "Case_Id", "Description", "Employee_Id", "Finish_DateTime", "Price", "Start_DateTime", "Title" },
                values: new object[,]
                {
                    { 1, 1, "this is the Billable Activity 1", 1, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 200.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 1" },
                    { 2, 1, "this is the Billable Activity 2", 1, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 200.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 2" },
                    { 3, 1, "this is the Billable Activity 3", 2, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 300.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 3" },
                    { 4, 1, "this is the Billable Activity 4", 2, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 100.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 4" },
                    { 5, 2, "this is the Billable Activity 5", 2, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 250.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 5" },
                    { 6, 1, "this is the Billable Activity 6", 3, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 450.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 6" },
                    { 7, 1, "this is the Billable Activity 7", 3, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 400.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 7" },
                    { 8, 2, "this is the Billable Activity 8", 4, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 350.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 8" },
                    { 9, 2, "this is the Billable Activity 9", 4, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 250.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 9" },
                    { 10, 2, "this is the Billable Activity 10", 5, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 500.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 10" },
                    { 11, 1, "this is the Billable Activity 11", 5, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 100.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 11" },
                    { 12, 2, "this is the Billable Activity 12", 6, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 150.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 12" },
                    { 13, 2, "this is the Billable Activity 13", 6, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 300.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "Billable Activity 13" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillableActivities_Employee_Id",
                table: "BillableActivities",
                column: "Employee_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillableActivities");

            migrationBuilder.CreateTable(
                name: "TimeFrames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_Id = table.Column<int>(type: "int", nullable: false),
                    Finish_DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Start_DateTime = table.Column<DateTime>(type: "DATETIME2 (7)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "IX_TimeFrames_Employee_Id",
                table: "TimeFrames",
                column: "Employee_Id");
        }
    }
}
