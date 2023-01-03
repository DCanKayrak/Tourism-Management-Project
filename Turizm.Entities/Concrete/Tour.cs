using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Abstract;

namespace Turizm.Entities.Concrete
{
    public class Tour:IEntity
    {
        public int Id { get; set; }
        public string TourType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // bunun için özel bir form oluşturup bu şekil yapılabilir.
        public string isVisaRequired { get; set; }
        public string isAnyMealsIncluded { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
