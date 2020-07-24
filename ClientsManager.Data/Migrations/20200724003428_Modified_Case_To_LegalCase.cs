using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class Modified_Case_To_LegalCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillableActivities_Cases_Case_Id",
                table: "BillableActivities");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.CreateTable(
                name: "LegalCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustFund = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalCases_Clients_Client_Id",
                        column: x => x.Client_Id,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LegalCases",
                columns: new[] { "Id", "Client_Id", "Description", "Title", "TrustFund" },
                values: new object[,]
                {
                    { 1, 1, "Mr. Jones Will and State liquidation", "Mr. Jones Will", 5000.00m },
                    { 2, 1, "Alpa Corp Real State Purchase Agreement to aquire Mr. Jones residential property in Houston TX", "Alpa Corp Real State Purchase Agreement", 8000.00m },
                    { 3, 2, "Real State Commercial Lease from Santa Fe Realty", "Santa Fe Realty Commercial Lease", 4500.00m },
                    { 4, 2, "Purchase Agreement for Tico Inc aquisition", "Tico Inc Purchase Agreement", 3500.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegalCases_Client_Id",
                table: "LegalCases",
                column: "Client_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillableActivities_LegalCases_Case_Id",
                table: "BillableActivities",
                column: "Case_Id",
                principalTable: "LegalCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillableActivities_LegalCases_Case_Id",
                table: "BillableActivities");

            migrationBuilder.DropTable(
                name: "LegalCases");

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustFund = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Clients_Client_Id",
                        column: x => x.Client_Id,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "Id", "Client_Id", "Description", "Title", "TrustFund" },
                values: new object[,]
                {
                    { 1, 1, "Mr. Jones Will and State liquidation", "Mr. Jones Will", 5000.00m },
                    { 2, 1, "Alpa Corp Real State Purchase Agreement to aquire Mr. Jones residential property in Houston TX", "Alpa Corp Real State Purchase Agreement", 8000.00m },
                    { 3, 2, "Real State Commercial Lease from Santa Fe Realty", "Santa Fe Realty Commercial Lease", 4500.00m },
                    { 4, 2, "Purchase Agreement for Tico Inc aquisition", "Tico Inc Purchase Agreement", 3500.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_Client_Id",
                table: "Cases",
                column: "Client_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillableActivities_Cases_Case_Id",
                table: "BillableActivities",
                column: "Case_Id",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
