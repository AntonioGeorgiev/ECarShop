using ECarShop.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using ECarShop.BL.Interfaces;
using ECarShop.BL.Services;
using ECarShop.Controllers;
using ECarShop.DL.Interfaces;
using ECarShop.Extensions;
using ECarShop.Models.Requests;
using ECarShop.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using Xunit;
using ECarShop.Host.Controllers;

namespace ECarShop.Test
{
    public class DealerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDealerRepository> _dealerRepository;
        private readonly IDealerService _dealerService;
        private readonly DealerController _dealerController;

        private IList<Dealer> Dealers = new List<Dealer>()
        {
        new Dealer()
            {
                Id = 1,
                Name = "Dealer1",
                PhoneNumber = 696969
            },
            new Dealer()
            {
                Id = 2,
                Name = "Dealer2",
                PhoneNumber = 123456
            }, };


        public DealerTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();

            _dealerRepository = new Mock<IDealerRepository>();

            var logger = new Mock<ILogger>();

            _dealerService = new DealerService(_dealerRepository.Object, logger.Object);
            _dealerController = new DealerController(_dealerService, _mapper);

        }
        [Fact]
        public void Dealer_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            var mockedService = new Mock<IDealerService>();

            mockedService.Setup(x => x.GetAll()).Returns(Dealers);

            //inject
            var controller = new DealerController(mockedService.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var dealers= okObjectResult.Value as IEnumerable<Dealer>;
            Assert.NotNull(dealers);
            Assert.Equal(expectedCount, dealers.Count());
        }

        [Fact]
        public void Dealer_GetById_NameCheck()
        {
            //setup
            var DealerId = 2;
            var expectedName = "Dealer2";

            _dealerRepository.Setup(x => x.GetById(DealerId))
                .Returns(Dealers.FirstOrDefault(e => e.Id == DealerId));

            //Act
            var result = _dealerController.GetById(DealerId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as DealerResponse;
            var dealer = _mapper.Map<Dealer>(response);

            Assert.NotNull(dealer);
            Assert.Equal(expectedName, dealer.Name);
        }

        [Fact]
        public void Dealer_GetById_NotFound()
        {
            //setup
            var dealerId = 3;

            _dealerRepository.Setup(x => x.GetById(dealerId))
                .Returns(Dealers.FirstOrDefault(t => t.Id == dealerId));

            //Act
            var result = _dealerController.GetById(dealerId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(dealerId, response);
        }

        [Fact]
        public void Dealer_Update_DealerName()
        {
            var DealerId = 1;
            var expectedName = "Updated Dealer Name";

            var dealer = Dealers.FirstOrDefault(x => x.Id == DealerId);
            dealer.Name = expectedName;

            _dealerRepository.Setup(x => x.GetById(DealerId))
                .Returns(Dealers.FirstOrDefault(t => t.Id == DealerId));
            _dealerRepository.Setup(x => x.Update(dealer))
                .Returns(dealer);

            //Act
            var dealerUpdateRequest = _mapper.Map<Dealer>(dealer);
            var result = _dealerController.Update(dealerUpdateRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Dealer;
            Assert.NotNull(val);
            Assert.Equal(expectedName, val.Name);

        }

        [Fact]
        public void Dealer_Delete_ExistingDealer()
        {
            //Setup
            var dealerId = 1;

            var dealer = Dealers.FirstOrDefault(x => x.Id == dealerId);

            _dealerRepository.Setup(x => x.Delete(dealerId)).Callback(() => Dealers.Remove(dealer)).Returns(dealer);

            //Act
            var result = _dealerController.Delete(dealerId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Dealer;
            Assert.NotNull(val);
            Assert.Equal(1, Dealers.Count);
            Assert.Null(Dealers.FirstOrDefault(x => x.Id == dealerId));
        }

        [Fact]
        public void Dealer_Delete_NotExisting_Dealer()
        {
            //Setup
            var dealerId = 5;

            var dealer = Dealers.FirstOrDefault(x => x.Id == dealerId);

            _dealerRepository.Setup(x => x.Delete(dealerId)).Callback(() => Dealers.Remove(dealer)).Returns(dealer);

            //Act
            var result = _dealerController.Delete(dealerId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(dealerId, response);
        }

        [Fact]
        public void Dealer_CreateDealer()
        {
            //setup
            var dealer = new Dealer()
            {
                Id = 3,
                Name = "Dealer3",
                PhoneNumber = 000000,
            };

        _dealerRepository.Setup(x => x.GetAll()).Returns(Dealers);

        _dealerRepository.Setup(x => x.Create(It.IsAny<Dealer>())).Callback(() =>
            {
                Dealers.Add(dealer);
            }).Returns(dealer);

            //Act
            var result = _dealerController.CreateDealer(_mapper.Map<DealerRequest>(dealer));

            //Assert
            var okObjectResult = result as OkObjectResult;

            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(Dealers.FirstOrDefault(x => x.Id == dealer.Id));
            Assert.Equal(3, Dealers.Count);

        }


    }
}
