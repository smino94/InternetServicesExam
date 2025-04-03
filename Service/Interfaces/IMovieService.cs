using MovieRentalApp.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentalApp.Service.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task<MovieDto> GetMovieByIdAsync(int id);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto);
        Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto movieDto);
        Task<bool> DeleteMovieAsync(int id);
    }
}