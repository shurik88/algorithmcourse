using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Task4_2
{
    class Task4_2
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var size = reader.ReadLine();//commands count
                if (size == null)
                    throw new ArgumentException("Invalid file format");
                var queue = new TailableQueue<int>(Int32.Parse(size));
                string line;
                using (var writer = new StreamWriter("output.txt"))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        var command = line.Split(new[] { ' ' });
                        switch (command.Length)
                        {
                            case 1:
                                if (command[0] != "-")
                                    throw new ArgumentException(string.Format("Unknown command arguments: {0}", line));
                                writer.WriteLine(queue.Dequeue());
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

        public class TailableQueue<T>
        {
            private readonly int _maxSize;
            private readonly T[] _data;
            private int _head = -1;
            private int _tail = 0;
            private bool _overflow = false;
            public TailableQueue(int maxSize)
            {
                if (maxSize < 1)
                    throw new ArgumentException("Invalid size");
                _maxSize = maxSize;
                _data = new T[maxSize];
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

                return elem;
            }
        }
    }
}
