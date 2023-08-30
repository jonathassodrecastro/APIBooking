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
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepository _clientRepository;
        public ClientController(IClientRepository clientRepository, ILogger<ClientController> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new client.
        /// </summary>
        /// <param name="newClient">The client information to be registered.</param>
        /// <returns>The registered client.</returns>
        [HttpPost("RegisterClient")]
        public async Task<IActionResult> Register(EntityClient entityClient)
        {
            try
            {
                _logger.LogInformation("Starting the Register method for a new client.");

                if (entityClient == null)
                {
                    _logger.LogWarning("No value provided for client registration.");
                    return BadRequest("No value provided. All fields are mandatory.");
                }

                var existingClient = await _clientRepository.GetById(entityClient.id);
                if (existingClient != null)
                {
                    _logger.LogWarning($"Client with ID {entityClient.id} already exists.");
                    return Conflict(new { message = $"Client with ID {entityClient.id} already exists." });
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
                    _logger.LogWarning(errorMessage);
                    return BadRequest(errorMessage);
                }

                _clientRepository.Insert(entityClient);

                _logger.LogInformation($"Client with ID: {entityClient.id} registered successfully.");
                return Ok($"Operation completed successfully. Client {entityClient.id} succesfully saved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the Register method.");
                return StatusCode(500, "An internal error occurred");
            }
        }


        /// <summary>
        /// Retrieves a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client to retrieve.</param>
        /// <returns>The client with the specified ID.</returns>
        [HttpGet("GetClientById")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                _logger.LogInformation($"Starting the GetClientById method for client ID: {id}");

                if (id == 0)
                {
                    _logger.LogWarning("No client ID provided for getting client information.");
                    return BadRequest("Please fill in the required fields.");
                }

                var client = await _clientRepository.GetById(id);

                if (client == null)
                {
                    _logger.LogWarning($"No client found with ID: {id}");
                    return BadRequest("No Client found. Id must be valid.");
                }

                _logger.LogInformation($"Client with ID: {id} found: {client.name}");
                return Ok($"Client Found! - {client.id}: {client.name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetClientById method.");
                return StatusCode(500, "An internal error occurred");
            }
        }


        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        /// <returns>A list of clients.</returns>
        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetClient()
        {
            try
            {
                _logger.LogInformation("Starting the GetClient method.");
                var clients = await _clientRepository.GetAll();
                List<EntityClient> clientList = clients.ToList();

                _logger.LogInformation($"Total clients retrieved: {clientList.Count}");
                return Ok(clientList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetClient method.");
                return StatusCode(500, "An internal error occurred");
            }
        }


        /// <summary>
        /// Deletes a client by their ID.
        /// </summary>
        /// <param name="clientId">The ID of the client to be deleted.</param>
        /// <returns>A status message indicating the result of the deletion.</returns>
        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> DeleteClientById(int id) 
        {
            try
            {
                _logger.LogInformation($"Starting the DeleteClientById method for client with ID {id}.");

                if (id == 0)
                {
                    _logger.LogWarning("Invalid client ID provided.");
                    return BadRequest("Please fill in the required fields.");
                }

                var client = await _clientRepository.GetById(id);

                if (client == null)
                {
                    _logger.LogWarning($"Client with ID {id} not found.");
                    return BadRequest("No Client found. Id must be valid.");
                }

                await _clientRepository.Delete(id);

                _logger.LogInformation($"Client with ID {id} successfully deleted.");

                return Ok("Client Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the DeleteClientById method.");
                return StatusCode(500, "An internal error occurred");
            }
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
            try
            {
                _logger.LogInformation($"Starting the UpdateClient method for client with ID {id}.");

                if (entityClient == null)
                {
                    _logger.LogWarning("Client not found.");
                    return BadRequest("Client not found");
                }

                if (id != entityClient.id)
                {
                    _logger.LogWarning($"Wrong client informed for update. Expected ID: {id}, Received ID: {entityClient.id}");
                    return BadRequest("Wrong client informed");
                }

                await _clientRepository.Update(id, entityClient);

                _logger.LogInformation($"Client with ID {id} successfully updated.");

                return Ok($"Client Updated! - {entityClient.id}: Name: {entityClient.name}, Last Name: {entityClient.lastname}, Age: {entityClient.age}, Phone Number: {entityClient.phoneNumber}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the UpdateClient method.");
                return StatusCode(500, "An internal error occurred");
            }
        }

    }
}
