using MovieRentalApp.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentalApp.Data.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> CreateAsync(Movie movie);
        Task<Movie> UpdateAsync(Movie movie);
        Task<bool> DeleteAsync(int id);
    }
}