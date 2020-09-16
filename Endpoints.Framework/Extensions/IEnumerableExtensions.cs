using System;
using System.Collections.Generic;

namespace Endpoints.Framework.Extensions
{
    public static class IEnumerableExtensions
    {
        public static List<TResult> Map<T, TResult>(this IEnumerable<T> list, Func<T, TResult> action)
        {
            List<TResult> _list = new List<TResult>();
            foreach (T data in list)
            {
                _list.Add(action(data));
            }
            return _list;
        }
    }
}
