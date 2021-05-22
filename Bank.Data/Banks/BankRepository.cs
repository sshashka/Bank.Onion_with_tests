using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Core.Banks;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data.Banks
{
    public class BankRepository : IBankRepository
    {
        private readonly IMapper _mapper;
        private readonly BankContext _context;

        public BankRepository(IMapper mapper,
            BankContext bankContext)
        {
            _mapper = mapper;
            _context = bankContext;
        }

        public async Task<Core.Banks.Bank> AddAsync(Core.Banks.Bank bank)
        {
            var daoNew = _mapper.Map<Bank>(bank);
            var addAsync = await _context.Banks.AddAsync(daoNew);
            await _context.SaveChangesAsync();
            return _mapper.Map<Core.Banks.Bank>(addAsync.Entity);
        }
        
        public async Task<Core.Banks.Bank> GetByIdAsync(int id)
        {
            var entity = await _context.Banks.FirstAsync(x => x.Id == id);
            if (entity == null)
                throw new ArgumentNullException();
            return _mapper.Map<Core.Banks.Bank>(entity);
        }

        public async Task RemoveById(int id)
        {
            var entity = await _context.Banks.FirstAsync(x => x.Id == id);
            _context.Banks.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Core.Banks.Bank> Update(int id, int count)
        {
            Bank bank = (
                from n in _context.Banks
                where n.Id == id
                select n).First();
            
            bank.Count = count;
            var addResult = _context.Banks.Update(bank);

            await _context.SaveChangesAsync();
            return _mapper.Map<Core.Banks.Bank>(addResult.Entity);
        }
    }
}
