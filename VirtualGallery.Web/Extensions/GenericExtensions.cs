using System;
using System.Linq.Expressions;

namespace VirtualGallery.Web.Extensions
{
    public static class GenericExtensions
    {
        public static string GetMemberName<T, TValue>(this T obj, Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
    }
}