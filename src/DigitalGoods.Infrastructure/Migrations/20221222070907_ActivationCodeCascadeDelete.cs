using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActivationCodeCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivationCodes_Offers_OfferId",
                table: "ActivationCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivationCodes_Offers_OfferId",
                table: "ActivationCodes",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivationCodes_Offers_OfferId",
                table: "ActivationCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivationCodes_Offers_OfferId",
                table: "ActivationCodes",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }
    }
}
