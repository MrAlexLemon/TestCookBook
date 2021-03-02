using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Entities.SinglyLinkedList
{
    internal class SinglyLinkedListEnumerator<T> : IEnumerator<T>
    {
        internal SinglyLinkedListNode<T> headNode;
        internal SinglyLinkedListNode<T> currentNode;

        internal SinglyLinkedListEnumerator(ref SinglyLinkedListNode<T> headNode)
        {
            this.headNode = headNode;
        }

        public bool MoveNext()
        {
            if (headNode == null)
                return false;

            if (currentNode == null)
            {
                currentNode = headNode;
                return true;
            }

            if (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
                return true;
            }

            return false;

        }

        public void Reset()
        {
            currentNode = headNode;
        }


        object IEnumerator.Current => Current;

        public T Current
        {
            get
            {
                return currentNode.Data;
            }
        }
        public void Dispose()
        {
            headNode = null;
            currentNode = null;
        }

    }
}
