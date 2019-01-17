using FoodPlan.Core;
using FoodPlan.Core.Entity;
using FoodPlan.DB.Modules;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodPlan.DB.Mongo.IRepository
{
    //public interface IBaseRepository { }
    public interface IBaseRepository<T> where T: IEntityBase
    {
        /// <summary>
        /// 添加一个数据
        /// </summary>
        /// <param name="addData">添加的数据</param>
        /// <returns></returns>
        Task<ReturnData<T>> AddAsync(T addData);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> AllAsync(QueryParameters queryParameters);
        /// <summary>
        /// 根据url获取数据
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        Task<PageReturnData<IEnumerable<object>>> GetAsync(QueryParameters queryParameters);
        /// <summary>
        /// 根据Id获取一条数据
        /// </summary>
        /// <param name="id">数据Guid</param>
        /// <returns></returns>
        Task<ReturnData<T>> GetOneAsync(Guid id);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        Task<DeleteResult> DeleteAsync(Guid id);

        /// <summary>
        /// 修改一条完整的数据
        /// </summary>
        /// <param name="addData">修改的数据</param>
        /// <returns></returns>
        Task<ReturnData<T>> UpdateOneAsync(T addData);

        /// <summary>
        /// 修改一条完整的数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="upDate"></param>
        /// <returns></returns>
        Task<ReturnData<T>> UpdatePropertyAsync(Guid id, UpDateModle upDate);
    }

}
