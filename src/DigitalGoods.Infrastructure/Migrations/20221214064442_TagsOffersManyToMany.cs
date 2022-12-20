using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TagsOffersManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Offers_OfferId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_OfferId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "OfferTag",
                columns: table => new
                {
                    OffersId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTag", x => new { x.OffersId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_OfferTag_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferTag_TagsId",
                table: "OfferTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferTag");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_OfferId",
                table: "Tags",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Offers_OfferId",
                table: "Tags",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }
    }
}
