using AutoMapper;
using ECarShop.Controllers;
using ECarShop.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ECarShop.BL.Interfaces;
using ECarShop.BL.Services;
using ECarShop.DL.Interfaces;
using ECarShop.Models.DTO;
using ECarShop.Models.Requests;
using ECarShop.Models.Responses;
using Xunit;

namespace ECarShop.Test
{
    public class CarTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICarRepository> _carRepository;
        private readonly ICarService _carService;
        private readonly CarController _carController;

        private IList<Car> Cars = new List<Car>() {

            new Car()
             {
                    Id = 1,
                    Brand = "Opel",
                    Model = "Vectra",
                     Price = 5000,

                },
                new Car()
               {
                    Id = 2,
                    Brand = "BMW",
                    Model = "E60",
                    Price = 10000,
                },
                new Car()
                 {
                    Id = 3,
                    Brand = "Mercedes Benz",
                    Model = "S500",
                   Price = 15000,
                },
        };


        public CarTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _carRepository = new Mock<ICarRepository>();

            var logger = new Mock<ILogger>();

            _carService = new CarService(_carRepository.Object, logger.Object);

            _carController = new CarController(_carService, _mapper);
        }

        [Fact]
        public void Car_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 3;

            var mockedService = new Mock<ICarService>();

            mockedService.Setup(x => x.GetAll()).Returns(Cars);

            //inject
            var controller = new CarController(mockedService.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var cars = okObjectResult.Value as IEnumerable<Car>;
            Assert.NotNull(cars);
            Assert.Equal(expectedCount, Cars.Count());
        }

        [Fact]
        public void Car_GetById_Amount_Check()
        {
            //setup
            var carId = 2;
            var expectedPrice = 10000;

            _carRepository.Setup(x => x.GetById(carId))
                .Returns(Cars.FirstOrDefault(t => t.Id == carId));

            //Act
            var result = _carController.GetById(carId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as CarResponse;
            var car = _mapper.Map<Car>(response);

            Assert.NotNull(car);
            Assert.Equal(expectedPrice, car.Price);

        }

        [Fact]
        public void Car_GetById_NotFound()
        {
            //setup
            var carId = 4;

            _carRepository.Setup(x => x.GetById(carId))
                .Returns(Cars.FirstOrDefault(t => t.Id == carId));

            //Act
            var result = _carController.GetById(carId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(carId, response);
        }

        [Fact]
        public void Car_Update_CarPrice()
        {
            var carId = 2;
            var expectedPrice = 10000;

            var car = Cars.FirstOrDefault(x => x.Id == carId);
            car.Price = expectedPrice;

            _carRepository.Setup(x => x.GetById(carId))
                .Returns(Cars.FirstOrDefault(t => t.Id == carId));
            _carRepository.Setup(x => x.Update(car))
                .Returns(car);

            //Act
            var carRequest = _mapper.Map<CarRequest>(car);
            var result = _carController.Update(car);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Car;
            Assert.NotNull(val);
            Assert.Equal(expectedPrice, val.Price);

        }

        [Fact]
        public void Car_Delete_ExistingCar()
        {
            //Setup
            var carId = 2;

            var car = Cars.FirstOrDefault(x => x.Id == carId);

            _carRepository.Setup(x => x.Delete(carId)).Callback(() => Cars.Remove(car)).Returns(car);

            //Act
            var result = _carController.Delete(carId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Car;
            Assert.NotNull(val);
            Assert.Equal(2, Cars.Count);
            Assert.Null(Cars.FirstOrDefault(x => x.Id == carId));
        }

        [Fact]
        public void Car_Delete_NotExisting_Tag()
        {
            //Setup
            var carId = 6;

            var car = Cars.FirstOrDefault(x => x.Id == carId);

            _carRepository.Setup(x => x.Delete(carId)).Callback(() => Cars.Remove(car)).Returns(car);

            //Act
            var result = _carController.Delete(carId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(carId, response);
        }

        [Fact]
        public void Car_CreateTag()
        {
            //setup
            var car = new Car()
            {

                Id = 4,
                Brand = "Car4",
                Model = "Model4",
                Price = 10000,

            };

            _carRepository.Setup(x => x.GetAll()).Returns(Cars);

            _carRepository.Setup(x => x.Create(It.IsAny<Car>())).Callback(() =>
            {
                Cars.Add(car);
            }).Returns(car);

            //Act
            var result = _carController.CreateCar(_mapper.Map<CarRequest>(car));

            //Assert
            var okObjectResult = result as OkObjectResult;

            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(Cars.FirstOrDefault(x => x.Id == car.Id));
            Assert.Equal(4, Cars.Count);

        }

    }
}
