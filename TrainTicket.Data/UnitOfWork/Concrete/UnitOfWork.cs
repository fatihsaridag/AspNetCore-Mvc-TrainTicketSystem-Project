using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Data.Contexts;
using TrainTicket.Data.Repositories.Abstract;
using TrainTicket.Data.Repositories.Concrete;
using TrainTicket.Data.UnitOfWork.Abstract;

namespace TrainTicket.Data.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrainTicketContext _context;
        private EfCityRepository _cityRepository;
        private EfTicketRepository _ticketRepository;
        private EfTrainRouteRepository _trainRouteRepository;

        public UnitOfWork(TrainTicketContext context)
        {
            _context = context;
        }


        public ITicketRepository Tickets => _ticketRepository ?? new EfTicketRepository(_context);

        public ITrainRouteRepository TrainRoutes  => _trainRouteRepository ?? new EfTrainRouteRepository(_context);

        public ICityRepository Cities => _cityRepository ?? new EfCityRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();

        }
    }
}
