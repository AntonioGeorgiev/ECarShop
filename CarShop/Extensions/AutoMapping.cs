using AutoMapper;
using ECarShop.Models.DTO;
using ECarShop.Models.Requests;
using ECarShop.Models.Responses;

namespace ECarShop.Extensions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Car, CarResponse>().ReverseMap();
            CreateMap<CarResponse, Car>().ReverseMap();
            CreateMap<CarRequest, Car>().ReverseMap();

            CreateMap<Client, ClientResponse>().ReverseMap();
            CreateMap<ClientResponse, Client>().ReverseMap();
            CreateMap<ClientRequest, User>().ReverseMap();

            CreateMap<Dealer, DealerResponse>().ReverseMap();
            CreateMap<DealerResponse, Dealer>().ReverseMap();
            CreateMap<DealerRequest, Dealer>().ReverseMap();

        }
    }
}
