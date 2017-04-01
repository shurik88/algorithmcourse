namespace Lab5
{
    public static class HeapSortHelper
    {
        public static void SortAscending(int[] arr)
        {
            HeapHelper.BuildMaxHeap(arr);
            var heapSize = arr.Length;
            for(var i = arr.Length - 1; i >= 1; i--)
            {
                var temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                HeapHelper.MaxHeapify(arr, 0, --heapSize);
            }
        }

        public static void SortDescending(int[] arr)
        {
            HeapHelper.BuildMinHeap(arr);
            var heapSize = arr.Length;
            for (var i = arr.Length - 1; i >= 1; i--)
            {
                var temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                HeapHelper.MinHeapify(arr, 0, --heapSize);
            }
        }
    }
}
