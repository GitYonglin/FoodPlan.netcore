using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.Core.Entity
{
    public class ShoppingCar: IEntityBase
    {
        public Guid _id { get; set; }
        /// <summary>
        /// 购物车商品状态 true=套餐商品 false=单品
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 套餐商品
        /// </summary>
        public SetMeals SetMeals { get; set; }
        /// <summary>
        /// 单类商品
        /// </summary>
        public Single Single { get; set; }
    }
}
