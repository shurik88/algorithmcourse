namespace TestLab
{
    public static class SortingValidator
    {
        public static bool IsAscending(int[] arr)
        {
            for(var i = 0; i < arr.Length - 1; i++ )
            {
                if (arr[i] > arr[i + 1])
                    return false;
            }
            return true;
        }

        public static bool IsDescending(int[] arr)
        {
            for (var i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] < arr[i + 1])
                    return false;
            }
            return true;
        }
    }
}
