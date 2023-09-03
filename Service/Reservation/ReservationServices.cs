using APIBooking.Domain.Entities;
using APIBooking.Domain.Exceptions;
using APIBooking.Domain.Models.Requests;
using Microsoft.Extensions.Logging;
using Repositories.Interface;
using System.Text;
using System.Text.Json;

namespace Service.Reservation
{
    public class ReservationServices
    {

        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<ReservationServices> _logger;
        private readonly IHouseRepository _houseRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationServices(IReservationRepository reservationRepository, ILogger<ReservationServices> logger, IHouseRepository houseRepository, IHttpClientFactory httpClientFactory)
        {
            _reservationRepository = reservationRepository;
            _logger = logger;
            _houseRepository = houseRepository;
            _httpClientFactory = httpClientFactory;
        }

        public Task<EntityReservation> GetEntityReservation(int Id)
        {
            var reservation = _reservationRepository.GetById(Id);
            if (reservation == null)
            {
                _logger.LogError("No Reservation found for this Id.");
                throw new NotFoundException("Reservation not found. Id must be valid.");
            }
            return reservation;
        }

        public async Task<EntityReservation> RegisterReservation(UpdateReservationRequest reservationRequest)
        {
            //Checking the House ID.
            var house = await _houseRepository.GetById(reservationRequest.HouseId);
            var reservation = new EntityReservation();

            //Validating if the House exists.
            if (house == null)
            {
                _logger.LogError("No House found for this Id.");
                throw new NotFoundException("House Not found");
            }

            reservation.Update(reservationRequest);

            var discountResponse = CallDiscountApiAsync(reservation);

            if (discountResponse.Result == true)
            {
                try 
                {
                    await _reservationRepository.Insert(reservation);
                    return reservation;
                }
                catch 
                {
                    throw new SQLErrorException("Execute query error");
                }  
            }
            else
            {
                throw new DomainException("Discount API returned a non-success status.");
            }
        }

        public async Task<IEnumerable<EntityReservation>> GetAllReservations()
        {
            var reservationList = await _reservationRepository.GetAll();
            if (reservationList == null)
            {
                _logger.LogError("No reservation found.");
                throw new NotFoundException("Reservation Not found");
            }
            return reservationList.OrderBy(reservation => reservation.Id);
        }

        public async Task<IEnumerable<EntityReservation>> DeleteReservation(int Id)
        {
            await _reservationRepository.Delete(Id);
            var reservationList = await _reservationRepository.GetAll();
            return reservationList.OrderBy(reservation => reservation.Id);
        }

        public async Task<EntityReservation> UpdateReservation(int Id, UpdateReservationRequest reservation)
        {
            var reservartionDB = await _reservationRepository.GetById(Id);

            if (reservartionDB == null)
            {
                _logger.LogError("No reservation found for this Id.");
                throw new NotFoundException("Reservation Not found");
            }

            reservartionDB.Update(reservation);
            await _reservationRepository.Update(reservartionDB.Id, reservartionDB);
            return reservartionDB;
        }

        private async Task<bool> CallDiscountApiAsync(EntityReservation reservation)
        {
            // Create API Discount request object
            var discountRequest = new
            {
                userId = reservation.ClientId.ToString(),
                houseId = reservation.HouseId.ToString(),
                discountCode = reservation.DiscountCode
            };

            // Serialize object
            var discountRequestBody = JsonSerializer.Serialize(discountRequest);

            // Send request to API Discount
            var client = _httpClientFactory.CreateClient("DiscountCode");

            try
            {
                HttpResponseMessage response = await client.PostAsync("https://sbv2bumkomidlxwffpgbh4k6jm0ydskh.lambda-url.us-east-1.on.aws/",
                                               new StringContent(discountRequestBody, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException)
            {
                throw new DomainException(discountRequestBody);
            }
        }

    }
}
