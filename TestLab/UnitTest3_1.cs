using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Lab3.Task3_1;
using System.IO;

namespace TestLab
{
    [TestClass]
    public class UnitTest3_1
    {
        static Random rand = new Random();

        [TestMethod]
        public void Task3_1TestSumEqual()
        {
            const int arrayMaxSize = 5;
            const int arrayMaxValue = 5;
            const int eachKNumber = 10;

            var arr_1 = GenerateArray(40000, 6000);
            var arr_2 = GenerateArray(40000, 1000);
            SaveTestInput(arr_1, arr_2);
            var res = Task3_1.GetSumOfKNumberOfSortedArrayWithMatrix(arr_1, arr_2, eachKNumber);
            return;
            for (var k = 0; k < 10000; ++k)
            {
                var arr1 = GenerateArray(arrayMaxValue, arrayMaxSize);
                var arr2 = GenerateArray(arrayMaxValue, arrayMaxSize);

                var countingSum = Task3_1.GetSumOfKNumberOfSortedArrayWithCounting(arr1, arr2, eachKNumber);
                var fullSortSum = Task3_1.GetSumOfKNumberOfSortedArrayWithFullSort(arr1, arr2, eachKNumber);
                if(countingSum.Sum != fullSortSum)
                {
                    SaveTestInput(arr1, arr2);
                    Assert.Fail($"Counting sum = {countingSum}, Full sort = {fullSortSum}");                    
                }
            }
        }

        private static void SaveTestInput(int[] arr1, int[] arr2)
        {
            var writer = new StreamWriter("input.txt");
            writer.WriteLine($"{arr1.Length} {arr2.Length}");
            writer.WriteLine(string.Join(" ", arr1));
            writer.WriteLine(string.Join(" ", arr2));
            writer.Close();
        }

        private static int[] GenerateArray(int maxValue, int maxSize)
        {
            return Enumerable.Range(0, maxSize).Select(x => rand.Next(1, maxValue)).ToArray();
        }
    }
}
