using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hjelmqvist.Collections.Generic
{
    public class LinkedList<T> : ICollection, ICollection<T>, IEnumerable<T>, IReadOnlyCollection<T>//, ISerializable, IDeserializationCallback
    {
        public LinkedListNode<T> First { get; private set; }
        public LinkedListNode<T> Last { get; private set; }
        public int Count { get; private set; }

        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public LinkedList()
        {
            First = null;
            Last = null;
            Count = 0;
        }

        public LinkedList(params T[] values)
        {
            First = null;
            Last = null;
            Count = 0;

            foreach (T value in values)
                AddLast( value );
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();
                var node = First;
                int count = 0;

                while (node != null)
                {
                    if (index == count)
                        return node.Value;
                    node = node.Next;
                    count++;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();
                var node = First;
                int count = 0;

                while (node != null)
                {
                    if (index == count)
                    {
                        node.Value = value;
                        return;
                    }
                    node = node.Next;
                    count++;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public void Add(T item) => AddLast( item );

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            var current = First;
            if (node == Last)
                current = Last;

            while (current != null)
            {
                if (current.Equals( node ))
                {
                    var newNode = new LinkedListNode<T>( value );
                    newNode.SetReferences( current.Next, current, this );

                    current.Next?.SetPrevious( newNode );
                    current.SetNext( newNode );
                    if (current == Last)
                        Last = newNode;
                    Count++;
                    return newNode;
                }
                current = current.Next;
            }
            return null;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            var current = First;
            if (node == Last)
                current = Last;

            while (current != null)
            {
                if (current.Equals( node ))
                {
                    var newNode = new LinkedListNode<T>( value );
                    newNode.SetReferences( current, current.Previous, this );

                    current.Previous?.SetNext( newNode );
                    current.SetPrevious( newNode );
                    if (current == First)
                        First = newNode;
                    Count++;
                    return newNode;
                }
                current = current.Next;
            }
            return null;
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            var newNode = new LinkedListNode<T>( value );

            if (Count == 0)
                First = Last = newNode;
            else
            {
                First.SetReferences( First.Next, newNode, this );
                newNode.SetReferences( First, null, this );
                First = newNode;
            }
            Count++;
            return newNode;
        }

        public LinkedListNode<T> AddLast(T value)
        {
            var newNode = new LinkedListNode<T>( value );

            if (Count == 0)
                First = Last = newNode;
            else
            {
                Last.SetReferences( newNode, Last.Previous, this );
                newNode.SetReferences( null, Last, this );
                Last = newNode;
            }
            Count++;
            return newNode;
        }

        public void Clear()
        {
            // :thinking:
            First = null;
            Last = null;
            Count = 0;
        }

        public bool Contains(T value)
        {
            var node = First;

            while (node != null)
            {
                if (node.Value.Equals( value ))
                    return true;
                node = node.Next;
            }

            return false;
        }

        public void CopyRange(ref T[] target, int index) // bonus
        {
            T[] newArray = new T[target.Length + Count];

            for (int i = 0, j = 0; i < target.Length; i++)
            {
                if (i == index)
                {
                    var current = First;
                    while (current != null)
                    {
                        newArray[i + j] = current.Value;
                        current = current.Next;
                        j++;
                    }
                }
                newArray[i + j] = target[i];
            }
            target = newArray;
        }

        public void CopyTo(T[] target, int index)
        {
            var current = First;
            for (int i = index; i < target.Length; i++)
            {
                if (current != null)
                {
                    target[i] = current.Value;
                    current = current.Next;
                }
            }
        }

        public void CopyTo(Array array, int index)
        {
            var current = First;
            for (int i = index; i < array.Length; i++)
            {
                if (current != null)
                {
                    array.SetValue( current.Value, i );
                    current = current.Next;
                }
            }
        }

        public LinkedListNode<T> Find(T value)
        {
            var current = First;

            while (current != null)
            {
                if (current.Value.Equals( value ))
                    return current;
                current = current.Next;
            }
            return null;
        }

        public LinkedListNode<T> FindLast(T value)
        {
            var current = Last;

            while (current != null)
            {
                if (current.Value.Equals( value ))
                    return current;
                current = current.Previous;
            }
            return null;
        }

        public int IndexOf(T value) // bonus
        {
            var current = First;
            int count = 0;

            while (current != null)
            {
                if (current.Value.Equals( value ))
                    return count;
                current = current.Next;
                count++;
            }
            return -1;
        }

        public void Remove(LinkedListNode<T> node)
        {
            if (node == First)
            {
                RemoveFirst();
                return;
            }
            else if (node == Last)
            {
                RemoveLast();
                return;
            }

            var previous = First;
            var current = First;

            while (current != null)
            {
                if (current.Equals( node ))
                {
                    current.Next.SetPrevious( previous );
                    previous.SetNext( current.Next );
                    Count--;
                    return;
                }
                previous = current;
                current = current.Next;
            }
        }

        public bool Remove(T value)
        {
            var previous = First;
            var current = First;

            while (current != null)
            {
                if (current.Value.Equals( value ))
                {
                    if (current == First)
                        RemoveFirst();
                    else if (current == Last)
                        RemoveLast();
                    else
                    {
                        current.Next.SetPrevious( previous );
                        previous.SetNext( current.Next );
                        Count--;
                    }
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        public void RemoveAt(int index) // bonus
        {
            if (index == 0)
            {
                RemoveFirst();
                return;
            }
            else if (index == Count - 1)
            {
                RemoveLast();
                return;
            }

            var previous = First;
            var current = First;
            int count = 0;

            while (current != null)
            {
                if (count == index)
                {
                    previous.SetNext( current.Next );
                    Count--;
                    return;
                }
                previous = current;
                current = current.Next;
                count++;
            }
        }

        public void RemoveFirst()
        {
            if (First != null)
            {
                First.Next.SetPrevious( null );
                First = First.Next;
                Count--;
            }
        }

        public void RemoveLast()
        {
            if (Last != null)
            {
                Last.Previous.SetNext( null );
                Last = Last.Previous;
                Count--;
            }
        }

        public T[] ToArray()
        {
            T[] array = new T[Count];
            int count = 0;
            var current = First;
            while (current != null)
            {
                array[count] = current.Value;
                count++;
                current = current.Next;
            }
            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnDeserialization(object sender)
        //{
        //    throw new NotImplementedException();
        //}
    }
}