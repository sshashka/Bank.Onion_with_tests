using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Core.Clients;

namespace Bank.Data.Client
{
    public class ClientRepository : IClientsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClientRepository(IMapper mapper, AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Core.Clients.Clients> GetAsync(int id)
        {
            var clientsSearchResult = _context.Clients.Where(c => c.Id == id).Single();
            return _mapper.Map<Core.Clients.Clients>(clientsSearchResult);
        }
        public async Task<Core.Clients.Clients> PostAsync(Core.Clients.Clients clients)
        {
            var mappedToDataClients = _mapper.Map<Clients>(clients);

            var addResult =await _context.Clients.AddAsync(mappedToDataClients);
            await _context.SaveChangesAsync();

            var outClients = _mapper.Map<Core.Clients.Clients>(addResult.Entity);
            return outClients;
        }
        public async Task<Core.Clients.Clients> DeleteAsync(int id)
        {
            var categorSearchResult = _context.Clients.Where(c => c.Id == id).Single();

            _context.Clients.Remove(categorSearchResult);
            await _context.SaveChangesAsync();

            return _mapper.Map<Core.Clients.Clients>(categorSearchResult);
        }
    }
}