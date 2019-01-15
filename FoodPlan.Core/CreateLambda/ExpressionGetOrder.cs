using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FoodPlan.Core.CreateLambda
{
    public static class ExpressionGetOrder
    {
        //public static Expression<Func<T, TKey>> GetOrderExpression<T, TKey>(
        //    string oedays)
        //{
        //    ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
        //    var fieldsAfterSplit = oedays.Split(',');
        //    List<Expression> ae = new List<Expression>();
        //    foreach (var field in fieldsAfterSplit)
        //    {
        //        var propertyName = field.Trim();
        //        var v = Expression.Property(parameter, propertyName);
        //        ae.Add(v);

        //    }
        //    return Expression.Lambda<Func<T, TKey>>(ae, parameter);
        //}
        /// <summary>
        /// Linq排序扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">属性的字符串名称</param>
        /// <param name="sort">方向</param>
        /// <returns></returns>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string propertyName, SortDirectionEnum sort)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (String.IsNullOrEmpty(propertyName) || propertyName.Trim().Length == 0)
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, String.Empty);
            MemberExpression property = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = (sort == SortDirectionEnum.Ascending) ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);

        }
        /// <summary>
        /// 指定对项列表进行排序的方向。
        /// </summary>
        public enum SortDirectionEnum
        {
            /// <summary>
            /// 从小到大排序。例如，从 A 到 Z。
            /// </summary>
            Ascending = 0,
            /// <summary>
            /// 从大到小排序。例如，从 Z 到 A。
            /// </summary>
            Descending = 1,
        }

    }
}
