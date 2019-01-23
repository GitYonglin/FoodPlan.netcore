using FoodPlan.Core;
using FoodPlan.Core.Entity;
using FoodPlan.DB.Modules;
using FoodPlan.DB.Mongo.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once IdentifierTypo
namespace FoodPlan.DB.Mongo.Repository
{
    public class SetMealsRepository: BaseRepository<SetMeals>, ISetMealsRepository
    {
        public SetMealsRepository(IOptions<DBSettings> dBSettings) : base(dBSettings)
        {
            Context = Datebase.ContactSetMeals;
        }

        public async Task<PageReturnData<IEnumerable<SetMealTables>>> GetTableAsync(QueryParameters queryParameters)
        {
            try
            {
                var count = await Context.CountDocumentsAsync(_ => true);
                SortDefinition<SetMeals> sort = "{Id: -1}";
                FilterDefinition<SetMeals> Search = "{}";
                if (queryParameters.Search != null)
                {
                    Search = "{Name: { $regex: '"+ queryParameters.Search + "', $options: 'i' } }";
                }
                if (queryParameters.Ordays != null)
                {
                    sort = queryParameters.OrdayOk;
                }
                var list = new List<SetMealTables>();
                await Context.Find(Search)
                    .Sort(sort)
                    //.Project(projection)
                    .Skip((queryParameters.PageIndex - 1) * queryParameters.PageSize)
                    .Limit(queryParameters.PageSize).ForEachAsync(f =>
                    {
                        list.Add(new SetMealTables
                        {
                            _id = f._id,
                            Name = f.Name,
                            Inventory = f.Inventory,
                            Putaway = f.Putaway,
                            Unit = f.Unit,
                            UnitName = f.UnitName,
                            Price = PriceCount(f.Singles)
                        });
                    });
                return new PageReturnData<IEnumerable<SetMealTables>>()
                    { Data = list, Success = true, Count = count };

            }
            catch (Exception e)
            {
                return new PageReturnData<IEnumerable<SetMealTables>>()
                {
                    Success = false,
                    Error = e.Message,
                };
            }
        }
        /// <summary>
        /// 计算总价格
        /// </summary>
        /// <param name="Singles">食材组</param>
        /// <returns></returns>
        private static decimal PriceCount(IEnumerable<SelectSingles> Singles)
        {
            return Singles.Where(single => !single.State)
                .Sum(
                    single => 
                        (single.Single.Price / single.Single.Unit) * single.Number);
        }
    }
}
