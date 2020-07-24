using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class BillableActivity_LegalCase_Id_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillableActivities_LegalCases_Case_Id",
                table: "BillableActivities");

            migrationBuilder.RenameColumn(
                name: "Case_Id",
                table: "BillableActivities",
                newName: "LegalCase_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BillableActivities_Case_Id",
                table: "BillableActivities",
                newName: "IX_BillableActivities_LegalCase_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillableActivities_LegalCases_LegalCase_Id",
                table: "BillableActivities",
                column: "LegalCase_Id",
                principalTable: "LegalCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillableActivities_LegalCases_LegalCase_Id",
                table: "BillableActivities");

            migrationBuilder.RenameColumn(
                name: "LegalCase_Id",
                table: "BillableActivities",
                newName: "Case_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BillableActivities_LegalCase_Id",
                table: "BillableActivities",
                newName: "IX_BillableActivities_Case_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillableActivities_LegalCases_Case_Id",
                table: "BillableActivities",
                column: "Case_Id",
                principalTable: "LegalCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
