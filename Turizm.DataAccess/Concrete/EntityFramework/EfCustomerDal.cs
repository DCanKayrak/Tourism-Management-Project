using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.DataAccess.Abstract;
using Turizm.Entities.Concrete;

namespace Turizm.DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepository<Customer,TurizmContext> , ICustomerDal
    {
    }
}
