using System;
using System.Collections.Generic;

namespace FoodPlan.Core.Entity
{
    public class Single: IEntityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid _id { get; set; }
        /// <summary>
        /// 食材名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 食材价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 食材剂量
        /// </summary>
        public int Unit { get; set; }
        /// <summary>
        /// 剂量名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 最小剂量
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// 最大剂量
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Inventory { get; set; }
        /// <summary>
        /// 上架
        /// </summary>
        public bool Putaway { get; set; }
        /// <summary>
        /// 价格历史
        /// </summary>
        public List<TimePrice> TimePrices { get; set; }
    }
    public class TimePrice
    {
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
    }

    public class SingleTables
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// 上架
        /// </summary>
        public bool Putaway { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Inventory { get; set; }
        /// <summary>
        /// 剂量
        /// </summary>
        public int Unit { get; set; }
        /// <summary>
        /// 剂量单位
        /// </summary>
        public string UnitName { get; set; }
    }
}
