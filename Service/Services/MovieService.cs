using AutoMapper;
using MovieRentalApp.Data.Interfaces;
using MovieRentalApp.Models.DTOs;
using MovieRentalApp.Models.Entities;
using MovieRentalApp.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentalApp.Service.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return null;
            }
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto)
        {
            // Map DTO to entity
            var movie = _mapper.Map<Movie>(movieDto);

            // Create movie in repository
            var createdMovie = await _movieRepository.CreateAsync(movie);

            // Map back to DTO
            return _mapper.Map<MovieDto>(createdMovie);
        }

        public async Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto movieDto)
        {
            // Get existing movie
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return null;
            }

            // Update properties
            _mapper.Map(movieDto, movie);

            // Save changes
            var updatedMovie = await _movieRepository.UpdateAsync(movie);

            // Map back to DTO
            return _mapper.Map<MovieDto>(updatedMovie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            return await _movieRepository.DeleteAsync(id);
        }
    }
}