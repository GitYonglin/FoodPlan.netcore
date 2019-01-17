using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace FoodPlan.DB.Service
{
    public static class BsonDocumentDispose
    {
        /// <summary>
        /// 构建更新操作定义 
        /// </summary>
        /// <param name="bc">bsondocument文档</param>
        /// <returns></returns>
        public static List<UpdateDefinition<T>> UpdateDefinitionData<T>(T upData)
        {
            //UpdateDefinition<T> update = "{ $set: { x: 1, y: 3 }, $inc: { z: 1 } }";

            var properties = upData.GetType().GetProperties();
            var bsonlist = new List<string>();
            foreach (var propertyInfo in properties)
            {
                var properName = propertyInfo.Name;
                var value = propertyInfo.GetValue(upData, null);
                bsonlist.Add($"{properName}:{value}");
            }

            var v = JsonConvert.SerializeObject(upData);
            UpdateDefinition<T> up = v;

            return (
                from propertyInfo in properties
                let properName = propertyInfo.Name
                let value = propertyInfo.GetValue(upData, null)
                where value != null
                select new BsonDocument("$set", 
                    new BsonDocument(properName, BsonValue.Create(value))))
                .Select(dummy => (UpdateDefinition<T>) dummy)
                .ToList();
        }
    }
}
