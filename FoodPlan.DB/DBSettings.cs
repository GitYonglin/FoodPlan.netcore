using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.DB
{
    /// <summary>
    /// 数据连接字符串
    /// </summary>
    public class DBSettings
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 库名称
        /// </summary>
        public string Database { get; set; }
    }
}
