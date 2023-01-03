using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Business.Abstract;
using Turizm.Business.Concrete.FluentValidation;
using Turizm.DataAccess.Abstract;
using Turizm.DataAccess.Concrete.EntityFramework;
using Turizm.Entities.Concrete;

namespace Turizm.Business.Concrete
{
    public class LossManager : ILossService
    {
        private ILossDal _lossDal;
        public LossManager(ILossDal lossDal)
        {
            _lossDal = lossDal;
        }
        public void Add(Loss entity)
        {
            LossValidator validator = new LossValidator();
            var result = validator.Validate(entity);
            if(result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
            _lossDal.Add(entity);
        }

        public void Delete(Loss entity)
        {
            _lossDal.Delete(entity);
        }

        public void Update(Loss entity)
        {
            _lossDal.Update(entity);
        }
        public List<Loss> getAllWithName(string name)
        {
            return _lossDal.GetAll(p => p.Name.Equals(name));
        }
        public List<Loss> getAllWithTourName(string name)
        {
            return _lossDal.GetAll(p => p.Tour.Equals(name));
        }
        public Loss GetWithId(int id)
        {
            return _lossDal.Get(p => p.Id.Equals(id));
        }
        public List<Loss> GetAll()
        {
            return _lossDal.GetAll(null);
        }
    }
}
