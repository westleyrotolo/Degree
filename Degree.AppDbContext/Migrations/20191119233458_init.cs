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
                name: "Tweets",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tweet_created_at = table.Column<DateTime>(nullable: false),
                    tweet_id = table.Column<long>(nullable: false),
                    tweet_id_str = table.Column<long>(nullable: false),
                    tweet_text = table.Column<string>(nullable: true),
                    tweet_source = table.Column<string>(nullable: true),
                    tweet_truncated = table.Column<long>(nullable: false),
                    tweet_in_reply_to_status_id = table.Column<string>(nullable: true),
                    tweet_in_reply_to_user_id = table.Column<string>(nullable: true),
                    tweet_in_reply_to_screen_name = table.Column<string>(nullable: true),
                    tweet_place_country = table.Column<string>(nullable: true),
                    tweet_place_full_name = table.Column<string>(nullable: true),
                    tweet_retweet_count = table.Column<int>(nullable: false),
                    tweet_favorite_count = table.Column<int>(nullable: false),
                    tweet_favorited = table.Column<int>(nullable: false),
                    tweet_retweeted = table.Column<int>(nullable: false),
                    tweet_lang = table.Column<string>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    user_id_str = table.Column<long>(nullable: false),
                    user_name = table.Column<string>(nullable: true),
                    user_screen_name = table.Column<string>(nullable: true),
                    user_location = table.Column<string>(nullable: true),
                    user_description = table.Column<string>(nullable: true),
                    user_protected = table.Column<int>(nullable: false),
                    user_verified = table.Column<int>(nullable: false),
                    user_followers_count = table.Column<int>(nullable: false),
                    user_friends_count = table.Column<int>(nullable: false),
                    user_listed_count = table.Column<int>(nullable: false),
                    user_favourites_count = table.Column<int>(nullable: false),
                    user_statuses_count = table.Column<int>(nullable: false),
                    user_created_at = table.Column<string>(nullable: true),
                    user_geo_enabled = table.Column<int>(nullable: false),
                    user_lang = table.Column<string>(nullable: true),
                    tweet_sentiment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweets", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tweets");
        }
    }
}
