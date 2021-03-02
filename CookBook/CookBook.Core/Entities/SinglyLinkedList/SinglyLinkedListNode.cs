using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Entities.SinglyLinkedList
{
    public class SinglyLinkedListNode<T>
    {
        public SinglyLinkedListNode<T> Next;
        public T Data;

        public SinglyLinkedListNode(T data)
        {
            Data = data;
        }
    }
}
