using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class Likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "category",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "like",
                columns: table => new
                {
                    id_article = table.Column<int>(nullable: false),
                    id_user = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_like", x => new { x.id_article, x.id_user });
                    table.ForeignKey(
                        name: "FK_like_article_id_article",
                        column: x => x.id_article,
                        principalTable: "article",
                        principalColumn: "id_article",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_like_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_name",
                table: "category",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_like_id_user",
                table: "like",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "like");

            migrationBuilder.DropIndex(
                name: "IX_category_name",
                table: "category");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "category",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
