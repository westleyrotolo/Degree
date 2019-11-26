using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Degree.AppDbContext.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ScreenName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Verified = table.Column<bool>(nullable: false),
                    Followers = table.Column<int>(nullable: false),
                    Following = table.Column<int>(nullable: false),
                    Statuses = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ProfileImage = table.Column<string>(nullable: true),
                    ProfileBanner = table.Column<string>(nullable: true),
                    DefaultProfile = table.Column<bool>(nullable: false),
                    default_profile_image = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TweetsRaw",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Truncated = table.Column<bool>(nullable: false),
                    InReplyToStatusId = table.Column<long>(nullable: true),
                    InReplyToUserId = table.Column<long>(nullable: true),
                    InReplyToScreenName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    QuotedStatusId = table.Column<long>(nullable: true),
                    IsQuoteStatus = table.Column<bool>(nullable: false),
                    RetweetedStatusId = table.Column<long>(nullable: true),
                    QuoteCount = table.Column<long>(nullable: false),
                    ReplyCount = table.Column<long>(nullable: false),
                    RetweetCount = table.Column<long>(nullable: false),
                    FavoriteCount = table.Column<long>(nullable: false),
                    Lang = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetsRaw", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetsRaw_TweetsRaw_QuotedStatusId",
                        column: x => x.QuotedStatusId,
                        principalTable: "TweetsRaw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TweetsRaw_TweetsRaw_RetweetedStatusId",
                        column: x => x.RetweetedStatusId,
                        principalTable: "TweetsRaw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TweetsRaw_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    TweetRawId = table.Column<long>(nullable: false),
                    GeoCoordinates = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.TweetRawId);
                    table.ForeignKey(
                        name: "FK_Coordinates_TweetsRaw_TweetRawId",
                        column: x => x.TweetRawId,
                        principalTable: "TweetsRaw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtendedTweets",
                columns: table => new
                {
                    TweetRawId = table.Column<long>(nullable: false),
                    FullText = table.Column<string>(nullable: true),
                    DisplayTextRange = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendedTweets", x => x.TweetRawId);
                    table.ForeignKey(
                        name: "FK_ExtendedTweets_TweetsRaw_TweetRawId",
                        column: x => x.TweetRawId,
                        principalTable: "TweetsRaw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Attributes = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlaceType = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    TweetRawId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_TweetsRaw_TweetRawId",
                        column: x => x.TweetRawId,
                        principalTable: "TweetsRaw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoundingBoxes",
                columns: table => new
                {
                    PlaceId = table.Column<string>(nullable: false),
                    Coordinates = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoundingBoxes", x => x.PlaceId);
                    table.ForeignKey(
                        name: "FK_BoundingBoxes_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_TweetRawId",
                table: "Places",
                column: "TweetRawId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TweetsRaw_QuotedStatusId",
                table: "TweetsRaw",
                column: "QuotedStatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TweetsRaw_RetweetedStatusId",
                table: "TweetsRaw",
                column: "RetweetedStatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TweetsRaw_UserId",
                table: "TweetsRaw",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoundingBoxes");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "ExtendedTweets");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "TweetsRaw");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
