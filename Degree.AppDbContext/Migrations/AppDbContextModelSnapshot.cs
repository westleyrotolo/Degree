﻿// <auto-generated />
using System;
using Degree.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Degree.AppDbContext.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Degree.Models.BoundingBox", b =>
                {
                    b.Property<string>("PlaceId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Coordinates")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("PlaceId");

                    b.ToTable("BoundingBoxes");
                });

            modelBuilder.Entity("Degree.Models.Coordinates", b =>
                {
                    b.Property<long>("TweetRawId")
                        .HasColumnType("bigint");

                    b.Property<string>("GeoCoordinates")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("TweetRawId");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("Degree.Models.Entities", b =>
                {
                    b.Property<Guid>("EntitieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("ExtendedTweetRawId")
                        .HasColumnType("bigint");

                    b.Property<long>("TweetRawId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TweetRawId2")
                        .HasColumnType("bigint");

                    b.HasKey("EntitieId");

                    b.HasIndex("TweetRawId2");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("Degree.Models.ExtendedTweet", b =>
                {
                    b.Property<long>("TweetRawId")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayTextRange")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("EntitiesEntitieId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ExtendedEntitiesEntitieId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FullText")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("TweetRawId");

                    b.HasIndex("EntitiesEntitieId");

                    b.HasIndex("ExtendedEntitiesEntitieId");

                    b.ToTable("ExtendedTweets");
                });

            modelBuilder.Entity("Degree.Models.Hashtag", b =>
                {
                    b.Property<Guid>("EntitiesId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Indices")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("EntitiesId");

                    b.ToTable("Hashtags");
                });

            modelBuilder.Entity("Degree.Models.Media", b =>
                {
                    b.Property<Guid>("EntitiesId")
                        .HasColumnType("char(36)");

                    b.Property<string>("DisplayUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ExtendedUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MediaUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("EntitiesId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Degree.Models.Place", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Attributes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Country")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CountryCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PlaceType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("TweetRawId")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("TweetRawId")
                        .IsUnique();

                    b.ToTable("Places");
                });

            modelBuilder.Entity("Degree.Models.TweetRaw", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("EntitiesId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ExtendedEntitiesId")
                        .HasColumnType("char(36)");

                    b.Property<long>("FavoriteCount")
                        .HasColumnType("bigint");

                    b.Property<string>("InReplyToScreenName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long?>("InReplyToStatusId")
                        .HasColumnType("bigint");

                    b.Property<long?>("InReplyToUserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsQuoteStatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Lang")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("QuoteCount")
                        .HasColumnType("bigint");

                    b.Property<long?>("QuotedStatusId")
                        .HasColumnType("bigint");

                    b.Property<long>("ReplyCount")
                        .HasColumnType("bigint");

                    b.Property<long>("RetweetCount")
                        .HasColumnType("bigint");

                    b.Property<string>("Source")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Truncated")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EntitiesId")
                        .IsUnique();

                    b.HasIndex("ExtendedEntitiesId")
                        .IsUnique();

                    b.HasIndex("QuotedStatusId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("TweetsRaw");
                });

            modelBuilder.Entity("Degree.Models.Url", b =>
                {
                    b.Property<Guid>("EntitiesId")
                        .HasColumnType("char(36)");

                    b.Property<string>("DisplayUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ExpandedUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Indices")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TweetUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("EntitiesId");

                    b.ToTable("Urls");
                });

            modelBuilder.Entity("Degree.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("DefaultProfile")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Followers")
                        .HasColumnType("int");

                    b.Property<int>("Following")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProfileBanner")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ScreenName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Statuses")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Verified")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("default_profile_image")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Degree.Models.UserMention", b =>
                {
                    b.Property<Guid>("EntitiesId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Indices")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ScreenName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("EntitiesId");

                    b.ToTable("UserMentions");
                });

            modelBuilder.Entity("Degree.Models.BoundingBox", b =>
                {
                    b.HasOne("Degree.Models.Place", "Place")
                        .WithOne("BoundingBox")
                        .HasForeignKey("Degree.Models.BoundingBox", "PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Degree.Models.Coordinates", b =>
                {
                    b.HasOne("Degree.Models.TweetRaw", "TweetRaw")
                        .WithOne("Coordinates")
                        .HasForeignKey("Degree.Models.Coordinates", "TweetRawId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Degree.Models.Entities", b =>
                {
                    b.HasOne("Degree.Models.TweetRaw", null)
                        .WithMany("_Entities")
                        .HasForeignKey("TweetRawId2");
                });

            modelBuilder.Entity("Degree.Models.ExtendedTweet", b =>
                {
                    b.HasOne("Degree.Models.Entities", "Entities")
                        .WithMany()
                        .HasForeignKey("EntitiesEntitieId");

                    b.HasOne("Degree.Models.Entities", "ExtendedEntities")
                        .WithMany()
                        .HasForeignKey("ExtendedEntitiesEntitieId");

                    b.HasOne("Degree.Models.TweetRaw", "TweetRaw")
                        .WithOne("ExtendedTweet")
                        .HasForeignKey("Degree.Models.ExtendedTweet", "TweetRawId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Degree.Models.Hashtag", b =>
                {
                    b.HasOne("Degree.Models.Entities", "Entities")
                        .WithMany("Hashtags")
                        .HasForeignKey("EntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Degree.Models.Media", b =>
                {
                    b.HasOne("Degree.Models.Entities", "Entities")
                        .WithMany("Media")
                        .HasForeignKey("EntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Degree.Models.Place", b =>
                {
                    b.HasOne("Degree.Models.TweetRaw", "TweetRaw")
                        .WithOne("Place")
                        .HasForeignKey("Degree.Models.Place", "TweetRawId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Degree.Models.TweetRaw", b =>
                {
                    b.HasOne("Degree.Models.Entities", "Entities")
                        .WithOne("TweetRaw")
                        .HasForeignKey("Degree.Models.TweetRaw", "EntitiesId");

                    b.HasOne("Degree.Models.Entities", "ExtendedEntities")
                        .WithOne("ExtendedTweetRaw")
                        .HasForeignKey("Degree.Models.TweetRaw", "ExtendedEntitiesId");

                    b.HasOne("Degree.Models.TweetRaw", "QuotedStatus")
                        .WithOne("RetweetedStatus")
                        .HasForeignKey("Degree.Models.TweetRaw", "QuotedStatusId");

                    b.HasOne("Degree.Models.User", "User")
                        .WithMany("TweetRaws")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Degree.Models.Url", b =>
                {
                    b.HasOne("Degree.Models.Entities", "Entities")
                        .WithMany("Urls")
                        .HasForeignKey("EntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Degree.Models.UserMention", b =>
                {
                    b.HasOne("Degree.Models.Entities", "Entities")
                        .WithMany("UserMentions")
                        .HasForeignKey("EntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
