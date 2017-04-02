using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Task6_3
{
    public class Task6_3
    {
        public static void Main(string[] args)
        {
            
            try
            {
                var content = File.ReadAllLines("input.txt");
                if (Int32.Parse(content[0]) == 0)
                {
                    File.WriteAllText("output.txt", "0");
                    return;
                }
                var root = ParseNode(1, content);
                File.WriteAllText("output.txt", root.Height.ToString());
            }
            catch(Exception e)
            {
                File.WriteAllText("output.txt", e.Message);
            }
            

        }

        public static Node ParseNode(int rootIndex, string[] treeDef)
        {
            var nodeDef = treeDef[rootIndex].Split(new[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();
            var node = new Node { Key = nodeDef[0] };
            if (nodeDef[1] != 0)
                node.Left = ParseNode(nodeDef[1], treeDef);
            if (nodeDef[2] != 0)
                node.Right = ParseNode(nodeDef[2], treeDef);
            return node;
        }

        public class Node
        {
            public Node Parent { get; set; }

            public int Key { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public int Height
            {
                get
                {
                    var left = Left != null ? Left.Height : 0;
                    var right = Right != null ? Right.Height : 0;
                    return Math.Max(left, right) + 1;
                }
            }

            //public static Node Insert(Node to, Node inserted)
            //{
            //    if (to == null)
            //        return inserted;
            //    if (to.Key > inserted.Key)
            //    {
            //        to.Left = Insert(to.Left, inserted);
            //        return to.Left;
            //    }
            //    else
            //    {
            //        to.Right = Insert(to.Right, inserted);
            //        return to.Right;
            //    }

            //}
        }
    }
}
