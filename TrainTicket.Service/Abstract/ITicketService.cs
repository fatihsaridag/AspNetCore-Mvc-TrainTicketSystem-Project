using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Entity.Entities;

namespace TrainTicket.Service.Abstract
{
    public interface ITicketService
    {
        Ticket TGetById(int Id);
        void TAdd(Ticket entity);
        void TUpdate(Ticket entity);
        void TDelete(Ticket entity);
        List<Ticket> TGetAll();

        Ticket TicketQuery(string query);


    }
}
