using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Test
{
    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }
}
