using Microsoft.EntityFrameworkCore.Migrations;

namespace Degree.AppDbContext.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets");

            migrationBuilder.RenameTable(
                name: "Tweets",
                newName: "TweetsRaw");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TweetsRaw",
                table: "TweetsRaw",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TweetsRaw",
                table: "TweetsRaw");

            migrationBuilder.RenameTable(
                name: "TweetsRaw",
                newName: "Tweets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets",
                column: "id");
        }
    }
}
