using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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
        private EntityHouse _house;

        public ReservationController(IReservationRepository reservationRepository, IHouseRepository houseRepository, IClientRepository clientRepository, IHttpClientFactory httpClientFactory)
        {
            _reservationRepository = reservationRepository;
            _houseRepository = houseRepository;
            _clientRepository = clientRepository;
            _httpClientFactory = httpClientFactory;
        }


        /// <summary>
        /// Registers a new reservation.
        /// </summary>
        /// <param name="entityReservation">The reservation details to be registered.</param>
        /// <returns>Returns OK if the reservation was successfully registered, or BadRequest if an error occurred.</returns>
        [HttpPost("RegisterReservation")]
        public async Task<IActionResult> RegisterReservation (EntityReservation entityReservation)
        {
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
                return BadRequest(errorMessage);
            }

            _house = await _houseRepository.GetById(entityReservation.houseId);

            if (_house == null)
            {
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

            // Verificar a resposta da API Discount
            if (response.IsSuccessStatusCode)
            {
                // A API Discount retornou com sucesso, continue com a lógica de reserva
                await _reservationRepository.Insert(entityReservation);
                return Ok($"Operation completed successfully. Reservation {entityReservation.id} succesfully saved");
            }
            else
            {
                // A API Discount retornou um erro, retorne uma resposta de erro
                return BadRequest("Invalid Discount");
            }

           

        }

        /// <summary>
        /// Finds a reservation by its ID.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to retrieve.</param>
        /// <returns>Returns the reservation if found, or NotFound if the reservation does not exist.</returns>
        [HttpGet("FindReservationByID")]
        public async Task<IActionResult> GetReservationByID(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please fill in the required fields.");
            }

            var reservation = await _reservationRepository.GetById(id);
            if (reservation == null) { return BadRequest("No Reservation found. Id must be valid."); }

            return Ok($"Reservation {reservation.id} found! - {reservation.clientName}. Start Date: {reservation.startDate}. End Date: {reservation.endDate}. House ID: {reservation.houseId}");
        }

        /// <summary>
        /// Retrieves all reservations.
        /// </summary>
        /// <returns>Returns a list of all reservations.</returns>
        [HttpGet("GetAllReservations")]
        public async Task<IActionResult> GetClient()
        {
            var reservations = await _reservationRepository.GetAll();

            List<EntityReservation> reservationList = reservations.ToList();

            if (!reservationList.Any())
            {
                return NotFound();
            }

            return Ok(reservationList);
        }

        /// <summary>
        /// Deletes a reservation based on the provided reservation ID.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to be deleted.</param>
        /// <returns>Returns OK if the reservation was successfully deleted, NotFound if the reservation was not found, or BadRequest if an error occurred.</returns>
        [HttpDelete("DeleteReservation")]
        public async Task<IActionResult> DeleteClientById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please fill in the required fields.");
            }

            var reservation = await _reservationRepository.GetById(id);

            if (reservation == null)
            {
                return BadRequest("No Reservation found. Id must be valid.");
            }

            await _reservationRepository.Delete(id);

            return Ok("Reservation Deleted!");
        }

        /// <summary>
        /// Updates the information of an existing reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to be updated.</param>
        /// <param name="updatedReservation">The updated reservation data.</param>
        /// <returns>Returns OK if the reservation was successfully updated, NotFound if the reservation was not found, or BadRequest if an error occurred.</returns>
        [HttpPut("UpdateReservation")]
        public async Task<IActionResult> UpdateClient(int id, EntityReservation entityReservation)
        {
            if (entityReservation == null)
            {
                return BadRequest("Reservation not found");
            }

            if (id != entityReservation.id)
            {
                return BadRequest("Wrong client informed");
            }

            await _reservationRepository.Update(id, entityReservation);
            return Ok($"Reservation {entityReservation.id} Updated!: {entityReservation}");
        }
    }
}
