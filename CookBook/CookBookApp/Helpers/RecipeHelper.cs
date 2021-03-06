using CookBookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Helpers
{
    public static class RecipeHelper
    {
        public static IEnumerable<T> Expand<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> elementSelector)
        {
            var stack = new Stack<IEnumerator<T>>();
            var e = source.GetEnumerator();
            try
            {
                while (true)
                {
                    while (e.MoveNext())
                    {
                        var item = e.Current;
                        yield return item;
                        var elements = elementSelector(item);
                        if (elements == null) continue;
                        stack.Push(e);
                        e = elements.GetEnumerator();
                    }
                    if (stack.Count == 0) break;
                    e.Dispose();
                    e = stack.Pop();
                }
            }
            finally
            {
                e.Dispose();
                while (stack.Count != 0) stack.Pop().Dispose();
            }
        }

        public static IEnumerable<Recipe> Ordered(this IEnumerable<Recipe> source, Func<IEnumerable<Recipe>, IEnumerable<Recipe>> order = null)
        {
            if (order == null) order = items => items.OrderBy(item => item.Name);
            return order(source.Where(item => item.ParentId == null))
                .Expand(item => item.Children != null && item.Children.Any() ? order(item.Children) : null);
        }
    }
}
