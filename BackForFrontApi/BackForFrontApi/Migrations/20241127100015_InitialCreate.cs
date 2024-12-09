using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackForFrontApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Photo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Bidder = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "Country", "Description", "Photo", "Price" },
                values: new object[,]
                {
                    { 1, "12 Valley of Kings, Geneva", "Switzerland", "A superb detached Victorian property on one of the town's finest roads, within easy reach of Barnes Village. The property has in excess of 6000 sq/ft of accommodation, a driveway and landscaped garden.", null, 900000 },
                    { 2, "89 Road of Forks, Bern", "Switzerland", "This impressive family home, which dates back to approximately 1840, offers original period features throughout and is set back from the road with off street parking for up to six cars and an original Coach House, which has been incorporated into the main house to provide further accommodation. ", null, 500000 },
                    { 3, "Grote Hof 12, Amsterdam", "The Netherlands", "This house has been designed and built to an impeccable standard offering luxurious and elegant living. The accommodation is arranged over four floors comprising a large entrance hall, living room with tall sash windows, dining room, study and WC on the ground floor.", null, 200500 },
                    { 4, "Meel Kade 321, The Hague", "The Netherlands", "Discreetly situated a unique two storey period home enviably located on the corner of Krom Road and Recht Road offering seclusion and privacy. The house features a magnificent double height reception room with doors leading directly out onto a charming courtyard garden.", null, 259500 },
                    { 5, "Oude Gracht 32, Utrecht", "The Netherlands", "This luxurious three bedroom flat is contemporary in style and benefits from the use of a gymnasium and a reserved underground parking space.", null, 400500 }
                });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "Bidder", "HouseId" },
                values: new object[,]
                {
                    { 1, 200000, "Sonia Reading", 1 },
                    { 2, 202400, "Dick Johnson", 1 },
                    { 3, 302400, "Mohammed Vahls", 2 },
                    { 4, 310500, "Jane Williams", 2 },
                    { 5, 315400, "John Kepler", 2 },
                    { 6, 201000, "Bill Mentor", 3 },
                    { 7, 410000, "Melissa Kirk", 4 },
                    { 8, 450000, "Scott Max", 4 },
                    { 9, 470000, "Christine James", 4 },
                    { 10, 450000, "Omesh Carim", 5 },
                    { 11, 150000, "Charlotte Max", 5 },
                    { 12, 170000, "Marcus Scott", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_HouseId",
                table: "Bids",
                column: "HouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}
