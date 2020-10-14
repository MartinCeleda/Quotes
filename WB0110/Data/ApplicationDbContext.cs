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
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 1, Text = "Komunismus je rovnoměrné rozdělení bídy"});
            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 1, Category = Category.author, Name = "Třeba Churchil"});

            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 2, Text = "Z hlediska vyššího principu mravního vražda na tyranu není zločinem." });
            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 2, Category = Category.author, Name = "Jiří Krejčík" });

            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 3, Text = "Historie je lež, na náž jsme se shodli" });
            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 3, Category = Category.author, Name = "Voltaire" });

            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 4, Text = "Smažme husitskou válku z naší historie, i zhasne sláva českého národa. Ta jediná doba váží více než ostatní naše dějství, ba více, než celé věky čínské říše." });
            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 4, Category = Category.author, Name = "Karel Havlíček Borovský" });

            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 5, Text = "Falšovatelé historie svobodu národa nezachraňují, ale ohrožují." });
            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 5, Category = Category.author, Name = "Václav Havel" });


            modelBuilder.Entity<TagQuote>().HasKey(tq => new { tq.TagId, tq.QuoteId });
            modelBuilder.Entity<TagQuote>().HasOne(tq => tq.Tag).WithMany(t => t.TagQuotes).HasForeignKey(t => t.TagId);
            modelBuilder.Entity<TagQuote>().HasOne(tq => tq.Quote).WithMany(q => q.TagQuotes).HasForeignKey(q => q.QuoteId);
        }
    }
}
