using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Concrete;

namespace Turizm.Business.Abstract
{
    interface IAccountService:IEntityService<Account>
    {
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
    }
}
