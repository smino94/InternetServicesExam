using MovieRentalApp.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentalApp.Data.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task<bool> DeleteAsync(int id);
    }
}