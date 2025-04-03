using AutoMapper;
using MovieRentalApp.Data.Interfaces;
using MovieRentalApp.Models.DTOs;
using MovieRentalApp.Models.Entities;
using MovieRentalApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentalApp.Service.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                return null;
            }
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateClientAsync(ClientCreateDto clientDto)
        {
            // Map DTO to entity
            var client = _mapper.Map<Client>(clientDto);

            // Set default values for rent dates
            if (clientDto.MovieId.HasValue)
            {
                client.RentDate = DateTime.Now;
                client.ReturnDate = DateTime.Now.AddDays(7); // Default return after 7 days
            }

            // Create client in repository
            var createdClient = await _clientRepository.CreateAsync(client);

            // Map back to DTO
            return _mapper.Map<ClientDto>(createdClient);
        }

        public async Task<ClientDto> UpdateClientAsync(int id, ClientUpdateDto clientDto)
        {
            // Get existing client
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                return null;
            }

            // Update properties
            _mapper.Map(clientDto, client);

            // Save changes
            var updatedClient = await _clientRepository.UpdateAsync(client);

            // Map back to DTO
            return _mapper.Map<ClientDto>(updatedClient);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            return await _clientRepository.DeleteAsync(id);
        }
    }
}