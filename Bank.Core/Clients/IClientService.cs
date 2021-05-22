using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Core.Clients
{
    public interface IClientService
    {
        Task<Clients> GetByIdAsync(int id);
        Task<Clients> Update(int id, int sum);
        Task RemoveById(int id);
        Task<Clients> AddAsync(Clients client);
    }
}
