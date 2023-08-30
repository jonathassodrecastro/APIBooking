﻿using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using Repositories.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using APIBooking.Data.Context;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger _logger;
        private readonly IHouseRepository _houseRepository;
        private EntityHouse _house;

        public ReservationController(IReservationRepository reservationRepository, IHouseRepository houseRepository)
        {
            _reservationRepository = reservationRepository;
            _houseRepository = houseRepository;
        }



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



            await _reservationRepository.Insert(entityReservation);
            return Ok($"Operation completed successfully. Reservation {entityReservation.id} succesfully saved");

        }

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

        [HttpGet("GetAllReservations")]
        public async Task<IActionResult> GetClient()
        {
            var reservations = await _reservationRepository.GetAll();

            List<EntityReservation> reservationList = reservations.ToList();

            return Ok(reservationList);
        }

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