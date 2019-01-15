using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace FoodPlan.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ToDynamicOnject<Object>(
            this Object source,
            out string error,
            List<string> fieldsAfterSplit
            )
        {
            var dataShapedObject = new ExpandoObject();
            var Ts = source.GetType();
            foreach (var key in fieldsAfterSplit)
            {
                try
                {
                    //var ukey = $"{key.Substring(0, 1).ToUpper()}{key.Substring(1)}";
                    var ukey = key.ToUpperInitial();
                    var propertyValue = Ts.GetProperty(ukey).GetValue(source, null);

                    ((IDictionary<string, object>)dataShapedObject).Add(ukey, propertyValue);
                }
                catch (Exception)
                {
                    error = $"Property {key} wasn't found on {Ts}";
                    return null;
                    //throw new Exception($"Property {key} wasn't found on {typeof(TSource)}");
                }
            }
            error = null;
            return dataShapedObject;
        }
    }
}
