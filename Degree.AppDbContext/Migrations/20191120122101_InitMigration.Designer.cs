﻿// <auto-generated />
using System;
using Degree.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Degree.AppDbContext.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191120122101_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Degree.Models.TweetRaw", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("tweet_created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("tweet_favorite_count")
                        .HasColumnType("int");

                    b.Property<int>("tweet_favorited")
                        .HasColumnType("int");

                    b.Property<long>("tweet_id")
                        .HasColumnType("bigint");

                    b.Property<long>("tweet_id_str")
                        .HasColumnType("bigint");

                    b.Property<string>("tweet_in_reply_to_screen_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tweet_in_reply_to_status_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tweet_in_reply_to_user_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tweet_lang")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tweet_place_country")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tweet_place_full_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("tweet_retweet_count")
                        .HasColumnType("int");

                    b.Property<int>("tweet_retweeted")
                        .HasColumnType("int");

                    b.Property<string>("tweet_sentiment")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tweet_source")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tweet_text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("tweet_truncated")
                        .HasColumnType("bigint");

                    b.Property<string>("user_created_at")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("user_description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("user_favourites_count")
                        .HasColumnType("int");

                    b.Property<int>("user_followers_count")
                        .HasColumnType("int");

                    b.Property<int>("user_friends_count")
                        .HasColumnType("int");

                    b.Property<int>("user_geo_enabled")
                        .HasColumnType("int");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.Property<long>("user_id_str")
                        .HasColumnType("bigint");

                    b.Property<string>("user_lang")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("user_listed_count")
                        .HasColumnType("int");

                    b.Property<string>("user_location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("user_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("user_protected")
                        .HasColumnType("int");

                    b.Property<string>("user_screen_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("user_statuses_count")
                        .HasColumnType("int");

                    b.Property<int>("user_verified")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Tweets");
                });
#pragma warning restore 612, 618
        }
    }
}