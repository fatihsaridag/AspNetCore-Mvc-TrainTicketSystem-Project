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
    public class CityManager : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void TAdd(City entity)
        {
            _unitOfWork.Cities.Add(entity);
            _unitOfWork.SaveChangesAsync();

        }

        public void TDelete(City entity)
        {
            _unitOfWork.Cities.Delete(entity);
            _unitOfWork.SaveChangesAsync();

        }

        public List<City> TGetAll()
        {
           return  _unitOfWork.Cities.GetAll();
        }

        public City TGetById(int Id)
        {
            return _unitOfWork.Cities.GetById(Id);
        }

        public void TUpdate(City entity)
        {
            _unitOfWork.Cities.Update(entity);
            _unitOfWork.SaveChangesAsync();

        }
    }
}
