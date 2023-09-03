using APIBooking.Domain.Entities;
using Repositories.Interface;


namespace Service.House
{
    public class HouseServices
    {
        
        private readonly IHouseRepository _houserepository;


        public HouseServices(IHouseRepository houserepository)
        {
            
            _houserepository = houserepository;

        }

        public Task<EntityHouse> GetEntityHouse(int id)
        {
            var house = _houserepository.GetById(id);
            return house;
        }

        public async Task<EntityHouse> RegisterHouse(EntityHouse house)
        {

            await _houserepository.Insert(house);
            return house;
        }

        public async Task<IEnumerable<EntityHouse>> GetAllHouses()
        {
            var houseList = await _houserepository.GetAll();
            return houseList.OrderBy(house => house.Id);
        }

        public async Task<IEnumerable<EntityHouse>> DeleteHouse(int id)
        {
            await _houserepository.Delete(id);
            var houseList = await _houserepository.GetAll();
            return houseList.OrderBy(house => house.Id);
        }

        public async Task<EntityHouse> UpdateHouse(int id, EntityHouse house)
        {
            await _houserepository.Update(id, house);
            return house;
        }
    }
}
