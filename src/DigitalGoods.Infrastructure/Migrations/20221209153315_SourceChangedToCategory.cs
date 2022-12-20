using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SourceChangedToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Sources_SourceId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Sources_SourceId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Tags",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_SourceId",
                table: "Tags",
                newName: "IX_Tags_CategoryId");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Offers",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_SourceId",
                table: "Offers",
                newName: "IX_Offers_CategoryId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Categories_CategoryId",
                table: "Offers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Categories_CategoryId",
                table: "Tags",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Categories_CategoryId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Categories_CategoryId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Tags",
                newName: "SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_CategoryId",
                table: "Tags",
                newName: "IX_Tags_SourceId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Offers",
                newName: "SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_CategoryId",
                table: "Offers",
                newName: "IX_Offers_SourceId");

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sources_Sources_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sources_ParentId",
                table: "Sources",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Sources_SourceId",
                table: "Offers",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Sources_SourceId",
                table: "Tags",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
