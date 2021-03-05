using CookBook.Domain.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.Domain.Entities.Tree
{
    public class Tree<T> : IEnumerable<T>, IComparable where T : IComparable
    {
        private TreeNode<T> root { get; set; }

        public bool HasItem(T value)
        {
            if (root == null)
            {
                return false;
            }

            return Find(root, value) != null;
        }

        public int GetHeight()
        {
            return GetHeight(root);
        }

        public void Insert(T parent, T child)
        {
            if (root == null)
            {
                root = new TreeNode<T>(null, child);
                return;
            }

            var parentNode = Find(parent);

            if (parentNode == null)
            {
                throw new ArgumentNullException();
            }

            var exists = Find(root, child) != null;

            if (exists)
            {
                throw new ArgumentException("Value already exists");
            }

            parentNode.Children.SortedInsert(new TreeNode<T>(parentNode, child));
        }


        public IEnumerable<TreeNodeDto<T>> GetRootInfo()
        {
            if (root == null)
            {
                throw new Exception("Tree doesn't exist.");
            }

            return new TreeNodeDto<T>[] { new TreeNodeDto<T>{ Id = root.Id, Value = root.Value, IsLeaf = root.IsLeaf, Children = root.Children?.Select(y => new TreeNodeDto<T> { Id = y.Id, Value = y.Value }) } };
        }

        public List<T> GetAllPreviousRecipes(T node)
        {
            if (root == null)
            {
                throw new Exception("Tree doesn't exist.");
            }

            var currentNode = Find(node);

            if (currentNode == null)
            {
                throw new ArgumentNullException();
            }

            List<T> previousRecipesList = new List<T>();

            while (currentNode != null)
            {
                previousRecipesList.Add(currentNode.Value);
                if (currentNode.Parent is null)
                    break;
                currentNode = Find(currentNode.Parent.Value);
            }

            return previousRecipesList;
        }

        public void UpdateNode(T node, T data)
        {
            if (root == null)
            {
                throw new Exception("Tree doesn't exist.");
            }

            var parentNode = Find(node);

            if (parentNode == null)
            {
                throw new ArgumentNullException();
            }

            if (HasItem(data))
            {
                throw new ArgumentException("Value already exists.");
            }

            if (parentNode.Parent == null)
            {
                parentNode.Value = data;
                return;
            }

            parentNode.Parent.Children.SortedInsert(new TreeNode<T>(parentNode.Parent, data));
            var childrenNodes = parentNode.Children.ToList();
            var currentNode = Find(data);

            foreach (var item in childrenNodes)
            {
                item.Parent = currentNode;
                currentNode.Children.InsertLast(item);
            }

            parentNode.Children = new SinglyLinkedList.SinglyLinkedList<TreeNode<T>>();
            Delete(parentNode.Value);
        }

        public void Delete(T value)
        {
            Delete(root.Value, value);
        }

        public IEnumerable<T> Children(T value)
        {
            return Find(value)?.Children.Select(x => x.Value);
        }

        public IEnumerable<TreeNodeDto<T>> GetChildrenNodeDto(T value)
        {
            return Find(value)?.Children.Select(x => new TreeNodeDto<T>{ Id = x.Id, Value = x.Value, Parent = new TreeNodeDto<T>{ Id = x.Parent.Id, Value = x.Parent.Value }, IsLeaf = x.IsLeaf, Children = x.Children?.Select(y => new TreeNodeDto<T> { Id = y.Id, Value = y.Value }) });
        }

        private TreeNode<T> Find(T value)
        {
            if (root == null)
            {
                return null;
            }

            return Find(root, value);
        }

        private int GetHeight(TreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            var currentHeight = -1;

            foreach (var child in node.Children)
            {
                var childHeight = GetHeight(child);

                if (currentHeight < childHeight)
                {
                    currentHeight = childHeight;
                }
            }

            currentHeight++;

            return currentHeight;
        }

        private void Delete(T parentValue, T value)
        {
            var parent = Find(parentValue);

            if (parent == null)
            {
                throw new Exception("Cannot find parent");
            }

            var itemToRemove = Find(parent, value);

            if (itemToRemove == null)
            {
                throw new Exception("Cannot find item");
            }

            //if item is root
            if (itemToRemove.Parent == null)
            {
                if (itemToRemove.Children.Count() == 0)
                {
                    root = null;
                }
                else
                {
                    if (itemToRemove.Children.Count() == 1)
                    {
                        root = itemToRemove.Children.DeleteFirst();
                        root.Parent = null;
                    }
                    else
                    {
                        throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                    }
                }

            }
            else
            {
                if (itemToRemove.Children.Count() == 0)
                {
                    itemToRemove.Parent.Children.Delete(itemToRemove);
                }
                else
                {
                    if (itemToRemove.Children.Count() == 1)
                    {
                        var orphan = itemToRemove.Children.DeleteFirst();
                        orphan.Parent = itemToRemove.Parent;

                        itemToRemove.Parent.Children.SortedInsert(orphan);
                        itemToRemove.Parent.Children.Delete(itemToRemove);
                    }
                    else
                    {
                        throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                    }
                }
            }
        }

        private TreeNode<T> Find(TreeNode<T> parent, T value)
        {
            if (parent.Value.CompareTo(value) == 0)
            {
                return parent;
            }

            foreach (var child in parent.Children)
            {
                var result = Find(child, value);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TreeEnumerator<T>(root);

        }

        public int CompareTo(object obj)
        {
            var temp = obj as Tree<T>;
            return BfsCompare(root.Value, temp);
        }


        private int BfsCompare(T data1, Tree<T> data2)
        {
            var node1 = Find(data1);

            var theQueue1 = new Queue<TreeNode<T>>();
            theQueue1.Enqueue(node1);

            var node2 = data2.root;

            var theQueue2 = new Queue<TreeNode<T>>();
            theQueue2.Enqueue(node2);

            while (theQueue1.Count > 0 && theQueue2.Count > 0)
            {
                var n1 = theQueue1.Dequeue();
                var n2 = theQueue2.Dequeue();

                if (n1.Value.CompareTo(n2.Value) < 0)
                {
                    return -1;
                }
                else if (n1.Value.CompareTo(n2.Value) > 0)
                {
                    return 1;
                }

                foreach (var child in n1.Children)
                {
                    theQueue1.Enqueue(child);
                }

                foreach (var child in n2.Children)
                {
                    theQueue2.Enqueue(child);
                }
            }

            return 0;
        }
    }
}
