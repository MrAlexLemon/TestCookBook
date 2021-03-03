using CookBook.Domain.Entities.SinglyLinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.Domain.Entities.Tree
{
    internal class TreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal TreeNode<T> Parent { get; set; }
        internal SinglyLinkedList<TreeNode<T>> Children { get; set; }

        internal bool IsLeaf => Children.Count() == 0;

        internal TreeNode(TreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;

            Children = new SinglyLinkedList<TreeNode<T>>();
        }

        public int CompareTo(object obj)
        {
            var temp = obj as TreeNode<T>;
            return Value.CompareTo(temp.Value);
        }
    }
}
