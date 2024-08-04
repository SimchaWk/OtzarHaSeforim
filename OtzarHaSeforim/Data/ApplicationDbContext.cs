// Ignore Spelling: Seforim Otzar

using Microsoft.EntityFrameworkCore;
using OtzarHaSeforim.Models;
using System;

namespace OtzarHaSeforim.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<LibraryModel> Libraries { get; set; }

        public DbSet<ShelfModel> Shelves { get; set; }

        public DbSet<SetBooksModel> Sets { get; set; } 

        public DbSet<BookModel> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryModel>()
                .HasMany(library => library.Shelves)
                .WithOne(shelf => shelf.LibraryParent)
                .HasForeignKey(shelf => shelf.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShelfModel>()
                .HasMany(shelf => shelf.SetBooks)
                .WithOne(setBook => setBook.ShelfParent)
                .HasForeignKey(setBook => setBook.ShelfId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SetBooksModel>()
                .HasMany(setBook => setBook.Books)
                .WithOne(book => book.SetBooksParent)
                .HasForeignKey(book => book.SetBooksId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
