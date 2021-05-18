using System.Threading.Tasks;

namespace Bank.Core.Clients
{
    public interface IClientsService
    {
        Task<Clients> GetAsync(int id);
        Task<Clients> PostAsync(Clients clients);
        Task<Clients> DeleteAsync(int id);
    }
}