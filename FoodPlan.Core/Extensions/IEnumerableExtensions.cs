using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace FoodPlan.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ToDynamicIEnumerable<TSource>(
            this IEnumerable<TSource> source,
            out string error,
            string fields = null
            )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var expandoObjectList = new List<ExpandoObject>();
            var fieldsAfterSplit = fields.Split(',').ToList();

            foreach (TSource sourceObject in source)
            {
                var dataShapedObject = sourceObject.ToDynamicOnject(out string err, fieldsAfterSplit);
                if (err != null)
                {
                    error = err;
                    return null;
                }
                expandoObjectList.Add(dataShapedObject);
            }
            error = null;
            return expandoObjectList;
        }
    }
}
