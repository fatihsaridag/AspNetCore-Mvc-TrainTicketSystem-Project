using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Data.Repositories.Abstract;
using TrainTicket.Entity.Entities;

namespace TrainTicket.Data.Repositories.Concrete
{
    public class EfTicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public EfTicketRepository(DbContext context) : base(context)
        {
        }

    
    }
}
