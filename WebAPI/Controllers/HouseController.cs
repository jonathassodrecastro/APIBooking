using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using Polly;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly ILogger<HouseController> _logger;
        private readonly IHouseRepository _houseRepository;

        const int maxRetryAttempts = 3;
        const int retryDelayMilliseconds = 1000;
        const int timeoutMilliseconds = 5000; // 5 seconds

        public HouseController(IHouseRepository houseRepository, ILogger<HouseController> logger)
        {
            _houseRepository = houseRepository;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new house in the system.
        /// </summary>
        /// <param name="house">The details of the house to be registered.</param>
        /// <returns>The registered house information.</returns>
        [HttpPost("RegisterHouse")]
        public IActionResult RegisterHouse(EntityHouse entityhouse)
        {
            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(maxRetryAttempts, attempt => TimeSpan.FromMilliseconds(retryDelayMilliseconds),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Retrying RegisterReservation after {timeSpan.TotalMilliseconds}ms. Retry attempt: {retryCount}");
                    });
            try
            {
                _logger.LogInformation("Starting the RegisterHouse method.");

                if (entityhouse == null)
                {
                    _logger.LogWarning("No value provided. All fields are mandatory.");
                    return BadRequest("No value provided. All fields are mandatory.");
                }

                var missingFields = new List<string>
                {
                    string.IsNullOrWhiteSpace(entityhouse.id.ToString()) ? "House ID" : null,
                    string.IsNullOrWhiteSpace(entityhouse.available.ToString()) ? "Availability" : null,
                }
                    .Where(fieldName => fieldName != null)
                    .ToList();

                if (missingFields.Any())
                {
                    var errorMessage = "Required fields not filled: " + string.Join(", ", missingFields);
                    _logger.LogWarning(errorMessage);
                    return BadRequest(errorMessage);
                }

                if (entityhouse.available != 0 && entityhouse.available != 1)
                {
                    _logger.LogWarning("Invalid value for 'available' field. Only 0 or 1 are allowed.");
                    return BadRequest("Invalid value for 'available' field. Only 0 or 1 are allowed.");
                }

                _houseRepository.Insert(entityhouse);

                _logger.LogInformation($"House {entityhouse.id} successfully saved.");

                return Ok($"Operation completed successfully. House {entityhouse.id} successfully saved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the RegisterHouse method.");
                return StatusCode(500, "An error occurred while processing the request.");
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
            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(maxRetryAttempts, attempt => TimeSpan.FromMilliseconds(retryDelayMilliseconds),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Retrying RegisterReservation after {timeSpan.TotalMilliseconds}ms. Retry attempt: {retryCount}");
                    });
            try
            {
                _logger.LogInformation("Starting the GetHouseById method.");

                if (id == 0)
                {
                    _logger.LogWarning("Please fill in the required fields.");
                    return BadRequest("Please fill in the required fields.");
                }

                var house = await _houseRepository.GetById(id);

                if (house == null)
                {
                    _logger.LogWarning("No House found. Id must be valid.");
                    return BadRequest("No House found. Id must be valid.");
                }

                string availability = (house.available == 0) ? "Available" : "Not available";

                _logger.LogInformation($"House {house.id} found: {availability}");

                return Ok($"House Found! - {house.id}: {availability}!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the GetHouseById method.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        /// <summary>
        /// Retrieves a list of all available houses.
        /// </summary>
        /// <returns>A list of house information.</returns>
        [HttpGet("GetAllHouses")]
        public async Task<IActionResult> GetHouses()
        {
            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(maxRetryAttempts, attempt => TimeSpan.FromMilliseconds(retryDelayMilliseconds),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Retrying RegisterReservation after {timeSpan.TotalMilliseconds}ms. Retry attempt: {retryCount}");
                    });

            try
            {
                _logger.LogInformation("Starting the GetHouses method.");

                var houses = await _houseRepository.GetAll();

                List<EntityHouse> houseList = houses.ToList();

                List<object> responseList = new List<object>();

                foreach (var house in houseList)
                {
                    string availability = (house.available == 0) ? "Available" : "Not available";

                    var houseWithAvailability = new
                    {
                        house.id,
                        Availability = availability
                    };

                    responseList.Add(houseWithAvailability);
                }

                _logger.LogInformation("Houses retrieved successfully.");

                return Ok(responseList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the GetHouses method.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        /// <summary>
        /// Deletes a house by its ID.
        /// </summary>
        /// <param name="houseId">The ID of the house to be deleted.</param>
        /// <returns>A status message indicating the result of the deletion.</returns>
        [HttpDelete("DeleteHouse")]
        public async Task<IActionResult> DeleteHouseById(int id)
        {
            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(maxRetryAttempts, attempt => TimeSpan.FromMilliseconds(retryDelayMilliseconds),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Retrying RegisterReservation after {timeSpan.TotalMilliseconds}ms. Retry attempt: {retryCount}");
                    });

            try
            {
                _logger.LogInformation($"Starting the DeleteHouseById method for House ID {id}.");

                if (id == 0)
                {
                    return BadRequest("Please fill in the required fields.");
                }

                var house = await _houseRepository.GetById(id);

                if (house == null)
                {
                    return BadRequest("No House found. Id must be valid.");
                }

                await _houseRepository.Delete(id);

                _logger.LogInformation($"House ID {id} deleted successfully.");

                return Ok("House Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the DeleteHouseById method.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        /// <summary>
        /// Updates information for a specific house.
        /// </summary>
        /// <param name="houseId">The ID of the house to be updated.</param>
        /// <param name="updatedHouse">The updated information for the house.</param>
        /// <returns>The updated house information.</returns>
        [HttpPut("UpdateHouse")]
        public async Task<IActionResult> UpdateHouse(int id, EntityHouse entityhouse)
        {
            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(maxRetryAttempts, attempt => TimeSpan.FromMilliseconds(retryDelayMilliseconds),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Retrying RegisterReservation after {timeSpan.TotalMilliseconds}ms. Retry attempt: {retryCount}");
                    });

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
