using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab5;

namespace TestLab
{
    [TestClass]
    public class UnitTest5_Heap
    {
        static Random rand = new Random();

        [TestMethod]
        public void TestMaxHeap()
        {
            const int arraySize = 20;
            const int arrayMaxValue = 10;
            const int iterationCount = 10;

            for (var i = 0; i < iterationCount; ++i)
            {
                var arr = DataGenerator.GenerateArray(1, arrayMaxValue, arraySize);
                var clone = arr.Clone();
                HeapHelper.BuildMaxHeap(arr);
                Assert.IsTrue(HeapHelper.IsMaxHeap(arr), $"{string.Join(" ", clone)} - {string.Join(" ", arr)}");
                
            }
        }

        [TestMethod]
        public void TestMaxHeapSorting()
        {
            const int arraySize = 10;
            const int arrayMaxValue = 10;
            const int iterationCount = 10;

            for (var i = 0; i < iterationCount; ++i)
            {
                var arr = DataGenerator.GenerateArray(1, arrayMaxValue, arraySize);
                HeapSortHelper.SortAscending(arr);
                Assert.IsTrue(SortingValidator.IsAscending(arr), $"{string.Join(" ", arr)}");

            }
        }

        [TestMethod]
        public void TestMinHeapSorting()
        {
            const int arraySize = 10;
            const int arrayMaxValue = 10;
            const int iterationCount = 10;

            for (var i = 0; i < iterationCount; ++i)
            {
                var arr = DataGenerator.GenerateArray(1, arrayMaxValue, arraySize);
                HeapSortHelper.SortDescending(arr);
                Assert.IsTrue(SortingValidator.IsDescending(arr), $"{string.Join(" ", arr)}");

            }
        }


    }
}
