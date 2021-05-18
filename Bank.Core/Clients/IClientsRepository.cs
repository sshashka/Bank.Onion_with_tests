using System.Threading.Tasks;

namespace Bank.Core.Clients
{
    public interface IClientsRepository
    {
        Task<Clients> GetAsync(int id);
        Task<Clients> PostAsync(Clients clients);
        Task<Clients> DeleteAsync(int id);
    }
}