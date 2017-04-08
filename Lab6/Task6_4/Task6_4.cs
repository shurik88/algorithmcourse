using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab6.Task6_4
{
    public class Task6_4
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt").ToList();

            _fileSize = Int32.Parse(content[0]);
            if(_fileSize == 0)
            {
                File.WriteAllText("output.txt", "0");
                return;
            }

            _treeDef = content.GetRange(0, _fileSize + 1).ToArray();
            _array = Enumerable.Repeat(NullValue, (int)Math.Pow(2, Math.Min(25, _fileSize) - 1)).ToArray();
            ParseTree(0, 1);
            var commands = content[_fileSize + 2].Split(new[] { ' '}).Select(x => Int32.Parse(x));
            using (var output = new StreamWriter("output.txt"))
            {
                try
                {
                    foreach (var com in commands)
                    {
                        var indexToDelete = GetIndex(com);
                        if (indexToDelete == -1)
                            output.WriteLine(_size.ToString());
                        else
                        {
                            DeleteSubTree(indexToDelete);
                            output.WriteLine(_size.ToString());
                        }
                    }
                }
                catch(Exception e)
                {
                    var st = new StackTrace(e, true);

                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    //var line = frame.GetMethod().Name.GetFileLineNumber();
                    output.WriteLine(frame.GetMethod().Name);
                }
                
            }
                

            //var treeToDelete = new BinaryTreeToDelete(treeDef, 25, 1000000000 + 1);
        }

        private static int _fileSize = 0;

        const int NullValue = 1000000001;

        private static string[] _treeDef;

        private static int[] _array; //=Enumerable.Repeat(NullValue, (int)Math.Pow(2, 25) - 1).ToArray(); //new int[(int)Math.Pow(2, 25) - 1];//

        private static int _size = 0;

        private static void ParseTree(int indexToInsert, int treeDefIndex)
        {
            var nodeDef = _treeDef[treeDefIndex].Split(new[] { ' ' });
            var left = Int32.Parse(nodeDef[1]);
            var right = Int32.Parse(nodeDef[2]);

            _array[indexToInsert] = Int32.Parse(nodeDef[0]);
            _size++;

            if (left != 0)
                ParseTree(indexToInsert * 2 + 1, left);
            if (right != 0)
                ParseTree(indexToInsert * 2 + 2, right);
        }

        private static int GetIndex(int key)
        {
            var index = SearchIndex(key, 0);
            if (_array[index] != key)
                return -1;
            return index;

        }

        private static void DeleteSubTree(int index)
        {
            _size -= GetSubTreeCount(index);
            _array[index] = NullValue;            
        }

        private static int SearchIndex(int key, int indexToStart)
        {
            if (_array[indexToStart] == NullValue || _array[indexToStart] == key)
                return indexToStart;
            if (_array[indexToStart] < key)
            {
                if (indexToStart * 2 + 2 >= _array.Length)
                    return indexToStart;
                return SearchIndex(key, indexToStart * 2 + 2);
            }
            else
            {
                if (indexToStart * 2 + 1 >= _array.Length)
                    return indexToStart;
                return SearchIndex(key, indexToStart * 2 + 1);
            }
        }


        private static int GetSubTreeCount(int index)
        {
            if (index >= _array.Length || _array[index] == NullValue)
                return 0;
            else
                return GetSubTreeCount(index * 2 + 1) + GetSubTreeCount(index * 2 + 2) + 1;
        }
    }
}
