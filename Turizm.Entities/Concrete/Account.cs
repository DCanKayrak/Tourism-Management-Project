using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Abstract;

namespace Turizm.Entities.Concrete
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
