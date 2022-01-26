using AutoMapper;
using BarManager.BL.Interfaces;
using ECarShop.BL.Interfaces;
using ECarShop.Models.DTO;
using ECarShop.Models.Requests;
using ECarShop.Models.Responses;
using ECarShop.Requests;
using ECarShop.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECarShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _clientService.GetAll();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();

            var result = _clientService.GetById(id);

            if (result == null) return NotFound(id);

            var response = _mapper.Map<ClientResponse>(result);

            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateClient([FromBody] ClientRequest clientRequest)
        {
            if (clientRequest == null) return BadRequest();

            var client = _mapper.Map<Client>(clientRequest);

            var result = _clientService.Create(client);

            return Ok(client);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest(id);

            var result = _clientService.Delete(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] ClientUpdateRequest clientRequest)
        {
            if (clientRequest == null) return BadRequest();

            var searchClient = _clientService.GetById(clientRequest.Id);

            if (searchClient == null) return NotFound(clientRequest.Id);

            searchClient.Username = clientRequest.Username;

            var result = _clientService.Update(searchClient);

            return Ok(result);
        }


    }
}
