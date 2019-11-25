using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Degree.AppDbContext.Migrations
{
    public partial class addoptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetsRaw_Entities_EntitiesId",
                table: "TweetsRaw");

            migrationBuilder.DropForeignKey(
                name: "FK_TweetsRaw_Entities_ExtendedEntitiesId",
                table: "TweetsRaw");

            migrationBuilder.AlterColumn<long>(
                name: "InReplyToUserId",
                table: "TweetsRaw",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "InReplyToStatusId",
                table: "TweetsRaw",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExtendedEntitiesId",
                table: "TweetsRaw",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntitiesId",
                table: "TweetsRaw",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_TweetsRaw_Entities_EntitiesId",
                table: "TweetsRaw",
                column: "EntitiesId",
                principalTable: "Entities",
                principalColumn: "EntitieId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TweetsRaw_Entities_ExtendedEntitiesId",
                table: "TweetsRaw",
                column: "ExtendedEntitiesId",
                principalTable: "Entities",
                principalColumn: "EntitieId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetsRaw_Entities_EntitiesId",
                table: "TweetsRaw");

            migrationBuilder.DropForeignKey(
                name: "FK_TweetsRaw_Entities_ExtendedEntitiesId",
                table: "TweetsRaw");

            migrationBuilder.AlterColumn<long>(
                name: "InReplyToUserId",
                table: "TweetsRaw",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "InReplyToStatusId",
                table: "TweetsRaw",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ExtendedEntitiesId",
                table: "TweetsRaw",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EntitiesId",
                table: "TweetsRaw",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TweetsRaw_Entities_EntitiesId",
                table: "TweetsRaw",
                column: "EntitiesId",
                principalTable: "Entities",
                principalColumn: "EntitieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TweetsRaw_Entities_ExtendedEntitiesId",
                table: "TweetsRaw",
                column: "ExtendedEntitiesId",
                principalTable: "Entities",
                principalColumn: "EntitieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
