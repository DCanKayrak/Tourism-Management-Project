using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Turizm.Business.Abstract;
using Turizm.DataAccess.Abstract;
using Turizm.Entities.Concrete;

namespace Turizm.Business.Concrete
{
    public class AccountManager : IAccountService
    {
        private IAccountDal _accountDal;

        public AccountManager(IAccountDal accountDal)
        {
            _accountDal = accountDal;
        }

        public bool isAccountExist(string mail)
        {
            if(_accountDal.Get(p => p.Mail == mail.Trim()) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool LogIn(Account account)
        {
            Account acc = _accountDal.Get(p => p.Mail == account.Mail.ToString() && p.Password == account.Password.ToString());
            if(acc != null)
            {
                return true;
            }
            return false;
        }
        public void Add(Account entity)
        {
            _accountDal.Add(entity);
        }

        public void Delete(Account entity)
        {
            _accountDal.Delete(entity);
        }

        public Account Get()
        {
            return _accountDal.Get(null);
        }

        public List<Account> GetAll()
        {
            return _accountDal.GetAll(null);
        }

        public void Update(Account entity)
        {
            _accountDal.Update(entity);
        }
    }
}
