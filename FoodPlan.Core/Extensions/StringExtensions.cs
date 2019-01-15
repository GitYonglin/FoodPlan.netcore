using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static String ToUpperInitial(this String str)
        {
            var TrimStr = str.Trim();
            return $"{TrimStr.Substring(0, 1).ToUpper()}{TrimStr.Substring(1)}";
        }
    }
}
