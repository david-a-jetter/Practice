using System;
using System.Collections;
using System.Collections.Generic;

namespace Practice.DataStructures
{
    public class DoublyLinkedList<T> : IEnumerable<DoublyLinkedNode<T>>
    {
        public DoublyLinkedNode<T> First { get; private set; }

        public DoublyLinkedNode<T> Last  { get; private set; }

        public int Count { get; private set; }

        private LinkedList<T> _Example;

        public DoublyLinkedList() { }

        public DoublyLinkedList(IEnumerable<T> nodes)
        {
            if (nodes is null) throw new ArgumentNullException(nameof(nodes));

            foreach(var node in nodes)
            {
                Append(node);
            }
        }

        public void Append(T value)
        {
            PracticeExtensions.ThrowIfNull(value, nameof(value));

            if (First is null)
            {
                SetFirstNode(value);
            }
            else
            {
                Last = Last.LinkAfter(value);
            }

            Count++;
        }

        public void Insert(T value)
        {
            PracticeExtensions.ThrowIfNull(value, nameof(value));

            if (First is null)
            {
                SetFirstNode(value);
            }
            else
            {
                First = First.LinkBefore(value);
            }

            Count++;
        }

        public void Replace(DoublyLinkedNode<T> node, T value)
        {
            if (node is null) throw new ArgumentNullException(nameof(node));

            PracticeExtensions.ThrowIfNull(value, nameof(value));

            var nodeIsFirst = ReferenceEquals(First, node);
            var nodeIsLast  = ReferenceEquals(Last, node);

            if (nodeIsFirst)
            {
                var newNode = First.Replace(value);
                First = newNode;
            }

            if (nodeIsLast)
            {
                var newNode = Last.Replace(value);
                Last = newNode;
            }
            
            if (!nodeIsFirst && !nodeIsLast)
            {
                node.Replace(value);
            }
        }

        public IEnumerator<DoublyLinkedNode<T>> GetEnumerator()
        {
            foreach(var node in GetAllNodes())
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void SetFirstNode(T newValue)
        {
            First = new DoublyLinkedNode<T>(newValue);
            Last = First;
        }

        private IEnumerable<DoublyLinkedNode<T>> GetAllNodes()
        {
            var current = First;

            while (current != null)
            {
                yield return current;

                current = current.Next;
            }
        }
    }
}
