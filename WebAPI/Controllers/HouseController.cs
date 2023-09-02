using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using Polly;
using Service.Reservation;
using Service.House;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly ILogger<HouseController> _logger;
        private readonly IHouseRepository _houseRepository;
        private readonly HouseServices _houseServices;

        public HouseController(IHouseRepository houseRepository, ILogger<HouseController> logger, HouseServices houseServices)
        {
            _houseRepository = houseRepository;
            _logger = logger;
            _houseServices = houseServices;
        }

        /// <summary>
        /// Registers a new house in the system.
        /// </summary>
        /// <param name="house">The details of the house to be registered.</param>
        /// <returns>The registered house information.</returns>
        [HttpPost("RegisterHouse")]
        public async Task<IActionResult> RegisterHouse(EntityHouse entityhouse)
        {
            try
            {
                var house = await _houseRepository.GetById(entityhouse.id);

                if (house == null)
                {
                    return Conflict($"No house found with ID: {entityhouse.id}");
                }
                else
                {
                    house = await _houseServices.RegisterHouse(entityhouse);
                    return Ok(house);

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
            }
        }


        /// <summary>
        /// Retrieves information about a specific house by its ID.
        /// </summary>
        /// <param name="houseId">The ID of the house to retrieve.</param>
        /// <returns>The house information.</returns>
        [HttpGet("GetHouseById")]
        public async Task<IActionResult> GetHouseById(int id)
        {
            try
            {
                _logger.LogInformation($"Starting the GetHouseById method. Searching for ID: {id}");

               var house = await _houseServices.GetEntityHouse(id);

                if (house == null)
                {
                    _logger.LogWarning("House not found.");
                    return BadRequest("No House found. Id must be valid.");
                }

                _logger.LogInformation("House found.");
                return Ok(house);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetReservationByID method.");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
            }
        }

        /// <summary>
        /// Retrieves a list of all available houses.
        /// </summary>
        /// <returns>A list of house information.</returns>
        [HttpGet("GetAllHouses")]
        public async Task<IActionResult> GetAllHouses()
        {
            try
            {
                _logger.LogInformation("Starting the GetAllHouses method");

                var house = await _houseServices.GetAllHouses();

                if (house == null)
                {
                    _logger.LogWarning("House not found.");
                    return BadRequest("No House found.");
                }

                _logger.LogInformation("House found.");
                return Ok(house);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the execution of the GetReservationByID method.");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");

            }
        }


        /// <summary>
        /// Deletes a house by its ID.
        /// </summary>
        /// <param name="houseId">The ID of the house to be deleted.</param>
        /// <returns>A status message indicating the result of the deletion.</returns>
        [HttpDelete("DeleteHouseById")]
        public async Task<IActionResult> DeleteHouseById(int id)
        {
            try
            {
                _logger.LogInformation($"Starting the DeleteHouseById method with ID: {id}");

                var house = await _houseServices.DeleteHouse(id);

                if (id == 0)
                {
                    _logger.LogWarning("House not found. No Id provided.");
                    return BadRequest("No Id provided.");
                }

                _logger.LogInformation("House deleted.");
                return Ok(house);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during the execution of the DeleteClientById method for reservation with ID: {id}");
                return StatusCode(500, ex.InnerException?.Message ?? "An internal error occurred.");
            }
        }


        /// <summary>
        /// Updates information for a specific house.
        /// </summary>
        /// <param name="houseId">The ID of the house to be updated.</param>
        /// <param name="updatedHouse">The updated information for the house.</param>
        /// <returns>The updated house information.</returns>
        [HttpPut("UpdateHouse")]
        public async Task<IActionResult> UpdateHouse(int id, EntityHouse house)
        {
            try
            {
                house = await _houseServices.GetEntityHouse(id);

                if (house == null)
                {
                    _logger.LogError("No house found for this ID.");
                    return BadRequest("An internal error occurred.");
                }
                else
                {
                    house = await _houseServices.UpdateHouse(id, house);
                    return Ok("House Updated!" + house);
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
