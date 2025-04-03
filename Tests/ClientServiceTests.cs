using AutoMapper;
using Moq;
using MovieRentalApp.Data.Interfaces;
using MovieRentalApp.Models.DTOs;
using MovieRentalApp.Models.Entities;
using MovieRentalApp.Service.Mapping;
using MovieRentalApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MovieRentalApp.Tests
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly IMapper _mapper;
        private readonly ClientService _clientService;

        public ClientServiceTests()
        {
            _mockClientRepository = new Mock<IClientRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });
            _mapper = mapperConfig.CreateMapper();

            _clientService = new ClientService(_mockClientRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetAllClientsAsync_ShouldReturnAllClients()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    Id = 1,
                    FirstName = "Slave",
                    LastName = "Manoski",
                    DOB = new DateTime(1985, 5, 15),
                    Address = "Berovo, Macedonia",
                    MembershipCardNumber = "M12345",
                    MembershipCardValidityDate = new DateTime(2025, 12, 31),
                    MovieId = 1
                },
                new Client
                {
                    Id = 2,
                    FirstName = "Ivona",
                    LastName = "Gjorgievska",
                    DOB = new DateTime(1990, 8, 22),
                    Address = "Strumica, Macedonia",
                    MembershipCardNumber = "M67890",
                    MembershipCardValidityDate = new DateTime(2026, 5, 15)
                }
            };

            _mockClientRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(clients);

            var result = await _clientService.GetAllClientsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Id == 1);
            Assert.Contains(result, c => c.Id == 2);
        }

        [Fact]
        public async Task GetClientByIdAsync_WithValidId_ShouldReturnClient()
        {
            var clientId = 1;
            var client = new Client
            {
                Id = clientId,
                FirstName = "Slavisha",
                LastName = "Trajkoski",
                DOB = new DateTime(1985, 5, 15),
                Address = "Skopje, MKD",
                MembershipCardNumber = "M12345",
                MembershipCardValidityDate = new DateTime(2025, 12, 31)
            };

            _mockClientRepository.Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync(client);

            var result = await _clientService.GetClientByIdAsync(clientId);

            Assert.NotNull(result);
            Assert.Equal(clientId, result.Id);
            Assert.Equal("Slavisha", result.FirstName);
            Assert.Equal("Trajkoski", result.LastName);
        }

        [Fact]
        public async Task CreateClientAsync_ShouldReturnCreatedClient()
        {
            var clientCreateDto = new ClientCreateDto
            {
                FirstName = "New",
                LastName = "Client",
                DOB = new DateTime(1995, 10, 25),
                Address = "ASNOM blv",
                MembershipCardNumber = "M54321",
                MembershipCardValidityDate = new DateTime(2026, 8, 31),
                MovieId = 2
            };

            _mockClientRepository.Setup(repo => repo.CreateAsync(It.IsAny<Client>()))
                .ReturnsAsync((Client client) =>
                {
                    client.Id = 3; // Simulate ID assignment by database
                    return client;
                });

            var result = await _clientService.CreateClientAsync(clientCreateDto);

            Assert.NotNull(result);
            Assert.Equal(3, result.Id);
            Assert.Equal("New", result.FirstName);
            Assert.Equal("Client", result.LastName);
            Assert.Equal(2, result.MovieId);
            Assert.NotNull(result.RentDate); // Should have set a rent date since MovieId was provided
        }

        [Fact]
        public async Task UpdateClientAsync_WithValidId_ShouldReturnUpdatedClient()
        {
            var clientId = 1;
            var existingClient = new Client
            {
                Id = clientId,
                FirstName = "Slavomir",
                LastName = "Mino",
                DOB = new DateTime(1985, 5, 15),
                Address = "Partizanska",
                MembershipCardNumber = "M12345",
                MembershipCardValidityDate = new DateTime(2025, 12, 31)
            };

            var clientUpdateDto = new ClientUpdateDto
            {
                FirstName = "Slavkomir",
                LastName = "Tralevski", // Changed last name
                DOB = new DateTime(1985, 5, 15),
                Address = "Ruzveltova 3", // Changed address
                MembershipCardNumber = "M12345",
                MembershipCardValidityDate = new DateTime(2025, 12, 31),
                MovieId = 3 // Added movie rental
            };

            _mockClientRepository.Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync(existingClient);

            _mockClientRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Client>()))
                .ReturnsAsync((Client client) => client);

            var result = await _clientService.UpdateClientAsync(clientId, clientUpdateDto);

            Assert.NotNull(result);
            Assert.Equal(clientId, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Tralevski", result.LastName); // Should be updated
            Assert.Equal("Ruzveltova 3", result.Address); // Should be updated
            Assert.Equal(3, result.MovieId); // Should be updated
        }
    }
}