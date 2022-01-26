using AutoMapper;
using ECarShop.BL.Interfaces;
using ECarShop.DL.Interfaces;
using ECarShop.Models.DTO;
using ECarShop.Models.Requests;
using ECarShop.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ECarShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController(IBillService carService,IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();

            var result = _carService.GetById(id);

            if (result == null) return NotFound(id);
            var response = _mapper.Map<CarResponse>(result);

            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateCar([FromBody] CarRequest carRequest)
        {
            if (carRequest == null) return BadRequest();

            var car = _mapper.Map<Car>(carRequest);

            var result = _carService.Create(car);

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest(id);

            var result = _carService.Delete(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] Car car)
        {
            if (car == null) return BadRequest();

            var searchCar = _carService.GetById(car.Id);

            if (searchCar == null) return NotFound(car.Id);

            var result = _carService.Update(car);

            return Ok(result);
        }

       
    }
}
