using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.Domain.Test.v3
{
    public class TreeItem
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public IEnumerable<TreeItem> Children { get; set; }

        public void PrintAllChildren()
        {
            this.PrintAllChildren(0);
        }

        private void PrintAllChildren(int indent)
        {
            Console.WriteLine("{1}Item id: {0}", this.Id, string.Concat(Enumerable.Repeat<int>(0, indent).Select(i => " ")));
            if (this.Children != null)
                foreach (var item in this.Children)
                    item.PrintAllChildren(indent + 1);
        }
    }

    public static class TreeItemExtension
    {
        public static IEnumerable<TreeItem> GetAsTree(this IEnumerable<TreeItem> data)
        {
            var lookup = data.ToLookup(i => i.ParentId);
            return lookup[null].Select(i => {
                i.FillChildren(lookup);
                return i;
            });
        }
        private static TreeItem FillChildren(this TreeItem item, ILookup<int?, TreeItem> lookup)
        {
            item.Children = lookup[item.Id].Select(i => i.FillChildren(lookup));
            return item;
        }

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

        public static IEnumerable<TreeItem> Ordered<T>(this IEnumerable<TreeItem> source, Func<IEnumerable<TreeItem>, IEnumerable<TreeItem>> order = null)
        {
            if (order == null) order = items => items.OrderBy(item => item.Name);
            return order(source.Where(item => item.ParentId == null))
                .Expand(item => item.Children != null && item.Children.Any() ? order(item.Children) : null);
        }

        public static IEnumerable<TreeItem> SortedList(IEnumerable<TreeItem> list = null, int? ParentID = null, ILookup<int?, TreeItem> lookup = null)
        {
            if (lookup == null)
                lookup = list.ToLookup(x => x.ParentId, x => x);

            foreach (var p in lookup[ParentID].OrderBy(x => x.Name))
            {
                yield return p;
                foreach (var c in SortedList(lookup: lookup, ParentID: p.Id))
                    yield return c;
            }
        }

        /*public static void OrderChildren(TreeItem el)
        {
            el.Children = el.Children.OrderBy(x => x.Name).ToList();
            if (el.Children != null)
            {
                foreach (var c in el.Children)
                {
                    OrderChildren(c);
                }
            }
        }*/
    }
}
