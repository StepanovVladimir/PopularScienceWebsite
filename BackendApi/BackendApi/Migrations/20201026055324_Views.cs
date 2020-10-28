using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class Views : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "view",
                columns: table => new
                {
                    id_article = table.Column<int>(nullable: false),
                    id_user = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_view", x => new { x.id_article, x.id_user });
                    table.ForeignKey(
                        name: "FK_view_article_id_article",
                        column: x => x.id_article,
                        principalTable: "article",
                        principalColumn: "id_article",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_view_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_view_id_user",
                table: "view",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "view");
        }
    }
}
