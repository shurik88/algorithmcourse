using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab3.Task3_1
{
    public class Task3_1
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var arr1 = content[1].Split(new char[] { ' ' }).Select(x => int.Parse(x)).ToArray();
            var arr2 = content[2].Split(new char[] { ' ' }).Select(x => int.Parse(x)).ToArray();


            var sum = GetSumOfKNumberOfSortedArrayWithMatrix1(arr1, arr2, 10);
            var writer = new StreamWriter("output.txt");
            writer.WriteLine(sum);
            writer.Close();

        }

        public static class HeapSortHelper
        {
            public static void SortAscending(int[] arr)
            {
                HeapHelper.BuildMaxHeap(arr);
                var heapSize = arr.Length;
                for (var i = arr.Length - 1; i >= 1; i--)
                {
                    var temp = arr[0];
                    arr[0] = arr[i];
                    arr[i] = temp;

                    HeapHelper.MaxHeapify(arr, 0, --heapSize);
                }
            }
        }

        public static class HeapHelper
        {
            public static void BuildMaxHeap(int[] arr)
            {
                var lastParent = (arr.Length - 1) / 2;
                for (var i = lastParent; i >= 0; i--)
                {
                    MaxHeapify(arr, i);
                }
            }

            private static int GetLeft(int root)
            {
                return 2 * root + 1;
            }

            private static int GetRight(int root)
            {
                return 2 * root + 2;
            }

            public static bool IsMaxHeap(int[] arr)
            {
                var lastParent = (arr.Length - 1) / 2;
                for (var i = 0; i <= lastParent; ++i)
                {
                    var left = (2 * i + 1) >= arr.Length ? arr[i] : arr[2 * i + 1];
                    var right = (2 * i + 2) >= arr.Length ? arr[i] : arr[2 * i + 2]; ;
                    if (arr[i] < left || arr[i] < right)
                    {
                        return false;
                    }
                }
                return true;
            }

            public static void MaxHeapify(int[] arr, int i, int? heapSize = null)
            {
                var length = heapSize.HasValue ? heapSize.Value : arr.Length;
                var left = GetLeft(i);
                var right = GetRight(i);
                var largest = i;
                if (left < length && arr[largest] < arr[left])
                    largest = left;
                if (right < length && arr[largest] < arr[right])
                    largest = right;

                if (largest != i)
                {
                    var temp = arr[i];
                    arr[i] = arr[largest];
                    arr[largest] = temp;
                    MaxHeapify(arr, largest, heapSize);
                }

            }
        }

        public class Pos : IComparable
        {
            public int I { get; set; }

            public int J { get; set; }

            public long Value { get; set; }

            public int CompareTo(object obj)
            {
                var elem = (Pos)obj;
                return Value.CompareTo(elem.Value);
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        public class Elem
        {
            public int I { get; set; }

            public int J { get; set; }
        }

        public class PriorityQueue
        {
            private readonly SortedDictionary<long, Queue<Pos>> _subQueues = new SortedDictionary<long, Queue<Pos>>();

            public bool HasItems { get { return _subQueues.Any(); } }

            private void AddQueueOfPriority(long value)
            {
                _subQueues.Add(value, new Queue<Pos>());
            }

            public void Enqueue(Pos pos)
            {
                if (!_subQueues.ContainsKey(pos.Value))
                    AddQueueOfPriority(pos.Value);

                _subQueues[pos.Value].Enqueue(pos);
            }

            public Pos Dequeue()
            {
                KeyValuePair<long, Queue<Pos>> first = _subQueues.First();
                var item = first.Value.Dequeue();
                if (!first.Value.Any())
                {
                    _subQueues.Remove(first.Key);
                }
                return item;
            }
        }

        private static Pos GetDown(int[] arr1, int[] arr2, int posI, int posJ)
        {
            if (posI == (arr1.Length - 1))
                return null;
            return new Pos { I = posI + 1, J = posJ, Value = arr1[posI + 1] * arr2[posJ] };
        }

        private static Pos GetRight(int[] arr1, int[] arr2, int posI, int posJ)
        {
            if (posJ == (arr2.Length - 1) || posI != 0)
                return null;
            return new Pos { I = posI, J = posJ + 1, Value = arr1[posI] * arr2[posJ + 1] };
        }

        public static long GetSumOfKNumberOfSortedArrayWithMatrix1(int[] arr1, int[] arr2, int k)
        {
            var kArr = new Dictionary<int, int>();
            QuickSort(arr1, 0, arr1.Length - 1);
            QuickSort(arr2, 0, arr2.Length - 1);
            long sum = arr1[0] * arr2[0];
            int num = 0;
            var queue = new PriorityQueue();
            var right = GetRight(arr1, arr2, 0, 0);
            var down = GetDown(arr1, arr2, 0, 0);
            if (right != null)
                queue.Enqueue(right);
            if (down != null)
                queue.Enqueue(down);
            while (queue.HasItems)
            {
                num++;
                var elem = queue.Dequeue();
                right = GetRight(arr1, arr2, elem.I, elem.J);
                down = GetDown(arr1, arr2, elem.I, elem.J);
                if (right != null)
                    queue.Enqueue(right);
                if (down != null)
                    queue.Enqueue(down);
                if (num == 10)
                {
                    sum += elem.Value;
                    num = 0;
                }
            }
            return sum;
        }

        public class GetSumResult
        {
            public long PrepareSort { get; set; }

            public long AlgorithmSort { get; set; }

            public long Sum { get; set; }
        }

        public static GetSumResult GetSumOfKNumberOfSortedArrayWithMatrix(int[] arr1, int[] arr2, int k)
        {
            var kArr = new Dictionary<int, int>();
            var stopwatch = Stopwatch.StartNew(); 
            QuickSort(arr1, 0, arr1.Length - 1);
            QuickSort(arr2, 0, arr2.Length - 1);
            stopwatch.Stop();
            var prepare = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            long sum = arr1[0] * arr2[0];
            int num = 0;
            var list = new List<Pos>();
            var right = GetRight(arr1, arr2, 0, 0);
            var down = GetDown(arr1, arr2, 0, 0);

            //var buf = new Pos[arr1.Length > arr2.Length ? arr1.Length : arr2.Length];
            //var bufSize = 0;
            if (right != null)
            {
                list.Add(right);
                //buf[bufSize] = right;
                //bufSize++;
            }
            if (down != null)
            {
                list.Add(down);
                //buf[bufSize] = down;                
                //bufSize++;
            }
            while (list.Count != 0)
            //while (bufSize != 0)
            {
                num++;
                var index = 0;
                var min = list[index].Value;//buf[index].Value;//
                //for (var i = 0; i < bufSize; ++i)
                //{
                //    if (buf[i].Value < min)
                //    {
                //        index = i;
                //        min = buf[i].Value;
                //    }
                //}
                //var deleted = buf[index];
                for (var i = 0; i < list.Count; ++i)
                {
                    if (list[i].Value < min)
                    {
                        index = i;
                        min = list[i].Value;
                    }
                }
                var deleted = list[index];
                right = GetRight(arr1, arr2, deleted.I, deleted.J);
                down = GetDown(arr1, arr2, deleted.I, deleted.J);

                //buf[index] = buf[bufSize-1];
                //buf[bufSize - 1] = null;
                //bufSize--;
                //if (right != null)
                //{
                //    buf[bufSize] = right;
                //    bufSize++;
                //}
                //if (down != null)
                //{
                //    buf[bufSize] = down;
                //    bufSize++;
                //}
                if (right != null)
                    list.Add(right);
                if (down != null)
                    list.Add(down);
                list.RemoveAt(index);
                if (num == 10)
                {
                    sum += min;
                    num = 0;
                }
            }

            stopwatch.Stop();
            return new Lab3.Task3_1.Task3_1.GetSumResult { AlgorithmSort = stopwatch.ElapsedMilliseconds, PrepareSort = prepare, Sum = sum };
            
            //return sum;
        }

        public static GetSumResult GetSumOfKNumberOfSortedArrayWithMatrix2(int[] arr1, int[] arr2, int k)
        {
            var kArr = new Dictionary<int, int>();
            var stopwatch = Stopwatch.StartNew();
            QuickSort(arr1, 0, arr1.Length - 1);
            QuickSort(arr2, 0, arr2.Length - 1);
            stopwatch.Stop();
            var prepare = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            long sum = arr1[0] * arr2[0];
            int num = 0;
            //Console.WriteLine($"Adding to sum:{sum}");
            var list = new OrderedLinkedList<Pos>();
            var right = GetRight(arr1, arr2, 0, 0);
            var down = GetDown(arr1, arr2, 0, 0);
            if (right != null)
                list.Add(right);
            if (down != null)
                list.Add(down);
            while (!list.IsEmpty)
            {
                num++;
                
                var first = list.First;
                var value = first.Value;
                //Console.WriteLine($"n:{num}; min:{value}");
                right = GetRight(arr1, arr2, first.I, first.J);
                down = GetDown(arr1, arr2, first.I, first.J);
                list.DeleteFirst();
                if (right != null)
                {
                    list.Add(right);
                }
                if (down != null)
                    list.Add(down);

                if (num == 10)
                {
                    //Console.WriteLine($"Adding to sum:{value}");
                    sum += value;
                    num = 0;
                }
            }
            stopwatch.Stop();
            return new Lab3.Task3_1.Task3_1.GetSumResult { AlgorithmSort = stopwatch.ElapsedMilliseconds, PrepareSort = prepare, Sum = sum };
        }

        public class LinkedListElem<T>
            where T : IComparable
        {
            public T Key { get; set; }

            public LinkedListElem<T> Next { get; set; }

            public LinkedListElem<T> Prev { get; set; }
        }

        public class OrderedLinkedList<T>
            where T : IComparable
        {
            private LinkedListElem<T> _head = null;


            public LinkedListElem<T> Search(T key)
            {
                throw new NotImplementedException();
            }

            public void Add(T key)
            {

                if (_head == null)
                    _head = new LinkedListElem<T> { Key = key };
                else
                    OrderedAdd(key);
            }

            internal void OrderedAdd(T key)
            {
                var pointer = _head;
                var elem = new LinkedListElem<T> { Key = key };
                while (pointer != null)
                {
                    if (pointer.Key.CompareTo(key) > 0)
                    {
                        if (pointer == _head)
                        {
                            elem.Next = pointer;
                            pointer.Prev = elem;
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
                        break;
                    }
                    if (pointer.Next == null)
                    {
                        elem.Prev = pointer;
                        pointer.Next = elem;
                        break;
                    }
                    pointer = pointer.Next;
                }
                //Console.Write("Adding ");
                //DisplayList();

            }

            private void DisplayList()
            {
                var pointer = _head;
                while (pointer != null)
                {
                    Console.Write(pointer.Key.ToString() + " ");
                    pointer = pointer.Next;
                }
                Console.WriteLine();
            }

            public bool IsEmpty { get { return _head == null; } }

            public T First
            {
                get
                {
                    if (_head == null)
                        throw new ArgumentException("List is empty");
                    return _head.Key;
                }
            }

            public void DeleteFirst()
            {
                if (_head == null)
                    throw new ArgumentException("List is empty");
                var next = _head.Next;
                if (next == null)
                    _head = null;
                else
                {
                    next.Prev = null;
                    _head = next;
                }
                //Console.Write("Removing ");
                //DisplayList();
            }
        }

        public static GetSumResult GetSumOfKNumberOfSortedArrayWithCounting(int[] arr1, int[] arr2, int k)
        {
            var kArr = new Dictionary<int, int>();
            var arr = new int[arr1.Length * arr2.Length];
            var stopwatch = Stopwatch.StartNew();
            QuickSort(arr1, 0, arr1.Length - 1);
            QuickSort(arr2, 0, arr2.Length - 1);
            stopwatch.Stop();
            var prepare = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            for (var i = 0; i < arr1.Length; ++i)
            {
                for (var j = 0; j < arr2.Length; ++j)
                {
                    var elem = arr1[i] * arr2[j];
                    arr[arr2.Length * i + j] = elem;
                    if (!kArr.ContainsKey(elem))
                        kArr.Add(elem, 1);
                    else
                        kArr[elem] = kArr[elem] + 1;
                }
            }

            var sortedKeys = kArr.Keys.ToArray();
            //sortedKeys.Sort();
            QuickSort(sortedKeys, 0, sortedKeys.Length - 1); ;
            long sum = 0;
            int num = 0;
            for (var i = 0; i < sortedKeys.Length; ++i)
            {
                if(num == 0 || num == k)
                {
                    sum += sortedKeys[i];
                    num = 0;
                }
                num += kArr[sortedKeys[i]];
                while ((num - k) > 0)
                {
                    sum += sortedKeys[i];
                    num -= k;
                }
                //if (num > k)
                //{
                //    var add = (sortedKeys[i] * ((num - 1) / k));
                //    sum += add;
                //    num = num - k * add;
                //}
                
                //kArr[sortedKeys[i]] += kArr[sortedKeys[i - 1]];
            }


            //for (var i = arr.Length - 1; i >= 0; i--)
            //{
            //    if ((kArr[arr[i]] - 1) % k == 0)
            //        sum += arr[i];
            //    kArr[arr[i]]--;
            //}

            stopwatch.Stop();
            return new Lab3.Task3_1.Task3_1.GetSumResult { AlgorithmSort = stopwatch.ElapsedMilliseconds, PrepareSort = prepare, Sum = sum };
        }

        public static long GetSumOfKNumberOfSortedArrayWithFullSort(int[] arr1, int[] arr2, int k)
        {
            var arr = new int[arr1.Length * arr2.Length];
            for (var i = 0; i < arr1.Length; ++i)
            {
                for (var j = 0; j < arr2.Length; ++j)
                {
                    var elem = arr1[i] * arr2[j];
                    arr[arr2.Length * i + j] = elem;
                }
            }

            QuickSort(arr, 0, arr.Length - 1);

            long sum = 0;
            for (var i = arr.Length - 1; i >= 0; i--)
            {
                if (i % k == 0)
                    sum += arr[i];
            }

            return sum;
        }





        static Random _rand = new Random();
        private static void QuickSort(int[] arr, int left, int right)
        {
            if (left == right)
                return;
            var x = arr[_rand.Next(left, right)];
            var i = left;
            var j = right;
            while (i <= j)
            {
                while (arr[i] < x)
                    i++;
                while (arr[j] > x)
                    j--;
                if (i <= j)
                {
                    Swap(arr, i, j);
                    i++;
                    j--;
                }
            }

            if (i < right)
                QuickSort(arr, i, right);
            if (left < j)
                QuickSort(arr, left, j);
        }

        private static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            //var tempIndex = indexes[i];
            arr[i] = arr[j];
            //indexes[i] = indexes[j];
            arr[j] = temp;
            //indexes[j] = tempIndex;
        }
    }
}
