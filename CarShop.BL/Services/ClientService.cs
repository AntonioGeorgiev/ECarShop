using ECarShop.BL.Interfaces;
using ECarShop.DL.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using ECarShop.Models.DTO;

namespace ECarShop.BL.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger _logger;

        public ClientService(IClientRepository clientRepository, ILogger logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public Client Create(Client client)
        {
            try
            {
                var index = _clientRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;

               client.Id = (int)(index != null ? index + 1 : 1);
                return _clientRepository.Create(client);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }

            _logger.Information("Client Create() ");

            return _clientRepository.Create(client);
        }

        public Client Delete(int id)
        {
            _logger.Information("Client Delete() ");

            return _clientRepository.Delete(id);
        }

        public IEnumerable<Client> GetAll()
        {
            _logger.Information("Client GetAll() ");

            return _clientRepository.GetAll();
        }

        public Client GetById(int id)
        {
            _logger.Information("Client GetById() ");

            return _clientRepository.GetById(id);
        }

        public Client Update(Client librarian)
        {
            _logger.Information("Client Update() ");

            return _clientRepository.Update(librarian);
        }
    }
}
