using Microsoft.EntityFrameworkCore.Migrations;

namespace Degree.AppDbContext.Migrations
{
    public partial class addoptionalquoted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetsRaw_TweetsRaw_QuotedStatusId",
                table: "TweetsRaw");

            migrationBuilder.AlterColumn<long>(
                name: "QuotedStatusId",
                table: "TweetsRaw",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TweetsRaw_TweetsRaw_QuotedStatusId",
                table: "TweetsRaw",
                column: "QuotedStatusId",
                principalTable: "TweetsRaw",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetsRaw_TweetsRaw_QuotedStatusId",
                table: "TweetsRaw");

            migrationBuilder.AlterColumn<long>(
                name: "QuotedStatusId",
                table: "TweetsRaw",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TweetsRaw_TweetsRaw_QuotedStatusId",
                table: "TweetsRaw",
                column: "QuotedStatusId",
                principalTable: "TweetsRaw",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
