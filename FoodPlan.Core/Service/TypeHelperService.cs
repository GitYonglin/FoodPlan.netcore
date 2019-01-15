using FoodPlan.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FoodPlan.Core.Service
{
    public class TypeHelperService: ITypeHelperService
    {
        /// <summary>
        /// 返回映射字段数据处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strs"></param>
        /// <returns></returns>
        public TypeHelperReturnData<string> Projections<T>(string strs)
        {
            if (IsNull(strs))
                return new TypeHelperReturnData<string>() { Success = false };

            var sbstr = new StringBuilder();
            var fieldsAfterSplit = strs.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = IfPropertyNam<T>(field);
                if (propertyName == null)
                {
                    return new TypeHelperReturnData<string>()
                    { Success = true, Error = field };
                }
                sbstr.Append(propertyName + ":1,");
            }
            return new TypeHelperReturnData<string>()
            { Success = false, Ok = "{" + sbstr.ToString() + "}" };
        }

        public bool TypeHasProperties<T>(string strs)
        {
            if (IsNull(strs)) return false;

            var fieldsAfterSplit = strs.Split(',');
            List<string> strList = new List<string>(); 

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.ToUpperInitial();
                strList.Add(propertyName);

                var propertyInfo = typeof(T)
                    .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 排序数处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strs"></param>
        /// <returns></returns>
        public TypeHelperReturnData<string> Orday<T>(string strs)
        {
            if (IsNull(strs))
                return new TypeHelperReturnData<string>(){ Success = true };

            var sbstr = new StringBuilder();
            var fieldsAfterSplit = strs.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var name = field.Split(':');
                var propertyName = IfPropertyNam<T>(name[0]);
                if (propertyName == null)
                {
                    return new TypeHelperReturnData<string>()
                    { Success = true, Error = field };
                }
                var v = name.Length == 1 ? ":1," : ":-1,";
                sbstr.Append($"{propertyName}{v}");
            }
            return new TypeHelperReturnData<string>()
            { Success = false, Ok = "{" + sbstr.ToString() + "}" };
        }
        private bool IsNull(string data)
        {
            return string.IsNullOrWhiteSpace(data);
        }
        private string IfPropertyNam<T> (string name)
        {
            var propertyName = name.ToUpperInitial();
            var propertyInfo = typeof(T)
                .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                return null;
            }
            return propertyName;
        }
    }
}
