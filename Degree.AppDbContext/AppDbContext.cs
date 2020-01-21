using Degree.Models;
using Degree.Models.Dto;
using Degree.Models.Twitter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Degree.AppDbContext
{
    public class AppDbContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            if (optionsBuilder != null)
            {
                optionsBuilder.EnableSensitiveDataLogging(true); 
                optionsBuilder.UseMySql("Server=degree.mysql.database.azure.com; Port=3306; Database=degree; Uid=westley@degree; Pwd=Sentiment2019.; SslMode=Preferred;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<TweetRaw>().HasKey(x => x.Id);
            modelBuilder.Entity<TweetSentiment>().HasKey(x => x.TweetRawId);
            modelBuilder.Entity<TweetEntityRecognized>().HasKey(x => x.Id);
            modelBuilder.Entity<GeoUser>().HasKey(x => new { x.Id, x.UserId });
            modelBuilder.Entity<UserDto>().HasNoKey();

            modelBuilder.Entity<User>()
            .HasMany(x => x.GeoUsers)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TweetRaw>()
            .HasOne(e => e.TweetSentiment)
            .WithOne(e => e.TweetRaw)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TweetRaw>()
            .HasMany(e => e.TweetEntities)
            .WithOne(e => e.TweetRaw)
            .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<TweetRaw>()
            .HasOne(e => e.ExtendedTweet)
            .WithOne(e => e.TweetRaw);
                                

            modelBuilder.Entity<TweetRaw>()
            .HasOne(e => e.QuotedStatus)
            .WithOne();

            modelBuilder.Entity<TweetRaw>()
            .HasOne(e => e.RetweetedStatus)
            .WithOne();

            modelBuilder.Entity<TweetRaw>()
            .HasOne(x => x.ExtendedTweet)
            .WithOne(x => x.TweetRaw);

            modelBuilder.Entity<Place>()
            .HasOne(x => x.BoundingBox)
            .WithOne(x => x.Place);

            modelBuilder.Entity<TweetRaw>()
            .HasMany(x => x.TweetsHashtags)
            .WithOne(x => x.TweetRaw)
            .HasForeignKey(x => x.TweetRawId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TweetSentiment>()
            .HasMany(x => x.Sentences)
            .WithOne(x => x.TweetsSentiment)
            .HasForeignKey(x => x.TweetSentimentId)
            .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<BoundingBox>()
            .Property(e => e.Coordinates)
            .HasConversion(
                v => string.Join(";", v.First().First()),
                v => new List<List<List<double>>>() { new List<List<double>>() { new List<double>(Split(v).Select(x => double.Parse(x))) } }
            );
            modelBuilder.Entity<TweetSentenceSentiment>()
            .Property(e => e.Warnings)
            .HasConversion(
                v => string.Join(";", v),
                v => Split(v)
            );
            modelBuilder.Entity<TweetSentiment>()
            .Property(e => e.Sentiment)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<DocumentSentimentLabel>(v)
            );

            modelBuilder.Entity<TweetSentenceSentiment>()
            .Property(e => e.Sentiment)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<SentenceSentimentLabel>(v)
            );

            modelBuilder.Entity<ExtendedTweet>()
            .Property(e => e.DisplayTextRange)
            .HasConversion(
                v => string.Join(";", v),
                v => new List<long>(Split(v).Select(x => long.Parse(x)))
            );

        

            modelBuilder.Entity<Coordinates>()
            .Property(e => e.GeoCoordinates)
            .HasConversion(
                v => string.Join(";", v),
                v => new List<double>(Split(v).Select(x => double.Parse(x)))
            );
            

            base.OnModelCreating(modelBuilder);
        }
        private string[] Split(string s)
        {
            return s.Split(";") != null ? s.Split(";") : new string[] { "0"};
        }

        public DbSet<TweetRaw> TweetsRaw { get; set; }
        
        public DbSet<ExtendedTweet> ExtendedTweets { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<BoundingBox> BoundingBoxes { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TweetSentiment> TweetsSentiment { get; set; }
        public DbSet<TweetSentenceSentiment> SentenceSentiments { get; set; }
        public DbSet<TweetHashtags> TweetsHashtags { get; set; }
        public DbSet<GeoUser> GeoUsers { get; set; }
        public DbSet<TweetEntityRecognized> TweetEntityRecognizeds { get; set; }
        public DbSet<UserDto> UserDto { get; set; }

    }
}
