using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Abstract;

namespace Turizm.Entities.Concrete
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string NationalityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string RegisteredTour { get; set; }
    }
}
