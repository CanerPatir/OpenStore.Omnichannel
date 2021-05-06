using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OpenStore.Omnichannel.Panel.Components.DataGrid
{
    internal static class GridUtils
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberExpression = TryGetMemberExpression(propertyExpression);

            var member = (PropertyInfo) memberExpression.Member;
            // var propertyType = member.PropertyType;
            // var name = member.Name;
            var strArray = memberExpression.ToString().Split('.');
            return strArray.Length > 2 ? string.Join(".", strArray.Skip(1)) : memberExpression.Member.Name;
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> source, Expression<Func<T, object>> propertyExpression, string term)
        {
            var memberExpression = TryGetMemberExpression(propertyExpression);
            var member = (PropertyInfo) memberExpression.Member;

            return source.Where(m => member.GetValue(m, null).ToString().IgnoreCaseContains(term));
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> source, IEnumerable<Expression<Func<T, object>>> propertyExpressions, string term)
        {
            var members = propertyExpressions.Select(propertyExpression =>
            {
                var memberExpression = TryGetMemberExpression(propertyExpression);
                var member = (PropertyInfo) memberExpression.Member;

                return member;
            });

            return source.Where(m => members.Any(member => member.GetValue(m, null).ToString().IgnoreCaseContains(term)));
        }

        private static bool IgnoreCaseContains(this string str, string term) => CultureInfo.InvariantCulture.CompareInfo.IndexOf(str, term, CompareOptions.IgnoreCase) >= 0;

        private static MemberExpression TryGetMemberExpression<T>(Expression<Func<T, object>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression memberExpression) return memberExpression;

            try
            {
                memberExpression = ((UnaryExpression) propertyExpression.Body).Operand as MemberExpression;
            }
            catch (InvalidCastException ex)
            {
                throw new Exception("Expression is not a MemeberExpression", ex);
            }

            return memberExpression;
        }
    }
}