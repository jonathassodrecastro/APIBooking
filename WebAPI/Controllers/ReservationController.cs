using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using Repositories.Interface;
using Service.Reservation;
using System.Net;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ReservationController> _logger;
        private readonly ReservationServices _reservationServices;
        //private readonly IRetryPolicyProvider _retryPolicyProvider;

        const int maxRetryAttempts = 3;
        const int retryDelayMilliseconds = 1000;
        const int timeoutMilliseconds = 5000; // 5 seconds


        public ReservationController(IReservationRepository reservationRepository, IHouseRepository houseRepository, IHttpClientFactory httpClientFactory, ILogger<ReservationController> logger, ReservationServices reservationServices)
        {
            _reservationRepository = reservationRepository;
            _houseRepository = houseRepository;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _reservationServices = reservationServices;
        }


        /// <summary>
        /// Registers a new reservation.
        /// </summary>
        /// <param name="entityReservation">The reservation details to be registered.</param>
        /// <returns>Returns OK if the reservation was successfully registered, or BadRequest if an error occurred.</returns>
        [HttpPost("RegisterReservation")]
        public async Task<IActionResult> RegisterReservation(EntityReservation entityReservation)
        {

            try 
            {
                var house = await _houseRepository.GetById(entityReservation.houseId);

                if (house == null)
                {
                    return Conflict($"No house found with ID: {entityReservation.houseId}");
                }
                else
                {
                    var reservation = await _reservationServices.RegisterReservation(entityReservation);
                    return Ok(reservation);

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
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

                EntityReservation reservation = await _reservationServices.GetEntityReservation(id);
                
                if (reservation == null)
                {
                    _logger.LogWarning("Reservation not found.");
                    return BadRequest("No Reservation found. Id must be valid.");
                }

                _logger.LogInformation("Reservation found.");
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetReservationByID method.");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves all reservations.
        /// </summary>
        /// <returns>Returns a list of all reservations.</returns>
        [HttpGet("GetAllReservations")]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                _logger.LogInformation("Starting the GetAllReservations method");

                var reservation = await _reservationServices.GetAllReservations();

                if (reservation == null)
                {
                    _logger.LogWarning("Reservations not found.");
                    return BadRequest("No Reservations found.");
                }

                _logger.LogInformation("Reservations found.");
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetReservationByID method.");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");

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
                _logger.LogInformation($"Starting the DeleteClientById method with ID: {id}");

                var reservation = await _reservationServices.DeleteReservation(id);

                if (id == 0)
                {
                    _logger.LogWarning("Reservation not found. No Id provided.");
                    return BadRequest("No Id provided.");
                }

                _logger.LogInformation("Reservations found.");
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during the execution of the DeleteClientById method for reservation with ID: {id}");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates the information of an existing reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to be updated.</param>
        /// <param name="updatedReservation">The updated reservation data.</param>
        /// <returns>Returns OK if the reservation was successfully updated, NotFound if the reservation was not found, or BadRequest if an error occurred.</returns>
        [HttpPut("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation(int id, EntityReservation reservartion)
        {
            try
            {
                reservartion = await _reservationServices.GetEntityReservation(id);

                if (reservartion == null)
                {
                    _logger.LogError("No reservation found for this ID.");
                    return BadRequest("An internal error occurred.");
                }
                else
                {
                    reservartion = await _reservationServices.UpdateReservation(id, reservartion);
                    return Ok("Reservation Updated!" + reservartion);
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
