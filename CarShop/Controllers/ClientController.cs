using AutoMapper;
using ECarShop.BL.Interfaces;
using ECarShop.Models.DTO;
using ECarShop.Models.Requests;
using ECarShop.Models.Responses;
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
        public IActionResult Update([FromBody] Client client)
        {
            if (client == null) return BadRequest();

            var searchClient = _clientService.GetById(client.Id);

            if (searchClient == null) return NotFound(client.Id);

            searchClient.Username = client.Username;

            var result = _clientService.Update(searchClient);

            return Ok(result);
        }


    }
}
