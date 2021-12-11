namespace Hjelmqvist.Collections.Generic
{
    public class LinkedListNode<T>
    {
        public T Value { get; set; }
        public LinkedListNode<T> Next { get; private set; }
        public LinkedListNode<T> Previous { get; private set; }
        public LinkedList<T> List { get; private set; }

        public LinkedListNode(T value)
        {
            Value = value;
        }

        internal void SetNext(LinkedListNode<T> node) => Next = node;
        internal void SetPrevious(LinkedListNode<T> node) => Previous = node;

        internal void SetReferences(LinkedListNode<T> next, LinkedListNode<T> previous, LinkedList<T> list)
        {
            Next = next;
            Previous = previous;
            List = list;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}