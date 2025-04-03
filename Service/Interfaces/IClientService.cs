using MovieRentalApp.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentalApp.Service.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<ClientDto> CreateClientAsync(ClientCreateDto clientDto);
        Task<ClientDto> UpdateClientAsync(int id, ClientUpdateDto clientDto);
        Task<bool> DeleteClientAsync(int id);
    }
}