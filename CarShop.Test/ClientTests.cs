using AutoMapper;
using ECarShop.BL.Interfaces;
using ECarShop.BL.Services;
using ECarShop.Controllers;
using ECarShop.DL.Interfaces;
using ECarShop.Extensions;
using ECarShop.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ECarShop.Models.Requests;
using ECarShop.Models.Responses;
using Xunit;

namespace BarManager.Test
{
    public class ClientTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly IClientService _clientService;
        private readonly ClientController _clientController;

        private IList<Client> Clients = new List<Client>()
        {
            {new Client() {Id = 1, Username = "TestUsername", Balance = 13000}},
            {new() {Id = 2, Username = "AnotherUsername", Balance = 17000}}
        };

        public ClientTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _clientRepository = new Mock<IClientRepository>();

            var logger = new Mock<ILogger>();

            _clientService = new ClientService(_clientRepository.Object, logger.Object);

            _clientController = new ClientController(_clientService, _mapper);
        }

        [Fact]
        public void Client_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            var mockedService = new Mock<IClientService>();

            mockedService.Setup(x => x.GetAll()).Returns(Clients);

            //inject
            var controller = new ClientController(mockedService.Object, _mapper);

           //Act
            var result = controller.GetAll();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var clients = okObjectResult.Value as IEnumerable<Client>;
            Assert.NotNull(clients);
            Assert.Equal(expectedCount, clients.Count());
        }

        [Fact]
        public void Client_GetById_NameCheck()
       {
           //setup
            var clientId = 2;
            var expectedUsername = "AnotherUsername";

            _clientRepository.Setup(x => x.GetById(clientId))
                .Returns(Clients.FirstOrDefault(t => t.Id == clientId));

           //Act
            var result = _clientController.GetById(clientId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as ClientResponse;
            var client = _mapper.Map<Client>(response);

            Assert.NotNull(client);
            Assert.Equal(expectedUsername, client.Username);
        }

        [Fact]
        public void Client_GetById_DiscountCheck()
        {
            //setup
            var clientId = 2;
            var expectedDiscount = 6;

            _clientRepository.Setup(x => x.GetById(clientId))
               .Returns(Clients.FirstOrDefault(t => t.Id == clientId));

            //Act
            var result = _clientController.GetById(clientId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as ClientResponse;
            var client = _mapper.Map<Client>(response);

            Assert.NotNull(client);
            Assert.Equal(expectedDiscount, client.Balance);
        }

        [Fact]
        public void Client_GetById_MoneySpendCheck()
        {
            //setup
            var clientId = 2;
            var expectedMoneySpend = 17000;

            _clientRepository.Setup(x => x.GetById(clientId))
                .Returns(Clients.FirstOrDefault(t => t.Id == clientId));

            //Act
            var result = _clientController.GetById(clientId);

            //Assert
            var okObjectResult = result as OkObjectResult;
           Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as ClientResponse;
            var client = _mapper.Map<Client>(response);

           Assert.NotNull(client);
            Assert.Equal(expectedMoneySpend, client.Balance);
        }

        [Fact]
        public void Client_GetById_NotFound()
        {
            //setup
            var clientId = 4;

            _clientRepository.Setup(x => x.GetById(clientId))
                .Returns(Clients.FirstOrDefault(t => t.Id == clientId));

            //Act
            var result = _clientController.GetById(clientId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(clientId, response);
        }

        [Fact]
        public void Client_Update_ClientName()
        {
            var clientId = 1;
            var expectedName = "Updated Client Name";

            var client = Clients.FirstOrDefault(x => x.Id == clientId);
            client.Username = expectedName;

            _clientRepository.Setup(x => x.GetById(clientId))
                .Returns(Clients.FirstOrDefault(t => t.Id == clientId));
          _clientRepository.Setup(x => x.Update(client))
                .Returns(client);

            //Act
            var clientUpdateRequest = _mapper.Map<ClientUpdateRequest>(client);
            var result = _clientController.Update(clientUpdateRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
           Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Client;
            Assert.NotNull(val);
            Assert.Equal(expectedName, val.Username);

        }

        [Fact]
        public void Client_Delete_ExistingClient()
       {
            //Setup
            var clientId = 1;

            var client = Clients.FirstOrDefault(x => x.Id == clientId);

            _clientRepository.Setup(x => x.Delete(clientId)).Callback(() => Clients.Remove(client)).Returns(client);

            //Act
            var result = _clientController.Delete(clientId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Client;
            Assert.NotNull(val);
            Assert.Equal(1, Clients.Count);
            Assert.Null(Clients.FirstOrDefault(x => x.Id == clientId));
        }

        [Fact]
        public void Client_Delete_NotExisting_Client()
        {
            //Setup
            var clientId = 4;

            var client = Clients.FirstOrDefault(x => x.Id == clientId);

            _clientRepository.Setup(x => x.Delete(clientId)).Callback(() => Clients.Remove(client)).Returns(client);

            //Act
            var result = _clientController.Delete(clientId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(clientId, response);
        }

        [Fact]
        public void Client_CreateClient()
        {
            //setup
            var client = new Client()
            {
               Id = 4,
                Username = "Username 4"
            };

            _clientRepository.Setup(x => x.GetAll()).Returns(Clients);

            _clientRepository.Setup(x => x.Create(It.IsAny<Client>())).Callback(() =>
            {
                Clients.Add(client);
            }).Returns(client);

            //Act
            var result = _clientController.CreateClient(_mapper.Map<ClientRequest>(client));

            //Assert
            var okObjectResult = result as OkObjectResult;

            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);
           Assert.NotNull(Clients.FirstOrDefault(x => x.Id == client.Id));
            Assert.Equal(3, Clients.Count);

        }

    }
}
