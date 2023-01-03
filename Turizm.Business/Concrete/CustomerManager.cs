using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Business.Abstract;
using Turizm.Business.Concrete.FluentValidation;
using Turizm.DataAccess.Abstract;
using Turizm.Entities.Concrete;

namespace Turizm.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public List<Customer> GetAll()
        {
            return _customerDal.GetAll(null);
        }
        public Customer GetWithId(int id)
        {
            return _customerDal.Get(p => p.Id.Equals(id));
        }
        public List<Customer> GetAllWithNationality(string nationalityId)
        {
            return _customerDal.GetAll(p => p.NationalityId.StartsWith(nationalityId.Trim()));
        }
        public List<Customer> GetAllWithTourName(string tourName)
        {
            return _customerDal.GetAll(p => p.RegisteredTour.ToLower().Trim().Contains(tourName.ToLower().Trim()));
        }

        public void Add(Customer entity)
        {
            CustomerValidator validationRules = new CustomerValidator();
            var result = validationRules.Validate(entity);
            if (result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
            _customerDal.Add(entity);
        }

        public void Delete(Customer entity)
        {
            _customerDal.Delete(entity);
        }

        public void Update(Customer entity)
        {
            CustomerValidator validationRules = new CustomerValidator();
            var result = validationRules.Validate(entity);
            if (result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
            _customerDal.Update(entity);
        }
    }
}
