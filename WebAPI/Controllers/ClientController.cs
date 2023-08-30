using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Registers a new client.
        /// </summary>
        /// <param name="newClient">The client information to be registered.</param>
        /// <returns>The registered client.</returns>
        [HttpPost("RegisterClient")]
        public IActionResult Register(EntityClient entityClient)
        {
            if (entityClient == null)
            {
                return BadRequest("No value provided. All fields are mandatory.");
            }

            var missingFields = new List<string>
            {
                string.IsNullOrWhiteSpace(entityClient.name) ? "Name" : null,
                string.IsNullOrWhiteSpace(entityClient.lastname) ? "LastName" : null,
                string.IsNullOrWhiteSpace(entityClient.age.ToString()) ? "Age" : null,
                string.IsNullOrWhiteSpace(entityClient.phoneNumber) ? "Phone Number" : null


            }
                .Where(fieldName => fieldName != null)
                .ToList();

            if (missingFields.Any())
            {
                var errorMessage = "Required fields not filled: " + string.Join(", ", missingFields);
                return BadRequest(errorMessage);
            }

            _clientRepository.Insert(entityClient);

            return Ok($"Operation completed successfully. Client {entityClient.id} succesfully saved");
        }

        /// <summary>
        /// Retrieves a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client to retrieve.</param>
        /// <returns>The client with the specified ID.</returns>
        [HttpGet("FindClient")] 
        public async Task<IActionResult> GetClientById(int id)
        {
            if (id == 0) 
            {
                return BadRequest("Please fill in the required fields.");
            }

            var client = await _clientRepository.GetById(id);

            if (client == null)
            {
                return BadRequest("No Client found. Id must be valid.");
            }

            return Ok($"Client Found! - {client.id}: {client.name}");
        }

        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        /// <returns>A list of clients.</returns>
        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetClient()
        {
            var clients = await _clientRepository.GetAll();

            List<EntityClient> clientList = clients.ToList();

            return Ok(clientList);
        }

        /// <summary>
        /// Deletes a client by their ID.
        /// </summary>
        /// <param name="clientId">The ID of the client to be deleted.</param>
        /// <returns>A status message indicating the result of the deletion.</returns>
        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> DeleteClientById(int id) 
        {
            if (id == 0)
            {
                return BadRequest("Please fill in the required fields.");
            }

            var client = await _clientRepository.GetById(id);

            if (client == null)
            {
                return BadRequest("No Client found. Id must be valid.");
            }

            await _clientRepository.Delete(id);

            return Ok("Client Deleted!");
        }

        /// <summary>
        /// Updates the information of a client.
        /// </summary>
        /// <param name="id">The ID of the client to update.</param>
        /// <param name="updatedClient">The updated client information.</param>
        /// <returns>The updated client.</returns>
        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClient(int id, EntityClient entityClient)
        {
            if (entityClient == null)
            {
                return BadRequest("Client not found");
            }

            if (id != entityClient.id)
            {
                return BadRequest("Wrong client informed");
            }

            await _clientRepository.Update(id, entityClient);
            return Ok($"Client Updated! - {entityClient.id}: Name: {entityClient.name}, Last Name: {entityClient.lastname}, Age: {entityClient.age}, Phone Number: {entityClient.phoneNumber}");
        }
    }
}
