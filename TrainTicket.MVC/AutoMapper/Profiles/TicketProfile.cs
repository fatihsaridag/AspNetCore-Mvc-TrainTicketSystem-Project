using AutoMapper;
using TrainTicket.Entity.Entities;
using TrainTicket.MVC.Models;

namespace TrainTicket.MVC.AutoMapper.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketAddViewModel>().ReverseMap();
            CreateMap<Ticket, TicketEditViewModel>().ReverseMap();
        }
    }
}
