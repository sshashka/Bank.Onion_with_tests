using System.Threading.Tasks;

namespace Bank.Core.Bank
{
    public interface IBankRepository
    {
        Bank Get(int id);
        Task<Bank> PostAsync(Bank bank);
        Task<Bank> PatchAsync(int id,int count);
        Task<Bank> DeleteAsync(int id);
    }
}