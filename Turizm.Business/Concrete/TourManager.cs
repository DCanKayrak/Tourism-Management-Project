using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Turizm.Business.Abstract;
using Turizm.Business.Concrete.FluentValidation;
using Turizm.DataAccess.Abstract;
using Turizm.Entities.Concrete;

namespace Turizm.Business.Concrete
{
    public class TourManager //: //ITourService
    {
        private ITourDal _tourDal;
        public TourManager(ITourDal tourManager)
        {
            _tourDal = tourManager;
        }
        public void Add(Tour entity)
        {
            TourValidator validationRules = new TourValidator();
            var result = validationRules.Validate(entity);
            if(result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
            _tourDal.Add(entity);
        }

        public void Delete(Tour entity)
        {
            _tourDal.Delete(entity);
        }

        //public Tour GetByTourType()
        //{
        //    _tourDal.Get(p=>p.TourType.ToLower().Equals());
        //}

        public List<Tour> GetAll()
        {
            return _tourDal.GetAll(null);
        }
        public List<Tour> GetAllWithName(string name)
        {
            return _tourDal.GetAll(p=>p.Name.ToLower().Contains(name.Trim().ToLower()));
        }
        public Tour GetWithName(string name)
        {
            return _tourDal.Get(p => p.Name.ToLower().Equals(name.Trim().ToLower()));
        }
        public List<Tour> GetAllWithType(string tourtype)
        {
            return _tourDal.GetAll(p=>p.TourType.ToLower().Contains(tourtype.Trim().ToLower()));
        }
        public Tour GetWithId(int Id)
        {
            return _tourDal.Get(p => p.Id.Equals(Id));
        }
        public void Update(Tour entity)
        {
            TourValidator validationRules = new TourValidator();
            var result = validationRules.Validate(entity);
            if(result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
            _tourDal.Update(entity);
        }
    }
}
