using APIBooking.Domain.Exceptions;
using APIBooking.Domain.Extensions;
using APIBooking.Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Reservation;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly ILogger<ReservationController> _logger;
        private readonly ReservationServices _reservationServices;


        public ReservationController(ILogger<ReservationController> logger, ReservationServices reservationServices)
        {
            _logger = logger;
            _reservationServices = reservationServices;
        }


        /// <summary>
        /// Registers a new reservation.
        /// </summary>
        /// <param name="entityReservation">The reservation details to be registered.</param>
        /// <returns>Returns OK if the reservation was successfully registered, or BadRequest if an error occurred.</returns>
        /// <response code="200">Return the created reservation</response>
        /// <response code="400"> Request error. Return Error</response>
        /// <response code="409">No house found with ID</response>
        [HttpPost]
        public async Task<IActionResult> RegisterReservation(UpdateReservationRequest request)
        {
            try
            {
                var reservation = await _reservationServices.RegisterReservation(request);
                return Ok(reservation.ToResponse());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the UpdateReservation method.");
                return StatusCode(500, "An internal error occurred.");
            }
        }


        /// <summary>
        /// Gets a reservation by its ID.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to retrieve.</param>
        /// <response code="200">Return the reservation</response>
        /// <response code="400"> Request error. Return Error</response>
        /// <response code="409">No reservation found with ID</response>
        /// <returns>Returns the reservation if found, or NotFound if the reservation does not exist.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationByID([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"Starting the GetReservationByID method. Searching for ID: {id}");

                var reservation = await _reservationServices.GetEntityReservation(id);

                _logger.LogInformation("Reservation found.");
                return Ok(reservation.ToResponse());
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
        /// <response code="200">Return the reservation list</response>
        /// <response code="400"> Request error. Return Error</response>
        /// <response code="409">No reservations found</response>
        /// <returns>Returns a list of all reservations.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                _logger.LogInformation("Starting the GetAllReservations method");

                var reservation = await _reservationServices.GetAllReservations();
                return Ok(reservation.Select(x => x.ToResponse()));
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
        /// <param name="id">The ID of the reservation to be deleted.</param>
        /// <response code="200">Return OK</response>
        /// <response code="400"> Request error. Return Error</response>
        /// <returns>Returns OK if the reservation was successfully deleted, NotFound if the reservation was not found, or BadRequest if an error occurred.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationById([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"Starting the DeleteClientById method with ID: {id}");

                var reservation = await _reservationServices.DeleteReservation(id);

                _logger.LogInformation("Reservation deleted.");
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during the execution of the DeleteClientById method for reservation with ID: {id}");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates the information of an existing reservation.
        /// </summary>
        /// <param name="id">The ID of the reservation to be updated.</param>
        /// <param name="request"></param>
        /// <response code="200">Return the reservation updated</response>
        /// <response code="400"> Request error. Return Error</response>
        /// <response code="409">No reservation found</response>
        /// <returns>Returns OK if the reservation was successfully updated, Conflict if the reservation was not found, or BadRequest if an error occurred.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation([FromRoute] int id, UpdateReservationRequest request)
        {
            try
            {
                var reservation = await _reservationServices.UpdateReservation(id, request);
                return Ok(reservation.ToResponse());
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the UpdateReservation method.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

    }
}
