using FoodPlan.Core.Entity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.DB.Mongo
{
    public partial class MongoContextService
    {
        /// <summary>
        /// 单品文档
        /// </summary>
        public IMongoCollection<SetMeals> ContactSetMeals
        {
            get
            {
                CheckAndCreateCollection("SetMeals", SetBsonClassMapSetMeals, CreateIndexSetMeals<SetMeals>);
                return _datebase.GetCollection<SetMeals>("SetMeals");

            }
        }
        /// <summary>
        /// 单品文档字段映射
        /// </summary>
        private void SetBsonClassMapSetMeals()
        {
            BsonClassMap.RegisterClassMap((BsonClassMap<SetMeals> cm) =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x._id).SetIdGenerator(CombGuidGenerator.Instance).SetElementName("id"); // 使用Guid作为文档id
            });
        }
        /// <summary>
        /// 单品文档字段映射
        /// </summary>
        private void CreateIndexSetMeals<T>() where T : SetMeals
        {
            var context = _datebase.GetCollection<T>("SetMeals");
            var unique = new CreateIndexOptions()
            {
                Unique = true, //name不能重复
            };
            var builder = Builders<T>.IndexKeys;
            var nameKey = builder.Ascending(d => d.Name);
            var ie = new List<CreateIndexModel<T>>
            {
                new CreateIndexModel<T>(nameKey, unique),
            };
            context.Indexes.CreateManyAsync(ie);
        }
    }
}
