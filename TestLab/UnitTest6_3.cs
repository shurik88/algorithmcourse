using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab6.Task6_3;

namespace TestLab
{
    /// <summary>
    /// Summary description for UnitTest6_3
    /// </summary>
    [TestClass]
    public class UnitTest6_3
    {
        

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestBinaryTreeSize()
        {
            const int size = 118291;
            var treeDef = GetLeftTreeDefinition(size, 1000);
            var tree = Task6_3.ParseNode(1, treeDef);
            Assert.AreEqual(size, tree.Height);
            //
            // TODO: Add test logic here
            //
        }

        private string[] GetLeftTreeDefinition(int size, int initKey)
        {
            var treeDef = new string[size + 1];
            treeDef[0] = size.ToString();
            for(var i = 1; i <= size; ++i)
            {
                treeDef[i] = string.Join(" ", initKey - i + 1, i == size ? 0 : i + 1, 0);
            }
            return treeDef;
        }
    }
}
