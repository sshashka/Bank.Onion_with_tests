using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Core.Banks
{
    public interface IBankService
    {
        Task<Bank> GetByIdAsync(int id);
        Task<Bank> Update(int id, int count);
        Task RemoveById(int id);
        Task<Bank> AddAsync(Bank bank);
    }
}
