using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseRepository _houseRepository;
        public HouseController(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        /// <summary>
        /// Registers a new house in the system.
        /// </summary>
        /// <param name="house">The details of the house to be registered.</param>
        /// <returns>The registered house information.</returns>
        [HttpPost("RegisterHouse")]
        public IActionResult RegisterHouse(EntityHouse entityhouse)
        {
            if (entityhouse == null)
            {
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
                return BadRequest(errorMessage);
            }

            if (entityhouse.available != 0 && entityhouse.available != 1)
            {
                return BadRequest("Invalid value for 'available' field. Only 0 or 1 are allowed.");
            }

            _houseRepository.Insert(entityhouse);

            return Ok($"Operation completed successfully. House {entityhouse.id} succesfully saved");
        }

        /// <summary>
        /// Retrieves information about a specific house by its ID.
        /// </summary>
        /// <param name="houseId">The ID of the house to retrieve.</param>
        /// <returns>The house information.</returns>
        [HttpGet("FindHouse")]
        public async Task<IActionResult> GetHouseById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please fill in the required fields.");
            }

            var house = await _houseRepository.GetById(id);

            if (house == null)
            {
                return BadRequest("No House found. Id must be valid.");
            }

            string availability = (house.available == 0) ? "Available" : "Not available";

            return Ok($"House Found! - {house.id}: {availability}!");
        }

        /// <summary>
        /// Retrieves a list of all available houses.
        /// </summary>
        /// <returns>A list of house information.</returns>
        [HttpGet("GetAllHouses")]
        public async Task<IActionResult> GetHouses()
         {
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

            return Ok(responseList);
        }

        /// <summary>
        /// Deletes a house by its ID.
        /// </summary>
        /// <param name="houseId">The ID of the house to be deleted.</param>
        /// <returns>A status message indicating the result of the deletion.</returns>
        [HttpDelete("DeleteHouse")]
        public async Task<IActionResult> DeleteHouseById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please fill in the required fields.");
            }

            var client = await _houseRepository.GetById(id);

            if (client == null)
            {
                return BadRequest("No House found. Id must be valid.");
            }

            await _houseRepository.Delete(id);

            return Ok("House Deleted!");
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
            if (entityhouse == null)
            {
                return BadRequest("House not found");
            }

            if (id != entityhouse.id)
            {
                return BadRequest("Wrong house informed");
            }

            await _houseRepository.Update(id, entityhouse);
            return Ok("House Updated!" + entityhouse);
        }
    }
}
