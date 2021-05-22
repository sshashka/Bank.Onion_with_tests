using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Core.Clients;
using Bank.Orchestrators.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Onion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Client : ControllerBase
    {
        private readonly IClientService _service;
        private readonly IMapper _mapper;

        public Client(IClientService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        
        [HttpPost("banks/{bankId}/clients")]
        public async Task<IActionResult> PostAsync(int bankId, Bank.Orchestrators.Clients.Client client)
        {
            var clientModel = _mapper.Map<Core.Clients.Clients>(client);
            clientModel.BankId = bankId
                ;
            var createdModel = await _service.AddAsync(clientModel);
            return Ok(_mapper.Map<Core.Clients.Clients>(createdModel));
        }
        [HttpGet("banks/clients/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var client = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<Core.Clients.Clients>(client));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCount(int id, [FromBody]UpdateSum sum)
        {
            await _service.Update(id, sum.Sum);
            return Ok((id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveById(id);
            return Ok();
        }

    }
}
