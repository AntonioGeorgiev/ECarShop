using ECarShop.BL.Interfaces;
using ECarShop.DL.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using ECarShop.Models.DTO;

namespace ECarShop.BL.Services
{
 public class CarService : ICarService
        {
            private readonly ICarRepository _carRepository;
            private readonly ILogger _logger;

            public CarService(ICarRepository carRepository, ILogger logger)
            {
                _carRepository = carRepository;
                _logger = logger;
            }

            public Car Create(Car car)
            {
                try
                {
                    var index = _carRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;
                    car.Id = (int)(index != null ? index + 1 : 1);
                    return _carRepository.Create(car);
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }

                _logger.Information("Car Create()");
                return _carRepository.Create(car);
            }

            public Car Delete(int id)
            {
                _logger.Information("Car Delete()");

                return _carRepository.Delete(id);
            }

            public IEnumerable<Car> GetAll()
            {
                _logger.Information("Car GetAll()");

                return _carRepository.GetAll();
            }

            public Car GetById(int id)
            {

                _logger.Information("Car GetById()");

                return _carRepository.GetById(id);
            }

            public Car Update(Car car)
            {
                _logger.Information("Car Update()");

                return _carRepository.Update(car);
            }
        }
    }

