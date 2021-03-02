using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Entities.Tree
{
    internal class TreeEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private readonly TreeNode<T> root;
        private Stack<TreeNode<T>> progress;

        internal TreeEnumerator(TreeNode<T> root)
        {
            this.root = root;
        }

        public bool MoveNext()
        {
            if (root == null)
            {
                return false;
            }

            if (progress == null)
            {
                progress = new Stack<TreeNode<T>>(root.Children);
                Current = root.Value;
                return true;
            }

            if (progress.Count > 0)
            {
                var next = progress.Pop();
                Current = next.Value;

                foreach (var child in next.Children)
                {
                    progress.Push(child);
                }

                return true;
            }

            return false;
        }

        public void Reset()
        {
            progress = null;
            Current = default;
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            progress = null;
        }
    }
}
