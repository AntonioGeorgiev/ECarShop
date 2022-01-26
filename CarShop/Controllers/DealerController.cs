using AutoMapper;
using BarManager.Models.Responses;
using ECarShop.BL.Interfaces;
using ECarShop.Models.DTO;
using ECarShop.Models.Requests;
using ECarShop.Models.Responses;
using Microsoft.AspNetCore.Mvc;
namespace ECarShop.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealerController : ControllerBase
    {
        private readonly IDealerService _dealerService;
        private readonly IMapper _mapper;
        public DealerController(IDealerService dealerService, IMapper mapper)
        {
            _dealerService = dealerService;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _dealerService.GetAll();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();

            var result = _dealerService.GetById(id);

            if (result == null) return NotFound(id);

            var response = _mapper.Map<DealerResponse>(result);

            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateDealer([FromBody] DealerRequest dealerRequest)
        {
            if (dealerRequest == null) return BadRequest();

            var dealer = _mapper.Map<Dealer>(dealerRequest);

            var result = _dealerService.Create(dealer);

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest(id);

            var result = _dealerService.Delete(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] DealerUpdateRequest dealerRequest)
        {
            if (dealerRequest == null) return BadRequest();

            var searchDealer = _dealerService.GetById(dealerRequest.Id);

            if (searchDealer == null) return NotFound(dealerRequest.Id);

            searchDealer.Name = dealerRequest.Name;

            var result = _dealerService.Update(searchDealer);

            return Ok(result);
        }
    }
}
