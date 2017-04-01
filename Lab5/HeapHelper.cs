namespace Lab5
{
    public static class HeapHelper
    {
        public static void BuildMaxHeap(int[] arr)
        {
            var lastParent = (arr.Length - 1) / 2;
            for(var i = lastParent; i >=0; i--)
            {
                MaxHeapify(arr, i);
            }
        }

        public static void BuildMinHeap(int[] arr)
        {
            var lastParent = (arr.Length - 1) / 2;
            for (var i = lastParent; i >= 0; i--)
            {
                MinHeapify(arr, i);
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

        public static bool IsMinHeap(int[] arr)
        {
            var lastParent = (arr.Length - 1) / 2;
            for (var i = 0; i <= lastParent; ++i)
            {
                var left = (2 * i + 1) >= arr.Length ? arr[i] : arr[2 * i + 1];
                var right = (2 * i + 2) >= arr.Length ? arr[i] : arr[2 * i + 2]; ;
                if (arr[i] > left || arr[i] > right)
                {
                    return false;
                }
            }
            return true;
        }

        public static void MinHeapify(int[] arr, int i, int? heapSize = null)
        {
            var length = heapSize.HasValue ? heapSize.Value : arr.Length;
            var left = GetLeft(i);
            var right = GetRight(i);
            var smallest = i;
            if (left < length && arr[smallest] > arr[left])
                smallest = left;
            if (right < length && arr[smallest] > arr[right])
                smallest = right;

            if (smallest != i)
            {
                var temp = arr[i];
                arr[i] = arr[smallest];
                arr[smallest] = temp;
                MinHeapify(arr, smallest, heapSize);
            }
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

            if(largest != i)
            {
                var temp = arr[i];
                arr[i] = arr[largest];
                arr[largest] = temp;
                MaxHeapify(arr, largest, heapSize);
            }
        }
    }
}
