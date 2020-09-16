using System;
using System.Collections;
using System.Collections.Generic;

namespace Endpoints.Framework.Extensions
{
    public static class ArrayExtensions
    {
        public static void ForEach<T>(this T[] arr, Action<T> action)
        {
            foreach (T data in arr)
            {
                action(data);
            }
        }

        public static void ForEach<T>(this T[] arr, Action<int, T> action)
        {
            int i = 0;
            foreach (T data in arr)
            {
                action(i, data);
            }
        }

        public static T[] Filter<T>(this T[] arr, Func<T, bool> action)
        {
            List<T> _list = new List<T>();
            foreach (T data in arr)
            {
                if (action(data))
                {
                    _list.Add(data);
                }
            }
            return _list.ToArray();
        }
    }
}
