using APIBooking.Domain.Entities;
using APIBooking.Domain.Models.Requests;
using Microsoft.Extensions.Logging;
using Moq; // To create Mock objects
using Repositories.Interface;
using Service.Reservation; 

namespace APIBooking.Tests
{
    public class ReservationServiceTests
    {
        
        [Fact]
        public async Task UpdateReservation_ValidId_ReturnsUpdatedReservation()
        {
            // Arrange
            int reservationId = 1;
            var reservationToUpdate = new UpdateReservationRequest
            {
                ClientId = 7654321,
                ClientName = "Roberta",
                ClientLastName = "Souza",
                ClientAge = 25,
                ClientPhoneNumber = "+5571998985421",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                HouseId = 2345678,
                DiscountCode = "XYZ98765"
            };

            var mockLogger = new Mock<ILogger<ReservationServices>>();
            var mockReservationRepository = new Mock<IReservationRepository>();
            var mockHouse = new Mock<IHouseRepository>();
            var mockHttpClient = new Mock<IHttpClientFactory>();

            // Simulate the behavior of GetById in the repository
            var existingReservation = new EntityReservation
            {
                Id = reservationId,
                ClientId = 1234567,
                ClientName = "Gonzalo",
                ClientLastName = "Castro",
                ClientAge = 30,
                ClientPhoneNumber = "+123456789012",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                HouseId = 14521,
                DiscountCode = "XYZ98765"
            };
            mockReservationRepository.Setup(repo => repo.GetById(reservationId)).ReturnsAsync(existingReservation);

            // Create an instance of the reservation service to be tested
            var reservationService = new ReservationServices(mockLogger.Object, mockReservationRepository.Object, mockHouse.Object, mockHttpClient.Object);

            // Act
            var updatedReservation = await reservationService.UpdateReservation(reservationId, reservationToUpdate);

            // Assert
            // Verify if the GetById method was called with the correct ID
            mockReservationRepository.Verify(repo => repo.GetById(reservationId), Times.Once);

            // Verify if the Update method was called with the updated reservation
            mockReservationRepository.Verify(repo => repo.Update(existingReservation.Id, existingReservation), Times.Once);

            // Verify if the result is an updated reservation
            Assert.Equal(reservationToUpdate.ClientId, updatedReservation.ClientId);
            Assert.Equal(reservationToUpdate.ClientName, updatedReservation.ClientName);
            Assert.Equal(reservationToUpdate.ClientLastName, updatedReservation.ClientLastName);
            Assert.Equal(reservationToUpdate.ClientAge, updatedReservation.ClientAge);
            Assert.Equal(reservationToUpdate.ClientPhoneNumber, updatedReservation.ClientPhoneNumber);
            Assert.Equal(reservationToUpdate.StartDate, updatedReservation.StartDate);
            Assert.Equal(reservationToUpdate.EndDate, updatedReservation.EndDate);
            Assert.Equal(reservationToUpdate.HouseId, updatedReservation.HouseId);
            Assert.Equal(reservationToUpdate.DiscountCode, updatedReservation.DiscountCode);
        }
    }
}