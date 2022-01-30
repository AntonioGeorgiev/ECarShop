using ECarShop.BL.Interfaces;
using ECarShop.DL.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using ECarShop.Models.DTO;

namespace ECarShop.BL.Services

   {
    public class DealerService : IDealerService
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly ILogger _logger;

        public DealerService(IDealerRepository dealerRepository, ILogger logger)
        {
            _dealerRepository = dealerRepository;
            _logger = logger;
        }

        public Dealer Create(Dealer dealer)
        {
            try
            {
                var index = _dealerRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;

                dealer.Id = (int)(index != null ? index + 1 : 1);
                return _dealerRepository.Create(dealer);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }

            _logger.Information("Dealer Create() ");

            return _dealerRepository.Create(dealer);
        }

        public Dealer Delete(int id)
        {
            _logger.Information("Dealer Delete() ");

            return _dealerRepository.Delete(id);
        }

        public IEnumerable<Dealer> GetAll()
        {
            _logger.Information("Dealer GetAll() ");

            return _dealerRepository.GetAll();
        }

        public Dealer GetById(int id)
        {
            _logger.Information("Dealer GetById() ");

            return _dealerRepository.GetById(id);
        }

        public Dealer Update(Dealer dealer)
        {
            _logger.Information("Dealer Update() ");

            return _dealerRepository.Update(dealer);
        }
    }
}
