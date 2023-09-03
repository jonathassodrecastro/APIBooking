using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using Service.Client;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepository _clientRepository;
        private readonly ClientServices _clientServices;

        public ClientController(IClientRepository clientRepository, ILogger<ClientController> logger, ClientServices clientServices)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _clientServices = clientServices;
        }

        /// <summary>
        /// Registers a new client.
        /// </summary>
        /// <param name="entityClient">The client information to be registered.</param>
        /// <returns>The registered client.</returns>
        [HttpPost("RegisterClient")]
        public async Task<IActionResult> RegisterClient(EntityClient entityClient)
        {
            try
            {
                var client = await _clientRepository.GetById(entityClient.Id);

                if (client == null)
                {
                    return Conflict("No client providade");
                }
                else
                {
                    client = await _clientServices.RegisterClient(entityClient);
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
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
                _logger.LogInformation($"Starting the GetClientById method. Searching for ID: {id}");

                var client = await _clientServices.GetEntityClient(id);

                if (client == null)
                {
                    _logger.LogWarning("Client not found.");
                    return BadRequest("No Client found. Id must be valid.");
                }

                _logger.LogInformation("Client found.");
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetReservationByID method.");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        /// <returns>A list of clients.</returns>
        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                _logger.LogInformation("Starting the GetAllClients method");

                var client = await _clientServices.GetAllClients();

                if (client == null)
                {
                    _logger.LogWarning("Client not found.");
                    return BadRequest("No Client found.");
                }

                _logger.LogInformation("Client found.");
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetReservationByID method.");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");

            }
        }


        /// <summary>
        /// Deletes a client by their ID.
        /// </summary>
        /// <param name="clientId">The ID of the client to be deleted.</param>
        /// <returns>A status message indicating the result of the deletion.</returns>
        [HttpDelete("DeleteClientById")]
        public async Task<IActionResult> DeleteClientById(int id)
        {
            try
            {
                _logger.LogInformation($"Starting the DeleteClientById method with ID: {id}");

                var client = await _clientServices.DeleteClient(id);

                if (id == 0)
                {
                    _logger.LogWarning("Client not found. No Id provided.");
                    return BadRequest("No Id provided.");
                }

                _logger.LogInformation($"Client deleted");
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during the execution of the DeleteClientById method for reservation with ID: {id}");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates the information of a client.
        /// </summary>
        /// <param name="id">The ID of the client to update.</param>
        /// <param name="updatedClient">The updated client information.</param>
        /// <returns>The updated client.</returns>
        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClient(int id, EntityClient client)
        {
            try
            {
                client = await _clientServices.GetEntityClient(id);

                if (client == null)
                {
                    _logger.LogError("No client found for this ID.");
                    return BadRequest("An internal error occurred.");
                }
                else
                {
                    client = await _clientServices.UpdateHouse(id, client);
                    return Ok("Client Updated!" + client);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the UpdateHouse method.");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
            }
        }

    }
}
