using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Data.UnitOfWork.Abstract;
using TrainTicket.Entity.Entities;
using TrainTicket.Service.Abstract;

namespace TrainTicket.Service.Concrete
{
    public class TrainRouteManager : ITrainRouteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainRouteManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void TAdd(TrainRoute entity)
        {
            _unitOfWork.TrainRoutes.Add(entity);
            _unitOfWork.SaveChangesAsync();
        }

        public void TDelete(TrainRoute entity)
        {
            _unitOfWork.TrainRoutes.Delete(entity);
            _unitOfWork.SaveChangesAsync();

        }

        public List<TrainRoute> TGetAll()
        {
          return   _unitOfWork.TrainRoutes.GetAll();
        }

        public TrainRoute TGetById(int Id)
        {
           return _unitOfWork.TrainRoutes.GetById(Id);
        }

        public void TUpdate(TrainRoute entity)
        {
            _unitOfWork.TrainRoutes.Update(entity);
            _unitOfWork.SaveChangesAsync();

        }

        public TrainRoute WhereToTrainRoute(string startRo, string endRo)
        {
            var TrainRoute = _unitOfWork.TrainRoutes.Where(x => x.StartRo == startRo && x.FinishRo == endRo);
            return TrainRoute;
        }
    }
}
