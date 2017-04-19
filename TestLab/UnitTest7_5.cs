using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestLab
{
    [TestClass]
    public class UnitTest7_5
    {
        [TestMethod]
        public void TestABLTree()
        {
            var from = 19285;
            var to = 1;
            using (var writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(from);
                for (var i = from; i >= to; i--)
                    writer.WriteLine("A " + i);
            }
        }
    }
}
