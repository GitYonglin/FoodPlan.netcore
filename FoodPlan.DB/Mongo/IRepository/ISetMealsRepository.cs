using FoodPlan.Core;
using FoodPlan.Core.Entity;
using FoodPlan.DB.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodPlan.DB.Mongo.IRepository
{
    public interface ISetMealsRepository: IBaseRepository<SetMeals>
    {
        //Task AddAsync(Core.Entity.Single food);
        //Task<IEnumerable<Core.Entity.Single>> AllAsync();
        Task<PageReturnData<IEnumerable<SetMealTables>>> GetTableAsync(QueryParameters queryParameters);
    }
}
