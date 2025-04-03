using AutoMapper;
using Moq;
using MovieRentalApp.Data.Interfaces;
using MovieRentalApp.Models.DTOs;
using MovieRentalApp.Models.Entities;
using MovieRentalApp.Service.Mapping;
using MovieRentalApp.Service.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MovieRentalApp.Tests
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly IMapper _mapper;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();

            // Configure AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });
            _mapper = mapperConfig.CreateMapper();

            // Create service with mocked repository
            _movieService = new MovieService(_mockMovieRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetAllMoviesAsync_ShouldReturnAllMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
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
                }
            };

            _mockMovieRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(movies);

            // Act
            var result = await _movieService.GetAllMoviesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, m => m.Id == 1 && m.Name == "Inception");
            Assert.Contains(result, m => m.Id == 2 && m.Name == "The Shawshank Redemption");
        }

        [Fact]
        public async Task GetMovieByIdAsync_WithValidId_ShouldReturnMovie()
        {
            // Arrange
            var movieId = 1;
            var movie = new Movie
            {
                Id = movieId,
                Name = "Inception",
                Director = "Christopher Nolan",
                Genre = "Sci-Fi",
                Year = 2010,
                Rating = 8.8,
                BoxOfficeEarnings = 836800000m
            };

            _mockMovieRepository.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(movie);

            // Act
            var result = await _movieService.GetMovieByIdAsync(movieId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result.Id);
            Assert.Equal("Inception", result.Name);
            Assert.Equal("Christopher Nolan", result.Director);
        }

        [Fact]
        public async Task CreateMovieAsync_ShouldReturnCreatedMovie()
        {
            // Arrange
            var movieCreateDto = new MovieCreateDto
            {
                Name = "The Dark Knight",
                Director = "Christopher Nolan",
                Genre = "Action",
                Year = 2008,
                Rating = 9.0,
                BoxOfficeEarnings = 1004600000m
            };

            _mockMovieRepository.Setup(repo => repo.CreateAsync(It.IsAny<Movie>()))
                .ReturnsAsync((Movie movie) =>
                {
                    movie.Id = 3; // Simulate ID assignment by database
                    return movie;
                });

            // Act
            var result = await _movieService.CreateMovieAsync(movieCreateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Id);
            Assert.Equal("The Dark Knight", result.Name);
            Assert.Equal("Christopher Nolan", result.Director);
            Assert.Equal(9.0, result.Rating);
        }

        [Fact]
        public async Task UpdateMovieAsync_WithValidId_ShouldReturnUpdatedMovie()
        {
            // Arrange
            var movieId = 1;
            var existingMovie = new Movie
            {
                Id = movieId,
                Name = "Inception",
                Director = "Christopher Nolan",
                Genre = "Sci-Fi",
                Year = 2010,
                Rating = 8.8,
                BoxOfficeEarnings = 836800000m
            };

            var movieUpdateDto = new MovieUpdateDto
            {
                Name = "Inception",
                Director = "Christopher Nolan",
                Genre = "Sci-Fi/Thriller", // Changed genre
                Year = 2010,
                Rating = 9.0, // Updated rating
                BoxOfficeEarnings = 836800000m
            };

            _mockMovieRepository.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(existingMovie);

            _mockMovieRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Movie>()))
                .ReturnsAsync((Movie movie) => movie);

            // Act
            var result = await _movieService.UpdateMovieAsync(movieId, movieUpdateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result.Id);
            Assert.Equal("Inception", result.Name);
            Assert.Equal("Sci-Fi/Thriller", result.Genre); // Should be updated
            Assert.Equal(9.0, result.Rating); // Should be updated
        }
    }
}