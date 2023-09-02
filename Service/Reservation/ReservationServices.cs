using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Azure.Core;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Collections;
using APIBooking.Domain.Models.Request.Reservations;
using APIBooking.Domain.Exceptions;

namespace Service.Reservation
{
    public class ReservationServices
    {
        
        private readonly IReservationRepository _reservationRepository;
        private readonly IHouseRepository _house;
        private readonly ILogger<ReservationServices> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationServices(IReservationRepository reservationRepository, ILogger<ReservationServices> logger, IHouseRepository house, IHttpClientFactory httpClientFactory)
        {
            _reservationRepository = reservationRepository;
            _house = house;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public Task<EntityReservation> GetEntityReservation(int id)
        {
            var reservation = _reservationRepository.GetById(id);
            return reservation;
        }

        public async Task<EntityReservation> RegisterReservation(EntityReservation reservation) 
        {
                
                await _reservationRepository.Insert(reservation);
                return reservation;
        }

        public async Task<IEnumerable<EntityReservation>> GetAllReservations()
        {
            var reservationList = await _reservationRepository.GetAll();
            return reservationList.OrderBy(reservation => reservation.id);
        }

        public async Task<IEnumerable<EntityReservation>> DeleteReservation(int id)
        {
            await _reservationRepository.Delete(id);
            var reservationList = await _reservationRepository.GetAll();
            return reservationList.OrderBy(reservation => reservation.id);
        }

        public async Task<EntityReservation> UpdateReservation(int id, UpdateReservationRequest reservation) 
        {
            var reservartionDB = await _reservationRepository.GetById(id);

            if (reservartionDB == null)
            {
                _logger.LogError("No reservation found for this ID.");
                throw new NotFoundException("Reservation Not found");
            }
            
            reservartionDB.Update(reservation);
            await _reservationRepository.Update(reservartionDB.id, reservartionDB);
            return reservartionDB;

        }
        //private async Task<HttpResponseMessage> CallDiscountApiAsync(EntityReservation entityReservation, ILogger logger)
        //{


        //    HttpResponseMessage response = null;

        //        // Create API Discount request object
        //        var discountRequest = new
        //        {
        //            userId = entityReservation.clientId.ToString(),
        //            houseId = entityReservation.houseId.ToString(),
        //            discountCode = entityReservation.discountCode
        //        };

        //        // Serialize object
        //        var discountRequestBody = JsonSerializer.Serialize(discountRequest);


        //        // Send request to API Discount
        //        var client = _httpClientFactory.CreateClient();
        //        try
        //        {
        //            response = await client.PostAsync("https://sbv2bumkomidlxwffpgbh4k6jm0ydskh.lambda-url.us-east-1.on.aws/",
        //                new StringContent(discountRequestBody, Encoding.UTF8, "application/json"));
        //        }
        //        catch (HttpRequestException ex)
        //        {
        //            logger.LogError($"API call failed with exception: {ex}");
        //        }


        //    return response;
        //}

    }
}
