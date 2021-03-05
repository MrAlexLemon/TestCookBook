using CookBook.Domain.Entities.SinglyLinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.Domain.Dto
{
    public class TreeNodeDto<T> : IComparable where T : IComparable
    {
        public Guid Id { get; set; }
        public T Value { get; set; }

        public bool IsLeaf { get; set; }

        public TreeNodeDto<T> Parent { get; set; }
        public IEnumerable<TreeNodeDto<T>> Children { get; set; }

        public int CompareTo(object obj)
        {
            var temp = obj as TreeNodeDto<T>;
            return Value.CompareTo(temp.Value);
        }
    }
}
