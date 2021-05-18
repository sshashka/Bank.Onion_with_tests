using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Core.Bank;

namespace Bank.Data.Bank
{
    public class BankRepository : IBankRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BankRepository(IMapper mapper, AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public Core.Bank.Bank Get(int id)
        {

            Bank bank = _context.Banks.First(b => b.Id == id);
            return _mapper.Map<Core.Bank.Bank>(bank);
        }
        public async Task<Core.Bank.Bank> PostAsync(Core.Bank.Bank bank)
        {
            var daoNew = _mapper.Map<Bank>(bank);
            var addAsync = await _context.Banks.AddAsync(daoNew);
            await _context.SaveChangesAsync();
            return _mapper.Map<Core.Bank.Bank>(addAsync.Entity);
        }

        public async Task<Core.Bank.Bank> PatchAsync(int id, int count)
        {
            Bank bank = (
                from n in _context.Banks
                where n.Id == id
                select n).First();

            bank.CountOfWorkers = count;
            var addResult = _context.Banks.Update(bank);

            await _context.SaveChangesAsync();
            return _mapper.Map<Core.Bank.Bank>(addResult.Entity);
        }
        public async Task<Core.Bank.Bank> DeleteAsync(int id)
        {
            Bank bank = (
                from n in _context.Banks
                where n.Id == id
                select n).First();
            
            _context.Banks.Update(bank);
            await _context.SaveChangesAsync();
            return _mapper.Map<Core.Bank.Bank>(bank);
        }
    }

    
}