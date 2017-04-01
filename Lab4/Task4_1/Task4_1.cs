using System;
using System.IO;

namespace Lab4.Task4_1
{
    class Task4_1
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var size = reader.ReadLine();//commands count
                if (size == null)
                    throw new ArgumentException("Invalid file format");
                var stack = new CustomStack<int>(Int32.Parse(size));
                string line;
                using (var writer = new StreamWriter("output.txt"))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        var command = line.Split(new[] { ' ' });
                        switch(command.Length)
                        {
                            case 1:
                                if(command[0] != "-")
                                    throw new ArgumentException(string.Format("Unknown command arguments: {0}", line));
                                writer.WriteLine(stack.Pop());
                                break;
                            case 2:
                                if (command[0] != "+")
                                    throw new ArgumentException(string.Format("Unknown command arguments: {0}", line));
                                stack.Push(Int32.Parse(command[1]));
                                break;
                            default:
                                throw new ArgumentException(string.Format("Unknown command arguments: {0}", line));
                        }
                    }
                }                    
            }
        }

        public class CustomStack<T>
        {
            private readonly int _maxSize;
            private readonly T[] _data;
            private int _current = -1;
            public CustomStack(int maxSize)
            {
                if (maxSize < 1)
                    throw new ArgumentException("Invalid size");
                _maxSize = maxSize;
                _data = new T[maxSize];
            }

            public void Push(T elem)
            {
                if ((_current + 1) == _maxSize)
                    throw new ArgumentException("Stack overflow");
                _current++;
                _data[_current] = elem;
            }

            public T Pop()
            {
                if (_current < 0)
                    throw new ArgumentException("Stack is empty");

                var elem = _data[_current];
                _data[_current] = default(T);
                _current--;

                return elem;
            }
        }
    }
}
