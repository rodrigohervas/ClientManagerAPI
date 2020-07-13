using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeFrames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Start_DateTime = table.Column<DateTime>(nullable: false),
                    Finish_DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TimeFrames",
                columns: new[] { "Id", "Description", "Employee_Id", "Finish_DateTime", "Price", "Start_DateTime", "Title" },
                values: new object[] { 1, "this is the timeframe 1", 1, new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 120.50m, new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), "timeframe 1" });

            migrationBuilder.InsertData(
                table: "TimeFrames",
                columns: new[] { "Id", "Description", "Employee_Id", "Finish_DateTime", "Price", "Start_DateTime", "Title" },
                values: new object[] { 2, "this is the timeframe 2", 1, new DateTime(2020, 6, 21, 17, 30, 0, 0, DateTimeKind.Unspecified), 400m, new DateTime(2020, 6, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), "timeframe 2" });

            migrationBuilder.InsertData(
                table: "TimeFrames",
                columns: new[] { "Id", "Description", "Employee_Id", "Finish_DateTime", "Price", "Start_DateTime", "Title" },
                values: new object[] { 3, "this is the timeframe 3", 2, new DateTime(2020, 6, 23, 13, 30, 0, 0, DateTimeKind.Unspecified), 300.50m, new DateTime(2020, 6, 23, 8, 15, 0, 0, DateTimeKind.Unspecified), "timeframe 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeFrames");
        }
    }
}
