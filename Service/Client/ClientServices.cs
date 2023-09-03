using APIBooking.Domain.Entities;
using Microsoft.Extensions.Logging;
using Repositories.Interface;


namespace Service.Client
{
    public class ClientServices
    {

        private readonly IClientRepository _clientrepository;
        private readonly ILogger<ClientServices> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientServices(ILogger<ClientServices> logger, IClientRepository clientrepository, IHttpClientFactory httpClientFactory)
        {

            _clientrepository = clientrepository;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public Task<EntityClient> GetEntityClient(int id)
        {
            var client = _clientrepository.GetById(id);
            return client;
        }

        public async Task<EntityClient> RegisterClient(EntityClient client)
        {

            await _clientrepository.Insert(client);
            return client;
        }

        public async Task<IEnumerable<EntityClient>> GetAllClients()
        {
            var clientList = await _clientrepository.GetAll();
            return clientList.OrderBy(client => client.Id);
        }

        public async Task<IEnumerable<EntityClient>> DeleteClient(int id)
        {
            await _clientrepository.Delete(id);
            var clientList = await _clientrepository.GetAll();
            return clientList.OrderBy(client => client.Id);
        }

        public async Task<EntityClient> UpdateHouse(int id, EntityClient client)
        {
            await _clientrepository.Update(id, client);
            return client;
        }
    }
}
