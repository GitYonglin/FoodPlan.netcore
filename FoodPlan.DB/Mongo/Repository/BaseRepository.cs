using FoodPlan.Core;
using FoodPlan.Core.Entity;
using FoodPlan.DB.Mongo.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using FoodPlan.DB.Modules;

namespace FoodPlan.DB.Mongo.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : IEntityBase
    {
        /// <summary>
        /// 文档
        /// </summary>
        protected IMongoCollection<T> _context;
        /// <summary>
        /// 数据库
        /// </summary>
        protected readonly MongoContextService _datebase;
        /// <summary>
        /// 构成函数
        /// </summary>
        /// <param name="dBSettings">数据库连接字符串</param>
        /// 注意：IOptions 是通过依赖注入到 .net core 应用时获取到的
        public BaseRepository(IOptions<DBSettings> dBSettings)
        {
            _datebase = new MongoContextService(dBSettings);
        }
  
        public async Task<ReturnData<T>> AddAsync(T data)
        {
            try
            {
                await _context.InsertOneAsync(data);
                return new ReturnData<T>() { Data = data, Success = true };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ReturnData<T>() { Success = false, Error = e.Message };
                throw;
            }
        }

        public async Task<IEnumerable<T>> AllAsync(QueryParameters queryParameters)
        {
            //SortDefinition<T> sort = "{Unit: 1, Min: -1}";
            ////ProjectionDefinition<T, T> projection = "{ Id: 1, Unit: 1 }";
            ////var projection = Builders<T>.Projection.Include("X").Include("Y").Exclude("Id");
            //var projection = Builders<T>.Projection.Expression(x => new { X = x.Id});
            //return await _context.Find(_ => true)
            //        .Sort(sort)
            //        .Skip(queryParameters.PageIndex * queryParameters.PagSize)
            //        .Limit(queryParameters.PagSize).ToListAsync();
            return await _context.Find(_ => true)
                    .SortBy(s => s.Id)
                    .Skip(queryParameters.PageIndex * queryParameters.PagSize)
                    .Limit(queryParameters.PagSize).ToListAsync();
        }

        public async Task<DeleteResult> DeleteAsync(Guid id)
        {
            return await _context.DeleteOneAsync(filter => filter.Id == id);
        }

        public async Task<T> GetOneAsync(Guid id)
        {
            try
            {
                //var projection = Builders<T>.Projection.Exclude("_id");
                //var document = _context.Find(f => f.Id == id).Project(projection).First();
                //var documenta = await _context.Find(f => f.Id == id).Project(projection).FirstAsync();
                return await _context.Find(f => f.Id == id).FirstAsync();
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public Task UpdateOneAsync(T addData)
        {
            throw new NotImplementedException();
        }

        public async Task<PageReturnData<IEnumerable<object>>> GetAsync(QueryParameters queryParameters)
        {
            try
            {
                var count = await _context.CountDocumentsAsync(_ => true);
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
                await _context.Find(_ => true)
                        .Sort(sort)
                        .Project(projection)
                        .Skip(queryParameters.PageIndex * queryParameters.PagSize)
                        .Limit(queryParameters.PagSize).ForEachAsync(f =>
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
    }
}
