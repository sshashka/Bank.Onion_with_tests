using System.Threading.Tasks;
using Bank.Core.Clients;

namespace Bank.Orchestrators.Client
{
    public class ClientService : IClientsService
    {
        private readonly IClientsRepository _repo;
        public ClientService(IClientsRepository repo)
        {
            _repo = repo;
        }
        public async Task<Core.Clients.Clients> GetAsync(int clientId)
        {
            return await _repo.GetAsync(clientId);
        }
        public async Task<Core.Clients.Clients> PostAsync(Core.Clients.Clients client)
        {
            return await _repo.PostAsync(client);
        }
        public async Task<Core.Clients.Clients> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}