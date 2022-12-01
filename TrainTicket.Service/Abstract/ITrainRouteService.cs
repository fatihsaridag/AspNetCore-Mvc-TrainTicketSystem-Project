using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Entity.Entities;

namespace TrainTicket.Service.Abstract
{
    public interface ITrainRouteService
    {
        TrainRoute TGetById(int Id);
        void TAdd(TrainRoute entity);
        void TUpdate(TrainRoute entity);
        void TDelete(TrainRoute entity);
        List<TrainRoute> TGetAll();
        TrainRoute WhereToTrainRoute(string startRo , string endRo);

    }
}
