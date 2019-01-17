using FoodPlan.Core;
using FoodPlan.Core.Entity;
using FoodPlan.DB.Mongo.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using FoodPlan.DB.Modules;
using MongoDB.Bson;
using FoodPlan.DB.Service;

// ReSharper disable once IdentifierTypo
namespace FoodPlan.DB.Mongo.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : IEntityBase
    {
        /// <summary>
        /// 文档
        /// </summary>
        protected IMongoCollection<T> Context;
        /// <summary>
        /// 数据库
        /// </summary>
        // ReSharper disable once IdentifierTypo
        protected readonly MongoContextService Datebase;
        /// <summary>
        /// 构成函数
        /// </summary>
        /// <param name="dBSettings">数据库连接字符串</param>
        /// 注意：IOptions 是通过依赖注入到 .net core 应用时获取到的
        protected BaseRepository(IOptions<DBSettings> dBSettings)
        {
            Datebase = new MongoContextService(dBSettings);
        }
  
        public async Task<ReturnData<T>> AddAsync(T data)
        {
            var r = new ReturnData<T>();
            try
            {
                await Context.InsertOneAsync(data);
                r.Data = data;
                r.Success = true;
            }
            catch (Exception e)
            {
                r.Error = e.Message;
            }

            return r;
        }

        public async Task<IEnumerable<T>> AllAsync(QueryParameters queryParameters)
        {
            return await Context.Find(_ => true)
                    .SortBy(s => s._id)
                    .Skip(queryParameters.PageIndex * queryParameters.PageSize)
                    .Limit(queryParameters.PageSize).ToListAsync();
        }

        public async Task<DeleteResult> DeleteAsync(Guid id)
        {
            return await Context.DeleteOneAsync(filter => filter._id == id);
        }

        public async Task<ReturnData<T>> GetOneAsync(Guid id)
        {
            try
            {
                var d =await Context.Find(filter => filter._id == id).FirstAsync();
                return new ReturnData<T>() { Data = d, Success = true };
            }
            catch (Exception e)
            {
                return new ReturnData<T>() { Success = false, Error = e.Message };
            }
        }

        public async Task<ReturnData<T>> UpdateOneAsync(T data)
        {
            var r = new ReturnData<T>();
            try
            {
                FilterDefinition<T> filter = new BsonDocument("_id", data._id);

                await Context.ReplaceOneAsync(filter, data);
                r = await GetOneAsync(data._id);
            }
            catch (Exception e)
            {
                r.Error = e.Message;
            }

            return r;
        }

        public async Task<PageReturnData<IEnumerable<object>>> GetAsync(QueryParameters queryParameters)
        {
            try
            {
                var count = await Context.CountDocumentsAsync(_ => true);
                ProjectionDefinition<T> projection = "{}";
                SortDefinition<T> sort = "{Id: 1}";
                if (queryParameters.Fields != null)
                {
                    projection = queryParameters.FieldOk;
                }
                if (queryParameters.Ordays != null)
                {
                    sort = queryParameters.OrdayOk;
                }
                var list = new List<object>();
                await Context.Find(_ => true)
                        .Sort(sort)
                        .Project(projection)
                        .Skip((queryParameters.PageIndex - 1) * queryParameters.PageSize)
                        .Limit(queryParameters.PageSize).ForEachAsync(f =>
                        {
                            list.Add(BsonSerializer.Deserialize<object>(f));
                        });
                return new PageReturnData<IEnumerable<object>>()
                { Data = list, Success = true, Count = count };

            }
            catch (Exception e)
            {
                return new PageReturnData<IEnumerable<object>>()
                {
                    Success = false,
                    Error = e.Message,
                };
            }
        }

        public async Task<ReturnData<T>> UpdatePropertyAsync(Guid id, UpDateModle date)
        {
            var r = new ReturnData<T>();
            try
            {
                //UpdateDefinition<T> update = "{ $set: { x: 1, y: 3 }, $inc: { z: 1 } }";
                FilterDefinition<T> filter = new BsonDocument("_id", id);
                var updateStr = "{$set:" + date.Set + ", $inc:" + date.Inc + "}";

                if (date.Set != null)
                    updateStr = "{$set:" + date.Set + "}";

                if (date.Inc != null)
                    updateStr = "{$inc:" + date.Inc + "}";

                UpdateDefinition<T> update = updateStr;
                await Context.UpdateOneAsync(filter, update);
                r = await GetOneAsync(id);

            }
            catch (Exception e)
            {
                r.Error = e.Message;
            }
            return r;
        }
    }
}
