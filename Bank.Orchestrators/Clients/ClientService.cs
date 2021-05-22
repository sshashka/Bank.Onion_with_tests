using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Core.Banks;
using Bank.Core.Clients;

namespace Bank.Orchestrators.Clients
{
    public class ClientService : IClientService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IClientRepository _clientRepository;

        public ClientService(
            IBankRepository bankRepository,
            IClientRepository clientRepository)
        {
            _bankRepository = bankRepository;
            _clientRepository = clientRepository;
        }
        public async Task<Core.Clients.Clients> GetByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task<Core.Clients.Clients> Update(int id, int sum)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                throw new ArgumentNullException();
            if (sum < 0)
                throw new ArgumentOutOfRangeException();
            client.Sum = sum;
            await _clientRepository.Update(id, sum);
            return client;
        }

        public async Task RemoveById(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                throw new ArgumentNullException();
            await _clientRepository.RemoveById(id);
        }

        public async Task<Core.Clients.Clients> AddAsync(Core.Clients.Clients client)
        {
            var existingBank = await _bankRepository.GetByIdAsync(client.BankId);
            

            return await _clientRepository.AddAsync(client);
        }
    }
}
