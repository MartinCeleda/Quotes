using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WB0110.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagQuote> TagQuotes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TagQuote>().HasOne(tg => tg.Tag).WithMany(t => t.Quotes).HasForeignKey(t => t.TagId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TagQuote>().HasKey(tg => new { tg.TagId, tg.QuoteId });
            modelBuilder.Entity<Quote>().HasData(new Quote { ID = 1, Text = "Komunismus je rovnoměrné rozdělení bídy"});
            modelBuilder.Entity<Tag>().HasData(new Tag { ID = 1, Category = Category.author, Name = "Třeba Churchil"});
        }
    }
}
