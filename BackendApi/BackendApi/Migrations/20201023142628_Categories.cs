using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id_category = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id_category);
                });

            migrationBuilder.CreateTable(
                name: "article_category",
                columns: table => new
                {
                    id_article = table.Column<int>(nullable: false),
                    id_category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_category", x => new { x.id_article, x.id_category });
                    table.ForeignKey(
                        name: "FK_article_category_article_id_article",
                        column: x => x.id_article,
                        principalTable: "article",
                        principalColumn: "id_article",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_article_category_category_id_category",
                        column: x => x.id_category,
                        principalTable: "category",
                        principalColumn: "id_category",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_category_id_category",
                table: "article_category",
                column: "id_category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_category");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
