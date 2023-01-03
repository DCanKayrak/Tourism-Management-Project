using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Abstract;

namespace Turizm.Entities.Concrete
{
    public class Loss : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AmountOfLoss { get; set; }
        public string Tour { get; set; }
    }
}
