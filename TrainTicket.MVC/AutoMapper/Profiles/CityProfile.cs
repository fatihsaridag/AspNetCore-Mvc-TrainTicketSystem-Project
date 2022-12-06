using AutoMapper;
using TrainTicket.Entity.Entities;
using TrainTicket.MVC.Models;

namespace TrainTicket.MVC.AutoMapper.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityAddViewModel>().ReverseMap();
            CreateMap<City, CityEditViewModel>().ReverseMap();
        }
    }
}
