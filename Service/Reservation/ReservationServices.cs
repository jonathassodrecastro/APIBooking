using APIBooking.Domain.Entities;
using Microsoft.Extensions.Logging;
using Repositories.Interface;
using APIBooking.Domain.Exceptions;
using APIBooking.Domain.Models.Requests;
using Repositories.Repository;

namespace Service.Reservation
{
    public class ReservationServices
    {

        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<ReservationServices> _logger;
        private readonly IHouseRepository _houseRepository;

   

        public ReservationServices(IReservationRepository reservationRepository,
                                   ILogger<ReservationServices> logger,
                                   IHouseRepository houseRepository)
        {
            _reservationRepository = reservationRepository;
            _logger = logger;
            _houseRepository = houseRepository;

        }

        public Task<EntityReservation> GetEntityReservation(int Id)
        {
            var reservation = _reservationRepository.GetById(Id);
            return reservation;
        }

        public async Task<EntityReservation> RegisterReservation(EntityReservation reservation)
        {

            //Checking the House ID.
            var House = await _houseRepository.GetById(reservation.HouseId);

            //Validating if the House exists.
            if (House == null)
            {
                _logger.LogError("No House found for this Id.");
                throw new NotFoundException("House Not found");
            }

            await _reservationRepository.Insert(reservation);
            return reservation;
        }

        public async Task<IEnumerable<EntityReservation>> GetAllReservations()
        {
            var reservationList = await _reservationRepository.GetAll();
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
