using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Concrete;

namespace Turizm.DataAccess.Abstract
{
    public interface IAccountDal : IEntityRepository<Account>
    {
    }
}
