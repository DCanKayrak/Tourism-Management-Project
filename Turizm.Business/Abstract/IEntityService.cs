using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Turizm.Business.Abstract
{
    interface IEntityService<Entity>
    {
        void Add(Entity entity);
        void Update(Entity entity);
        void Delete(Entity entity);
    }
}
