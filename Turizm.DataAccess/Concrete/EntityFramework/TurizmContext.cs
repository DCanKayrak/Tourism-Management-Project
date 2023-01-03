using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Concrete;

namespace Turizm.DataAccess.Concrete.EntityFramework
{
    public class TurizmContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loss> Losses { get; set; }
    }
}
