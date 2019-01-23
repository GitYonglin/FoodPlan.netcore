using FoodPlan.Core;
using FoodPlan.Core.Entity;
using FoodPlan.DB.Modules;
using FoodPlan.DB.Mongo.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once IdentifierTypo
namespace FoodPlan.DB.Mongo.Repository
{
    public class SinglesRepository: BaseRepository<Core.Entity.Single>, ISinglesRepository
    {
        private readonly IMongoCollection<SetMeals> _setMealContext;

        public SinglesRepository(IOptions<DBSettings> dBSettings) : base(dBSettings)
        {
            Context = Datebase.ContactSingles;
            _setMealContext = Datebase.ContactSetMeals;
        }

        public new async Task<ReturnData<Core.Entity.Single>> UpdateOneAsync(Core.Entity.Single data)
        {
            var r = new ReturnData<Core.Entity.Single>();
            var old = await GetOneAsync(data._id);
            try
            {
                FilterDefinition<Core.Entity.Single> filter = new BsonDocument("_id", data._id);

                await Context.ReplaceOneAsync(filter, data);
                r = await GetOneAsync(data._id);
                // 更新菜谱使用到的内容
                if (old.Data.Price != r.Data.Price)
                {
                    var fi = Builders<SetMeals>.Filter.And(
                        Builders<SetMeals>.Filter.ElemMatch(
                            s => s.Singles,
                            singles => singles.SingleId == r.Data._id)
                    );
                    var up = Builders<SetMeals>.Update
                        .Set("Singles.$.Single.Price", r.Data.Price);
                    var upd = _setMealContext.UpdateManyAsync(fi, up);
                }
            }
            catch (Exception e)
            {
                r.Error = e.Message;
            }

            return r;
        }

        public async Task<PageReturnData<IEnumerable<SingleTables>>> GetTableAsync(QueryParameters queryParameters)
        {
            try
            {
                var count = await Context.CountDocumentsAsync(_ => true);
                SortDefinition<Core.Entity.Single> sort = "{Id: -1}";
                FilterDefinition<Core.Entity.Single> Search = "{}";
                if (queryParameters.Search != null)
                {
                    Search = "{Name: { $regex: '" + queryParameters.Search + "', $options: 'i' } }";
                }
                if (queryParameters.Ordays != null)
                {
                    sort = queryParameters.OrdayOk;
                }
                var list = new List<SingleTables>();
                await Context.Find(Search)
                    .Sort(sort)
                    //.Project(projection)
                    .Skip((queryParameters.PageIndex - 1) * queryParameters.PageSize)
                    .Limit(queryParameters.PageSize).ForEachAsync(f =>
                    {
                        list.Add(new SingleTables
                        {
                            _id = f._id,
                            Name = f.Name,
                            Inventory = f.Inventory,
                            Putaway = f.Putaway,
                            Unit = f.Unit,
                            UnitName = f.UnitName,
                            Price = f.Price
                        });
                    });
                return new PageReturnData<IEnumerable<SingleTables>>()
                    { Data = list, Success = true, Count = count };

            }
            catch (Exception e)
            {
                return new PageReturnData<IEnumerable<SingleTables>>()
                {
                    Success = false,
                    Error = e.Message,
                };
            }
        }
    }
}
