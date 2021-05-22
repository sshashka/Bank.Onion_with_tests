using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Core.Clients;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMapper _mapper;
        private readonly BankContext _context;

        public ClientRepository(IMapper mapper, BankContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<global::Bank.Core.Clients.Clients> GetByIdAsync(int id)
        {
            var entity = await _context.Clients.FirstAsync(x => x.Id == id);
            return _mapper.Map<global::Bank.Core.Clients.Clients>(entity);
        }

        public async Task<global::Bank.Core.Clients.Clients> Update(int id, int sum)
        {
            Client clients = (
                from n in _context.Clients
                where n.Id == id
                select n).First();
            
            clients.Sum = sum;
            var addResult = _context.Clients.Update(clients);

            await _context.SaveChangesAsync();
            return _mapper.Map<global::Bank.Core.Clients.Clients>(clients);
            
        }

        public async Task RemoveById(int id)
        {
            var entity = await _context.Clients.FirstAsync(x => x.Id == id);
            _context.Clients.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<global::Bank.Core.Clients.Clients> AddAsync(global::Bank.Core.Clients.Clients client)
        {
            var clientEntity = _mapper.Map<Client>(client);
            var result = await _context.Clients.AddAsync(clientEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<global::Bank.Core.Clients.Clients>(result.Entity);
            
        }
    }
}
