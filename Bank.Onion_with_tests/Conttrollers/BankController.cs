using System.Threading.Tasks;
using AutoMapper;
using Bank.Core.Bank;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Onion_with_tests.Conttrollers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly IBankService _service;
        private readonly IMapper _mapper;
        public BankController(IMapper mapper, IBankService service)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public Core.Bank.Bank Get(int userId)
        {
            return _service.Get(userId);
        }
        [HttpPost]
        public async Task<Core.Bank.Bank> PostAsync([FromBody] Orchestrators.Bank.Bank bank)
        {
            var mappedCoreUser = _mapper.Map<Core.Bank.Bank>(bank);
            var addResult = await _service.PostAsync(mappedCoreUser);
            return addResult;
        }
        [HttpPatch]
        public async Task<IActionResult> PatchAsync(int id,int count)
        {
            var addResult = await _service.PatchAsync(id, count);
            return Ok(addResult);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedUser = await _service.DeleteAsync(id);
            return Ok(deletedUser);
        }
    }
}