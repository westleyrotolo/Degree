using Degree.Models;
using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<TweetRaw>()
            .HasMany(e => e._Entities)
            .WithOne();

            
            modelBuilder.Entity<Entities>()
            .HasOne(e => e.ExtendedTweetRaw)
            .WithOne(e => e.ExtendedEntities)
            .HasForeignKey<TweetRaw>(x => x.ExtendedEntitiesId);


            modelBuilder.Entity<Entities>()
            .HasOne(e => e.TweetRaw)
            .WithOne(e => e.Entities)
            .HasForeignKey<TweetRaw>(x => x.EntitiesId);

    
            modelBuilder.Entity<TweetRaw>()
            .HasOne(e => e.ExtendedTweet)
            .WithOne(e => e.TweetRaw);

            modelBuilder.Entity<ExtendedTweet>()
            .HasOne(x => x.Entities);
            modelBuilder.Entity<BoundingBox>()
            .Property(e => e.Coordinates)
            .HasConversion(
                v => string.Join(";", v.First().First()),
                v => new List<List<List<double>>>() { new List<List<double>>() { new List<double>(Split(v).Select(x => double.Parse(x))) } }
            );

            modelBuilder.Entity<ExtendedTweet>()
            .Property(e => e.DisplayTextRange)
            .HasConversion(
                v => string.Join(";", v),
                v => new List<long>(Split(v).Select(x => long.Parse(x)))
            );

            modelBuilder.Entity<Url>()
            .Property(e => e.Indices)
            .HasConversion(
                v => string.Join(";",v),
                v => new List<long>(Split(v).Select(x => long.Parse(x)))
            );

            modelBuilder.Entity<UserMention>()
            .Property(e => e.Indices)
            .HasConversion(
                v => string.Join(";", v),
                v => new List<long>(Split(v).Select(x => long.Parse(x)))
            );

            modelBuilder.Entity<Hashtag>()
            .Property(e => e.Indices)
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
