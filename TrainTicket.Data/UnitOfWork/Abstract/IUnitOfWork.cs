using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Data.Repositories.Abstract;

namespace TrainTicket.Data.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ITicketRepository Tickets { get; }
        ITrainRouteRepository TrainRoutes { get; }
        ICityRepository Cities { get; }
        int SaveChangesAsync();

    }
}
