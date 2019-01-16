using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.Core.Entity
{
    public class SetMeals: IEntityBase
    {
        public Guid _id { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 套餐描述
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 展示图片地址
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 剂量
        /// </summary>
        public int Unit { get; set; }
        /// <summary>
        /// 剂量单位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Inventory { get; set; }
        /// <summary>
        /// 烹饪时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
        public Select Select { get; set; }
    }

    public class Select
    {
        /// <summary>
        /// 主料
        /// </summary>
        public IEnumerable<SelectSingles> Main { get; set; }
        /// <summary>
        /// 辅料
        /// </summary>
        public IEnumerable<SelectSingles> Assist { get; set; }
        /// <summary>
        /// 酱料
        /// </summary>
        public IEnumerable<SelectSingles> Sauce { get; set; }
        /// <summary>
        /// 使用工具
        /// </summary>
        public IEnumerable<SelectSingles> Tools { get; set; }
        /// <summary>
        /// 购物用选择状态
        /// </summary>
        public bool Check { get; set; }
        /// <summary>
        /// 购物用选择数量
        /// </summary>
        public int Number { get; set; }

    }
    public class SelectSingles
    {
        /// <summary>
        /// 食材id
        /// </summary>
        public Guid SingleId { get; set; }
        /// <summary>
        /// 食材
        /// </summary>
        public Single Single { get; set; }
        /// <summary>
        /// 食材使用量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 自备食材状态
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 购物用选择状态
        /// </summary>
        public bool Check { get; set; }
    }
}
