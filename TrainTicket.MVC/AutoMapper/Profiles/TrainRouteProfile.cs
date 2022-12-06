using AutoMapper;
using TrainTicket.Entity.Entities;
using TrainTicket.MVC.Models;

namespace TrainTicket.MVC.AutoMapper.Profiles
{
    public class TrainRouteProfile : Profile
    {
        public TrainRouteProfile()
        {
            CreateMap<TrainRoute, TrainRouteAddViewModel>().ReverseMap();
            CreateMap<TrainRoute, TrainRouteEditViewModel>().ReverseMap();
        }
    }
}
