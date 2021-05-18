using System.Threading.Tasks;
using Bank.Core.Bank;

namespace Bank.Orchestrators.Bank
{
    public class BankService : IBankService

    {
    private readonly IBankRepository _repo;

    public BankService(IBankRepository repo)
    {
        _repo = repo;
    }

    public Core.Bank.Bank Get(int id)
    {
        return _repo.Get(id);
    }

    public async Task<Core.Bank.Bank> PostAsync(Core.Bank.Bank bank)
    {
        return await _repo.PostAsync(bank);
    }

    public async Task<Core.Bank.Bank> PatchAsync(int id, int count)
    {
        return await _repo.PatchAsync(id, count);
    }

    public async Task<Core.Bank.Bank> DeleteAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }
    }
}