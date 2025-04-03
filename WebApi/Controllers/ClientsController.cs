using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentalApp.Models.DTOs;
using MovieRentalApp.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentalApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientDto>> GetClientById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientDto>> CreateClient([FromBody] ClientCreateDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdClient = await _clientService.CreateClientAsync(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientDto>> UpdateClient(int id, [FromBody] ClientUpdateDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedClient = await _clientService.UpdateClientAsync(id, clientDto);
            if (updatedClient == null)
            {
                return NotFound();
            }
            return Ok(updatedClient);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _clientService.DeleteClientAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}