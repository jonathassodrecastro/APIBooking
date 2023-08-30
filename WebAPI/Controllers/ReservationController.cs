using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Interface;
using System.Text;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ReservationController> _logger;
        private EntityHouse _house;

        public ReservationController(IReservationRepository reservationRepository, IHouseRepository houseRepository, IClientRepository clientRepository, IHttpClientFactory httpClientFactory, EntityHouse house, ILogger<ReservationController> logger)
        {
            _reservationRepository = reservationRepository;
            _houseRepository = houseRepository;
            _clientRepository = clientRepository;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _house = house;
        }


        /// <summary>
        /// Registers a new reservation.
        /// </summary>
        /// <param name="entityReservation">The reservation details to be registered.</param>
        /// <returns>Returns OK if the reservation was successfully registered, or BadRequest if an error occurred.</returns>
        [HttpPost("RegisterReservation")]
        public async Task<IActionResult> RegisterReservation (EntityReservation entityReservation)
        {
            try 
            {
                _logger.LogInformation("Starting the RegisterReservation method.");

                var missingFields = new List<string>
                {
                    string.IsNullOrWhiteSpace(entityReservation.clientId.ToString()) ? "Client ID" : null,
                    string.IsNullOrWhiteSpace(entityReservation.clientName.ToString()) ? "Client Name" : null,
                    string.IsNullOrWhiteSpace(entityReservation.clientLastname.ToString()) ? "Client Last Name" : null,
                    string.IsNullOrWhiteSpace(entityReservation.clientAge.ToString()) ? "Client Age" : null,
                    string.IsNullOrWhiteSpace(entityReservation.clientPhoneNumber.ToString()) ? "Client Phone Number" : null,
                    string.IsNullOrWhiteSpace(entityReservation.startDate.ToString()) ? "Start Date" : null,
                    string.IsNullOrWhiteSpace(entityReservation.endDate.ToString()) ? "End Date" : null,
                    string.IsNullOrWhiteSpace(entityReservation.houseId.ToString()) ? "House ID" : null,
                    string.IsNullOrWhiteSpace(entityReservation.discountCode.ToString()) ? "Discount Code" : null,
                }
                .Where(fieldName => fieldName != null)
                .ToList();

                if (missingFields.Any())
                {
                    var errorMessage = "Required fields not filled: " + string.Join(", ", missingFields);
                    _logger.LogWarning(errorMessage);
                    return BadRequest(errorMessage);
                }

                _logger.LogInformation("Fetching information for house with ID {HouseId}.", entityReservation.houseId);
                _house = await _houseRepository.GetById(entityReservation.houseId);

                if (_house == null)
                {
                    _logger.LogWarning("House with ID {HouseId} not found.", entityReservation.houseId);
                    return BadRequest($"House with ID {entityReservation.houseId} not found.");
                }

                entityReservation.startDate = entityReservation.startDate.Date; // Apenas a parte da data, sem a hora
                entityReservation.endDate = entityReservation.endDate.Date; // Apenas a parte da data, sem a hora

                // Create API Discount request objetct
                var discountRequest = new
                {
                    userId = entityReservation.clientId.ToString(),
                    houseId = entityReservation.houseId.ToString(),
                    discountCode = entityReservation.discountCode
                };

                //Serialize object
                var discountRequestBody = JsonSerializer.Serialize(discountRequest);

                // Send request to API Discount
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync("https://sbv2bumkomidlxwffpgbh4k6jm0ydskh.lambda-url.us-east-1.on.aws/", new StringContent(discountRequestBody, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    await _reservationRepository.Insert(entityReservation);
                    _logger.LogInformation("Reservation {ReservationId} successfully saved.", entityReservation.id);
                    return Ok($"Operation completed successfully. Reservation {entityReservation.id} saved successfully");
                }
                else
                {
                    _logger.LogWarning("Invalid discount for reservation {ReservationId}.", entityReservation.id);
                    return StatusCode(401, "Invalid discount");
                }
            } 
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred during the execution of the RegisterReservation method.");
                return StatusCode(500, "An internal error occurred");
            }  
        }

        /// <summary>
        /// Gets a reservation by its ID.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to retrieve.</param>
        /// <returns>Returns the reservation if found, or NotFound if the reservation does not exist.</returns>
        [HttpGet("GetReservationByID")]
        public async Task<IActionResult> GetReservationByID(int id)
        {
            try 
            {
                _logger.LogInformation($"Starting the GetReservationByID method. Searching for ID: {id}");
                if (id == 0)
                {
                    _logger.LogWarning("No Id provided.");
                    return BadRequest("Please fill in the required fields.");
                }

                var reservation = await _reservationRepository.GetById(id);
                if (reservation == null) 
                {
                    _logger.LogWarning("Reservation with ID {ReservationId} not found.", id);
                    return BadRequest("No Reservation found. Id must be valid."); 
                }
                _logger.LogInformation("Reservation found.");
                return Ok($"Reservation {reservation.id} found! - {reservation.clientName}. Start Date: {reservation.startDate}. End Date: {reservation.endDate}. House ID: {reservation.houseId}");
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetReservationByID method.");
                return StatusCode(500, "An internal error occurred");
            }
        }

        /// <summary>
        /// Retrieves all reservations.
        /// </summary>
        /// <returns>Returns a list of all reservations.</returns>
        [HttpGet("GetAllReservations")]
        public async Task<IActionResult> GetClient()
        {
            try
            {
                _logger.LogInformation("Starting the GetClient method.");
                var reservations = await _reservationRepository.GetAll();
                List<EntityReservation> reservationList = reservations.ToList();

                if (!reservationList.Any())
                {
                    _logger.LogWarning("No reservations found in the database.");
                    return NotFound();
                }

                _logger.LogInformation("Returning a list of reservations.");
                return Ok(reservationList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetClient method.");
                return StatusCode(500, "An internal error occurred");
            }
        }

        /// <summary>
        /// Deletes a reservation based on the provided reservation ID.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to be deleted.</param>
        /// <returns>Returns OK if the reservation was successfully deleted, NotFound if the reservation was not found, or BadRequest if an error occurred.</returns>
        [HttpDelete("DeleteReservation")]
        public async Task<IActionResult> DeleteClientById(int id)
        {
            try
            {
                _logger.LogInformation($"Starting the DeleteClientById method for reservation with ID: {id}");

                if (id == 0)
                {
                    _logger.LogWarning("Invalid ID provided in the request.");
                    return BadRequest("Please fill in the required fields.");
                }

                var reservation = await _reservationRepository.GetById(id);
                if (reservation == null)
                {
                    _logger.LogWarning($"No reservation found with ID: {id}.");
                    return BadRequest("No Reservation found. Id must be valid.");
                }

                await _reservationRepository.Delete(id);

                _logger.LogInformation($"Reservation with ID: {id} deleted successfully.");
                return Ok("Reservation Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during the execution of the DeleteClientById method for reservation with ID: {id}");
                return StatusCode(500, "An internal error occurred");
            }
        }

        /// <summary>
        /// Updates the information of an existing reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to be updated.</param>
        /// <param name="updatedReservation">The updated reservation data.</param>
        /// <returns>Returns OK if the reservation was successfully updated, NotFound if the reservation was not found, or BadRequest if an error occurred.</returns>
        [HttpPut("UpdateReservation")]
        public async Task<IActionResult> UpdateHouse(int id, EntityHouse entityhouse)
        {
            try
            {
                _logger.LogInformation($"Starting the UpdateHouse method for House ID {id}.");

                if (entityhouse == null)
                {
                    return BadRequest("House not found");
                }

                if (id != entityhouse.id)
                {
                    return BadRequest("Wrong house informed");
                }

                await _houseRepository.Update(id, entityhouse);

                _logger.LogInformation($"House ID {id} updated successfully.");

                return Ok("House Updated!" + entityhouse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the UpdateHouse method.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

    }
}
