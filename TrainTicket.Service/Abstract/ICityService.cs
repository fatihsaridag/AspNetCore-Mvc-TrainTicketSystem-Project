using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Entity.Entities;

namespace TrainTicket.Service.Abstract
{
    public interface ICityService
    {
        City TGetById(int Id);
        void TAdd(City entity);
        void TUpdate(City entity);
        void TDelete(City entity);
        List<City> TGetAll();
    }
}
