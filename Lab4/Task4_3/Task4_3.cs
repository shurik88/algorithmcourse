using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Task4_3
{
    class Task4_3
    {
        static void Main(string[] args)
        {
            var braces = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' } };
            var openBraces = braces.Keys.ToList();
            var closedBraces = braces.Values.ToList();

            using (var reader = new StreamReader("input.txt"))
            {
                var size = reader.ReadLine();//commands count
                if (size == null)
                    throw new ArgumentException("Invalid file format");
                string line;
                using (var writer = new StreamWriter("output.txt"))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        bool hasError = false;
                        var stack = new CustomStack<char>(line.Length);
                        for(var i = 0; i < line.Length; ++i)
                        {
                            if (line[i] == '(' || line[i] == '[')
                                stack.Push(line[i]);
                            else if(line[i] == ')' || line[i] == ']')
                            {
                                if (stack.IsEmpty)
                                {
                                    hasError = true;
                                    break;
                                }
                                var last = stack.Peek();
                                if(braces[last] == line[i])
                                    stack.Pop();
                                else
                                {
                                    hasError = true;
                                    break;
                                }

                            }
                            else
                            {
                                throw new ArgumentException(string.Format("Unknown symbol {0}", line[i]));
                            }

                        }
                        if(hasError || !stack.IsEmpty)
                            writer.WriteLine("NO");
                        else
                            writer.WriteLine("YES");
                    }
                    
                }
            }
        }

        public class CustomStack<T>
        {
            private readonly int _maxSize;
            private readonly T[] _data;
            private int _current = EmptyIndex;

            const int EmptyIndex = -1;
            public CustomStack(int maxSize)
            {
                if (maxSize < 1)
                    throw new ArgumentException("Invalid size");
                _maxSize = maxSize;
                _data = new T[maxSize];
            }

            public bool IsEmpty { get { return _current == EmptyIndex; } }

            public T Peek()
            {
                if (_current == EmptyIndex)
                    throw new ArgumentException("Stack is empty");

                return _data[_current];
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
                if (_current == EmptyIndex)
                    throw new ArgumentException("Stack is empty");

                var elem = _data[_current];
                _data[_current] = default(T);
                _current--;

                return elem;
            }
        }
    }
}
