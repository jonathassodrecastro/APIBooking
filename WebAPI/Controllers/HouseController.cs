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

        [HttpPost("RegisterHouse")]
        public IActionResult Register(EntityHouse entityhouse)
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
                return BadRequest("No Client found. Id must be valid.");
            }

            string availability = (house.available == 0) ? "Available" : "Not available";

            return Ok($"House Found! - {house.id}: {availability}!");
        }

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
