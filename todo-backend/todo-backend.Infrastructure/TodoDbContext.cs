using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_backend.Domain.Models;

namespace todo_backend.Infrastructure
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<HistoryItem> HistoryItems { get; set; }
        public DbSet<Board> Boards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.HasMany(c => c.Cards)
                    .WithOne(o => o.Catalog)
                    .HasForeignKey(c => c.CatalogId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasMany(b => b.Catalogs)
                    .WithOne(c => c.Board)
                    .HasForeignKey(c => c.BoardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HistoryItem>(entity =>
            {
                entity.HasOne(h => h.Card)
                  .WithMany(c => c.HistoryItems)
                 .HasForeignKey(h => h.CardId)
                 .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(h => h.Board)
                 .WithMany(b => b.HistoryItems)
                 .HasForeignKey(h => h.BoardId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

        }

    }
}
