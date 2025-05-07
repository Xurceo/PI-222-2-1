
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }

        private const string DatabasePath = "auction.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Subcategories)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lot>()
                .HasOne(l => l.Winner)
                .WithMany()
                .HasForeignKey(l => l.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lot>()
                .HasOne(l => l.Owner)
                .WithMany(u => u.Lots)
                .HasForeignKey(l => l.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lot>()
                .HasOne(l => l.Category)
                .WithMany(c => c.Lots)
                .HasForeignKey(l => l.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bid>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Lot)
                .WithMany(l => l.Bids)
                .HasForeignKey(b => b.LotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
