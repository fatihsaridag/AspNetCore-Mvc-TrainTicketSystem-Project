using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Data.Repositories.Abstract;
using TrainTicket.Data.UnitOfWork.Abstract;
using TrainTicket.Entity.Entities;
using TrainTicket.Service.Abstract;

namespace TrainTicket.Service.Concrete
{
    public class TicketManager : ITicketService
    {

        private readonly IUnitOfWork _unitOfWork;

        public TicketManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void TAdd(Ticket entity)
        {
            _unitOfWork.Tickets.Add(entity);
            _unitOfWork.SaveChangesAsync();

        }

        public void TDelete(Ticket entity)
        {
            _unitOfWork.Tickets.Delete(entity);
            _unitOfWork.SaveChangesAsync();

        }

        public List<Ticket> TGetAll()
        {
           var ticketListEntity =  _unitOfWork.Tickets.GetAll();
            return ticketListEntity;
        }

        public Ticket TGetById(int Id)
        {
            return _unitOfWork.Tickets.GetById(Id);
        }

        public void TUpdate(Ticket entity)
        {
            _unitOfWork.Tickets.Update(entity);
            _unitOfWork.SaveChangesAsync();

        }


    }
}
