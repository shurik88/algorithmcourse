using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Task4_4
{
    class Task4_4
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var size = reader.ReadLine();//commands count
                if (size == null)
                    throw new ArgumentException("Invalid file format");
                var queue = new OrderedTailableQueueWithStack<int>(Int32.Parse(size));
                string line;
                using (var writer = new StreamWriter("output.txt"))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        var command = line.Split(new[] { ' ' });
                        switch (command.Length)
                        {
                            case 1:
                                if (command[0] == "-")
                                    queue.Dequeue();
                                else if (command[0] == "?")
                                    writer.WriteLine(queue.Min);
                                else
                                    throw new ArgumentException(string.Format("Unknown command arguments: {0}", line));                               

                                break;
                            case 2:
                                if (command[0] != "+")
                                    throw new ArgumentException(string.Format("Unknown command arguments: {0}", line));
                                queue.Enqueue(Int32.Parse(command[1]));
                                break;
                            default:
                                throw new ArgumentException(string.Format("Unknown command arguments: {0}", line));
                        }
                    }
                }
            }
        }

        public class Item<T>
        {
            public T Key { get; set; }

            public T Min { get; set; }
        }

        public class OrderedStack<T>
            where T: IComparable
        {
            
            private readonly int _maxSize;
            private readonly Item<T>[] _data;
            private int _current = EmptyIndex;
            const int EmptyIndex = -1;
            public OrderedStack(int maxSize)
            {
                if (maxSize < 1)
                    throw new ArgumentException("Invalid size");
                _maxSize = maxSize;
                _data = new Item<T>[maxSize];
            }

            public void Push(T elem)
            {
                if ((_current + 1) == _maxSize)
                    throw new ArgumentException("Stack overflow");
                var min = _current == EmptyIndex ? elem : _data[_current].Min;
                _current++;
                _data[_current] = new Item<T> { Key = elem, Min = elem.CompareTo(min) > 0 ? min : elem  };
            }

            public T Min
            {
                get
                {
                    return Peek().Min;
                }
            }

            public Item<T> Pop()
            {
                if (_current == EmptyIndex)
                    throw new ArgumentException("Stack is empty");

                var elem = _data[_current];
                _data[_current] = default(Item<T>);
                _current--;

                return elem;
            }

            public bool IsEmpty { get { return _current == EmptyIndex; } }

            public Item<T> Peek()
            {
                if (IsEmpty)
                    throw new ArgumentException("Stack is empty");

                return _data[_current];
            }
        }

        public class OrderedTailableQueueWithStack<T>
            where T : IComparable
        {
            private readonly OrderedStack<T> _s1, _s2;
            private readonly int _maxSize;
            public OrderedTailableQueueWithStack(int maxSize)
            {
                if (maxSize < 1)
                    throw new ArgumentException("Invalid size");
                _maxSize = maxSize;
                _s1 = new OrderedStack<T>(maxSize);
                _s2 = new OrderedStack<T>(maxSize);
            }

            public T Min
            {
                get
                {
                    if (_s1.IsEmpty && _s2.IsEmpty)
                        throw new ArgumentException("Queue is empty");
                    else if (_s2.IsEmpty)
                        return _s1.Min;
                    else if (_s1.IsEmpty)
                        return _s2.Min;

                    var min1 = _s1.Min;
                    var min2 = _s2.Min;
                    return min1.CompareTo(min2) > 0 ? min2 : min1;
                }
            }


            public void Enqueue(T elem)
            {
                _s1.Push(elem);
            }

            public T Dequeue()
            {
                if (_s2.IsEmpty && _s1.IsEmpty)
                    throw new ArgumentException("Queue is empty");
                if (_s2.IsEmpty)
                {
                    while(!_s1.IsEmpty)
                    {
                        var item = _s1.Pop();
                        _s2.Push(item.Key);
                    }
                }
                return _s2.Pop().Key;
            }
        }

        public class LinkedListElem<T>
            where T: IComparable
        {
            public T Key { get; set; }

            public LinkedListElem<T> Next { get; set; }

            public LinkedListElem<T> Prev { get; set; }
        }

        public class OrderedLinkedList<T>
            where T : IComparable
        {
            private readonly int _maxCount;
            private int _count = 0;

            private LinkedListElem<T> _head = null;

            private bool _overflow = false;
             
            public OrderedLinkedList(int maxCount)
            {
                if (maxCount < 1)
                    throw new ArgumentException("Invalid size");
                _maxCount = maxCount;
            }

            public LinkedListElem<T> Search(T key)
            {
                throw new NotImplementedException();
            }

            public void Add(T key)
            {
                if (_count == _maxCount)
                    throw new ArgumentException("List is overflow");

                if (_head == null)
                    _head = new LinkedListElem<T> { Key = key };
                else
                    OrderedAdd(key);

                _count++;
            }

            internal void OrderedAdd(T key)
            {
                var pointer = _head;
                var elem = new LinkedListElem<T> { Key = key };
                while(pointer != null)
                {
                    if (pointer.Key.CompareTo(key) > 0)
                    {
                        if(pointer == _head)
                        {
                            var next = pointer.Next;
                            elem.Next = next;
                            if (next != null)
                                next.Prev = elem;
                            _head = elem;
                        }
                        else
                        {
                            var prev = pointer.Prev;
                            elem.Prev = prev;
                            elem.Next = pointer;
                            prev.Next = elem;
                            pointer.Prev = elem;
                        }                        
                        return;
                    }
                    if(pointer.Next == null)
                    {
                        elem.Prev = pointer;
                        pointer.Next = elem;
                        return;
                    }
                    pointer = pointer.Next;
                }

            }

            public T First
            {
                get
                {
                    if (_head == null)
                        throw new ArgumentException("List is empty");
                    return _head.Key;
                }
            }

            internal void OrderedDelete(T key)
            {
                var pointer = _head;
                while (pointer != null)
                {
                    if (pointer.Key.CompareTo(key) == 0)
                    {
                        if (pointer == _head)
                        {
                            var next = pointer.Next;
                            if (next == null)
                                _head = null;
                            else
                            {
                                next.Prev = null;
                                _head = next;
                            }
                        }
                        else
                        {
                            var prev = pointer.Prev;
                            var next = pointer.Next;
                            prev.Next = next;
                            if (next != null)
                                next.Prev = prev;
                            pointer = null;
                        }
                        break;
                    }
                    pointer = pointer.Next;
                }
            }

            public void Delete(T key)
            {
                if (_head == null)
                    throw new ArgumentException("List is empty");
                OrderedDelete(key);
                _count--;
            }
        }



        public class OrderedTailableQueue<T>
            where T: IComparable
        {
            private readonly int _maxSize;
            private readonly T[] _data;
            private int _head = -1;
            private int _tail = 0;
            private bool _overflow = false;
            private readonly OrderedLinkedList<T> _list;
            public OrderedTailableQueue(int maxSize)
            {
                if (maxSize < 1)
                    throw new ArgumentException("Invalid size");
                _maxSize = maxSize;
                _data = new T[maxSize];
                _list = new OrderedLinkedList<T>(maxSize);
            }

            public T Min
            {
                get
                {
                    return _list.First;
                }
            }


            public void Enqueue(T elem)
            {
                if (_overflow)
                    throw new ArgumentException("Queue is overflow");
                if (_head == -1)
                    _head = 0;
                _data[_tail] = elem;
                _tail++;
                if (_tail == _maxSize)
                    _tail = 0;
                if (_tail == _head)
                    _overflow = true;
                _list.Add(elem);
            }

            public T Dequeue()
            {
                if (_head == -1)
                    throw new ArgumentException("Queue is empty");

                var elem = _data[_head];
                _data[_head] = default(T);
                _head++;
                _overflow = false;
                if (_head == _maxSize)
                    _head = 0;

                _list.Delete(elem);
                return elem;
            }
        }
    }
}
