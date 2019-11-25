using Degree.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Degree.AppDbContext
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder != null)
            {
                optionsBuilder.UseMySql("Server=degree.mysql.database.azure.com; Port=3306; Database=degree; Uid=westley@degree; Pwd=Sentiment2019.; SslMode=Preferred;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TweetRaw>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TweetRaw> TweetsRaw { get; set; }
        public DbSet<ExtendedTweet> ExtendedTweets { get; set; }
        public DbSet<Entities> Entities { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<UserMention> UserMentions {get;set;}
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<BoundingBox> BoundingBoxes { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
