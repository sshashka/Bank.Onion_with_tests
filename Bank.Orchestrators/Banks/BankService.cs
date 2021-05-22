using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Core.Banks;

namespace Bank.Orchestrators.Banks
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;
        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository ;
        }
        public async Task<Core.Banks.Bank> AddAsync(Core.Banks.Bank bank)
        {
            return await _bankRepository.AddAsync(bank); 
        }

        public async Task<Core.Banks.Bank> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            return await _bankRepository.GetByIdAsync(id);
        }
        public async Task<Core.Banks.Bank> Update(int id, int count)
        {
            var bank = await _bankRepository.GetByIdAsync(id);
            if (bank == null)
                throw new ArgumentNullException();
            bank.Count = count;
            var updateBank = await _bankRepository.Update(id, count);
            return updateBank;
        }
        public async Task RemoveById(int id)
        {
            var bank = await _bankRepository.GetByIdAsync(id);
            if (bank == null)
                throw new ArgumentOutOfRangeException();
            await _bankRepository.RemoveById(id);
        }
    }
}
