using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Entities.SinglyLinkedList
{
    public class SinglyLinkedList<T> : IEnumerable<T> where T : IComparable
    {
        public SinglyLinkedListNode<T> Head;


        public void SortedInsert(T data)
        {
            var new_node = new SinglyLinkedListNode<T>(data);

            SinglyLinkedListNode<T> current;

            /* Special case for head node */
            if (Head == null || Head.Data.CompareTo(new_node.Data) >=1)
            {
                new_node.Next = Head;
                Head = new_node;
            }
            else
            {
                /* Locate the node before  
                point of insertion. */
                current = Head;

                while (current.Next != null && current.Next.Data.CompareTo(new_node.Data) < 0)
                    current = current.Next;

                new_node.Next = current.Next;
                current.Next = new_node;
            }
        }

        public void InsertFirst(T data)
        {
            var newNode = new SinglyLinkedListNode<T>(data);

            newNode.Next = Head;

            Head = newNode;
        }

        public void InsertLast(T data)
        {
            var newNode = new SinglyLinkedListNode<T>(data);

            if (Head == null)
            {
                Head = new SinglyLinkedListNode<T>(data);
            }
            else
            {
                var current = Head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
            }

        }

        public T DeleteFirst()
        {
            if (Head == null)
            {
                throw new Exception("Nothing to remove");
            }

            var firstData = Head.Data;

            Head = Head.Next;

            return firstData;
        }

        public T DeleteLast()
        {
            if (Head == null)
            {
                throw new Exception("Nothing to remove");
            }

            var current = Head;
            SinglyLinkedListNode<T> prev = null;
            while (current.Next != null)
            {
                prev = current;
                current = current.Next;
            }

            var lastData = prev.Next.Data;
            prev.Next = null;
            return lastData;
        }

        public void Delete(T element)
        {
            if (Head == null)
            {
                throw new Exception("Empty list");
            }

            var current = Head;
            SinglyLinkedListNode<T> prev = null;

            do
            {
                if (current.Data.Equals(element))
                {
                    //last element
                    if (current.Next == null)
                    {
                        //head is the only node
                        if (prev == null)
                        {
                            Head = null;
                        }
                        else
                        {
                            //last element
                            prev.Next = null;
                        }
                    }
                    else
                    {
                        //current is head
                        if (prev == null)
                        {
                            Head = current.Next;
                        }
                        else
                        {
                            //delete
                            prev.Next = current.Next;
                        }
                    }

                    break;
                }

                prev = current;
                current = current.Next;
            }
            while (current != null);
        }

        public bool IsEmpty() => Head == null;

        public void Clear()
        {
            if (Head == null)
            {
                throw new Exception("Empty list");
            }

            Head = null;
        }

        public void InsertFirst(SinglyLinkedListNode<T> current)
        {
            current.Next = Head;
            Head = current;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SinglyLinkedListEnumerator<T>(ref Head);
        }
    }
}
