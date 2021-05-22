using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Core.Banks;
using Bank.Orchestrators.Banks;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Onion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly IBankService _service;
        private readonly IMapper _mapper;

        public BankController(IMapper mapper, IBankService service)
        {
            _mapper = mapper;
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var bank = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<Orchestrators.Banks.Bank>(bank));
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> PostAsync(Orchestrators.Banks.Bank bank)
        {
            var bankModel = _mapper.Map<Core.Banks.Bank>(bank);
            var createdModel = await _service.AddAsync(bankModel);
            return Ok(_mapper.Map<Orchestrators.Banks.Bank>(createdModel));
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCount(int id, UpdateCount count)
        {
            await _service.Update(id, count.Count);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.RemoveById(id);
            return Ok();
        }
    }
}
