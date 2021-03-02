using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.Domain.Entities.Tree
{
    public class Tree<T> : IEnumerable<T> where T : IComparable
    {
        private TreeNode<T> root { get; set; }

        public bool HasItem(T value)
        {
            if (root == null)
            {
                return false;
            }

            return find(root, value) != null;
        }

        public int GetHeight()
        {
            return getHeight(root);
        }

        public void Insert(T parent, T child)
        {
            if (root == null)
            {
                root = new TreeNode<T>(null, child);
                return;
            }

            var parentNode = find(parent);

            if (parentNode == null)
            {
                throw new ArgumentNullException();
            }

            var exists = find(root, child) != null;

            if (exists)
            {
                throw new ArgumentException("Value already exists");
            }

            parentNode.Children.SortedInsert(new TreeNode<T>(parentNode, child));
        }

        public List<T> GetAllPreviousRecipes(T node)
        {
            if (root == null)
            {
                throw new Exception("Tree doesn't exist.");
            }

            var currentNode = find(node);

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
                currentNode = find(currentNode.Parent.Value);
            }

            return previousRecipesList;
        }

        public void UpdateNode(T node, T data)
        {
            if (root == null)
            {
                throw new Exception("Tree doesn't exist.");
            }

            var parentNode = find(node);

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
            var currentNode = find(data);

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
            delete(root.Value, value);
        }

        public IEnumerable<T> Children(T value)
        {
            return find(value)?.Children.Select(x => x.Value);
        }

        private TreeNode<T> find(T value)
        {
            if (root == null)
            {
                return null;
            }

            return find(root, value);
        }

        private int getHeight(TreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            var currentHeight = -1;

            foreach (var child in node.Children)
            {
                var childHeight = getHeight(child);

                if (currentHeight < childHeight)
                {
                    currentHeight = childHeight;
                }
            }

            currentHeight++;

            return currentHeight;
        }

        private void delete(T parentValue, T value)
        {
            var parent = find(parentValue);

            if (parent == null)
            {
                throw new Exception("Cannot find parent");
            }

            var itemToRemove = find(parent, value);

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

        private TreeNode<T> find(TreeNode<T> parent, T value)
        {
            if (parent.Value.CompareTo(value) == 0)
            {
                return parent;
            }

            foreach (var child in parent.Children)
            {
                var result = find(child, value);

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

    }
}
