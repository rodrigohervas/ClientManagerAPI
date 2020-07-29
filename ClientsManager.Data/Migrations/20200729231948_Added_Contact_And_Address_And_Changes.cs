using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.Data.Migrations
{
    public partial class Added_Contact_And_Address_And_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Clients",
                type: "nvarchar(350)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Id = table.Column<int>(type: "int", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(350)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(75)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(75)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Clients_Client_Id",
                        column: x => x.Client_Id,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Id = table.Column<int>(type: "int", nullable: false),
                    Address_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Address_Address_Id",
                        column: x => x.Address_Id,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_Clients_Client_Id",
                        column: x => x.Client_Id,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Client_Id", "Country", "State", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "Silver Spring", 1, "United States", "Maryland", "53 6th Drive" },
                    { 2, "Saint Joseph", 1, "United States", "Missouri", "11392 Mayer Point" },
                    { 3, "Amarillo", 2, "United States", "Texas", "58 Esch Center" },
                    { 4, "Austin", 2, "United States", "Texas", "8 Veith Circle" },
                    { 5, "Evansville", 2, "United States", "Indiana", "819 Golden Leaf Terrace" }
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Website",
                value: "https://www.bigcompanyllc.com");

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address_Id", "Cellphone", "Client_Id", "Email", "Name", "Position", "Telephone" },
                values: new object[,]
                {
                    { 1, 1, "572-634-0767", 1, "mcoslett0@dedecms.com", "Myca Coslett", "Legal Assistant", "792-438-4570" },
                    { 8, 1, "448-516-4205", 2, "pcrilly7@123-reg.co.uk", "Pavel Crilly", "Librarian", "492-130-7365" },
                    { 10, 2, "768-183-0836", 2, "aakett9@de.vu", "Ardine Akett", "Food Chemist", "674-875-8519" },
                    { 3, 3, "524-411-4320", 2, "cfolli2@ask.com", "Carolan Folli", "Structural Analysis Engineer", "760-208-3333" },
                    { 6, 3, "819-964-7799", 1, "ggainfort5@imgur.com", "Gabriele Gainfort", "Senior Editor", "640-126-2181" },
                    { 2, 5, "866-685-4130", 1, "ddioniso1@printfriendly.com", "Dewitt Dioniso", "Project Manager", "943-333-2330" },
                    { 4, 5, "701-853-8605", 2, "eprudence3@jalbum.net", "Emmy Prudence", "Help Desk Technician", "402-106-2823" },
                    { 5, 5, "412-291-0241", 1, "rsimms4@tripadvisor.com", "Riane Simms", "Biostatistician I", "929-979-6229" },
                    { 7, 5, "547-991-5392", 1, "jgrave6@hc360.com", "Jimmy Grave", "Financial Advisor", "170-122-8134" },
                    { 9, 5, "947-673-3903", 1, "ltallboy8@reddit.com", "Lilias Tallboy", "Technical Writer", "847-641-7975" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_Client_Id",
                table: "Address",
                column: "Client_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Address_Id",
                table: "Contacts",
                column: "Address_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Client_Id",
                table: "Contacts",
                column: "Client_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Clients");
        }
    }
}
