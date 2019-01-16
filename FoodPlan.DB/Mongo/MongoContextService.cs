using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FoodPlan.DB.Mongo
{
    public class MongoContextService
    {
        private readonly IMongoDatabase _datebase;
        //private delegate void SetBsonClassMap();
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="dBSettings">.ner core 设置的链接字符串</param>
        public MongoContextService(IOptions<DBSettings> dBSettings)
        {
            var client = new MongoClient(dBSettings.Value.ConnectionString);
            if (client != null)
            {
                _datebase = client.GetDatabase(dBSettings.Value.Database);
            }

        }
        /// <summary>
        /// 判断文档是否存在 不存在创建
        /// </summary>
        /// <param name="CollectionName">文档名称</param>
        /// <param name="setBsonClassMap">首次创建文档字段映射与约束设置</param>
        private void CheckAndCreateCollection(string CollectionName, Action setBsonClassMap, Action CreateIndex)
        {
            // 获取数据库中的所有文档
            var collectionList = _datebase.ListCollections().ToList();
            // 保存文档名称
            var collectionNames = new List<string>();
            // 便利获取文档名称
            collectionList.ForEach(b => collectionNames.Add(b["name"].AsString));
            // 判断文档是否存在
            if (!collectionNames.Contains(CollectionName))
            {
                // 首次创建文档字段映射与约束设置
                setBsonClassMap();
                // 创建文档
                _datebase.CreateCollection(CollectionName);
                // 创建索引
                CreateIndex();


            }
        }
        /// <summary>
        /// 单品文档
        /// </summary>
        public IMongoCollection<Core.Entity.Single> ContactSingles
        {
            get
            {
                CheckAndCreateCollection("Singles", SetBsonClassMapSingles, CreateIndexSingles<Core.Entity.Single>);
                return _datebase.GetCollection<Core.Entity.Single>("Singles");
                
            }
        }
        /// <summary>
        /// 单品文档字段映射
        /// </summary>
        private void SetBsonClassMapSingles()
        {
            BsonClassMap.RegisterClassMap((BsonClassMap<Core.Entity.Single> cm) =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x._id).SetIdGenerator(CombGuidGenerator.Instance).SetElementName("id"); // 使用Guid作为文档id
            });
        }
        // <summary>
        /// 单品文档字段映射
        /// </summary>
        private void CreateIndexSingles<T>() where T: Core.Entity.Single
        {
            var context = _datebase.GetCollection<T>("Singles");
            var unique = new CreateIndexOptions()
            {
                Unique = true, //name不能重复
            };
            var builder = Builders<T>.IndexKeys;
            IndexKeysDefinition<T> nameKey = builder.Ascending(d => d.Name);
            var ie = new List<CreateIndexModel<T>>
            {
                new CreateIndexModel<T>(nameKey, unique),
            };
            context.Indexes.CreateManyAsync(ie);
        }
    }
}
