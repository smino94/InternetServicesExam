using Microsoft.EntityFrameworkCore;
using MovieRentalApp.Models.Entities;
using System;

namespace MovieRentalApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationship
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Clients)
                .HasForeignKey(c => c.MovieId);

            // Add sample data
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Name = "Inception",
                    Director = "Christopher Nolan",
                    Genre = "Sci-Fi",
                    Year = 2010,
                    Rating = 8.8,
                    BoxOfficeEarnings = 836800000m
                },
                new Movie
                {
                    Id = 2,
                    Name = "The Shawshank Redemption",
                    Director = "Frank Darabont",
                    Genre = "Drama",
                    Year = 1994,
                    Rating = 9.3,
                    BoxOfficeEarnings = 58300000m
                },
                new Movie
                {
                    Id = 3,
                    Name = "The Dark Knight",
                    Director = "Christopher Nolan",
                    Genre = "Action",
                    Year = 2008,
                    Rating = 9.0,
                    BoxOfficeEarnings = 1004600000m
                }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DOB = new DateTime(1985, 5, 15),
                    Address = "123 Main St, New York, NY",
                    MembershipCardNumber = "M12345",
                    MembershipCardValidityDate = new DateTime(2025, 12, 31),
                    MovieId = 1,
                    RentDate = new DateTime(2025, 3, 30),
                    ReturnDate = new DateTime(2025, 4, 6)
                },
                new Client
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DOB = new DateTime(1990, 8, 22),
                    Address = "456 Oak Ave, Los Angeles, CA",
                    MembershipCardNumber = "M67890",
                    MembershipCardValidityDate = new DateTime(2026, 5, 15)
                }
            );
        }
    }
}