using FoodPlan.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodPlan.DB.Mongo.IRepository
{
    public interface ISinglesRepository: IBaseRepository<Core.Entity.Single>
    {
        //Task AddAsync(Core.Entity.Single food);
        //Task<IEnumerable<Core.Entity.Single>> AllAsync();
    }
}
