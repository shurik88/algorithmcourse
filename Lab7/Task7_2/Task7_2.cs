using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab7.Task7_2
{
    public class Task7_2
    {
        public static void Main(string[] args)
        {

            try
            {
                var content = File.ReadAllLines("input.txt");
                var size = Int32.Parse(content[0]);
                if (size == 0 || size == 1)
                {
                    File.WriteAllText("output.txt", "0");
                    return;
                }

                var root = ParseNode(1, null, content);
                var newRoot = Node.BalanceNode(root);
                var list = GetTreeStringRepresentation(newRoot);

                using (var writer = new StreamWriter("output.txt"))
                {
                    writer.WriteLine(list.Length);
                    foreach (var node in list)
                        writer.WriteLine(node);
                }

                //var heights = new int[content.Length];//Enumerable.Range(0, content.Length).Select(x => -1).ToArray();
                //var balances = new int[content.Length];

                //for (var i = content.Length - 1; i > 0; i--)
                //{
                //    var nodeDef = content[i].Split(new[] { ' ' });
                //    var left = Int32.Parse(nodeDef[1]);
                //    var right = Int32.Parse(nodeDef[2]);
                //    heights[i] = Math.Max(heights[left], heights[right]) + 1;
                //    balances[i] = -heights[left] + heights[right];
                //}

                //if (balances[1] != 2)
                //    throw new ArgumentException(string.Format("Invalid balance of root: {0}", balances[1]));
                //var rootDef = content[1].Split(new[] { ' ' });
                //var bNode = Int32.Parse(rootDef[2]);
                //if (balances[bNode] == -1)//big left turn
                //{
                //    var cNode = Int32.Parse(content[bNode].Split(new[] { ' ' })[1]);
                //    var newRootDef = content[cNode].ToString().Split(new[] { ' ' });
                //    for (var i = cNode - 1; i >= 1; --i)
                //    {
                //        var nodeDef = content[i].Split(new[] { ' ' });
                //        var left = Int32.Parse(nodeDef[1]);
                //        var right = Int32.Parse(nodeDef[2]);
                //        if (left != 0 && left < cNode)
                //            left++;
                //        if (right != 0 && right < cNode)
                //            right++;
                //        content[i + 1] = string.Format("{0} {1} {2}", nodeDef[0], left, right);
                //    }

                //    rootDef = content[2].Split(new[] { ' ' });
                //    rootDef[2] = newRootDef[1];
                //    content[2] = string.Join(" ", rootDef);//string.Format("{0} {1} {2}", rootDef[0], rootDef[1], newRootDef[1]);

                //    var bNodeDef = content[bNode + 1].Split(new[] { ' ' });
                //    bNodeDef[1] = newRootDef[2];
                //    content[bNode + 1] = string.Join(" ", bNodeDef);

                //    newRootDef[1] = "2";
                //    newRootDef[2] = (bNode + 1).ToString();
                //    content[1] = string.Join(" ", newRootDef);
                //}
                //else //small left turn
                //{
                //    var bNodeDef = content[bNode].Split(new[] { ' ' });
                //    for (var i = bNode - 1; i >= 1; --i)
                //    {
                //        var nodeDef = content[i].Split(new[] { ' ' });
                //        var left = Int32.Parse(nodeDef[1]);
                //        var right = Int32.Parse(nodeDef[2]);
                //        if (left != 0 && left < bNode)
                //            left++;
                //        if (right != 0 && right < bNode)
                //            right++;
                //        content[i + 1] = string.Format("{0} {1} {2}", nodeDef[0], left, right);
                //    }
                //    rootDef = content[2].Split(new[] { ' ' });
                //    rootDef[2] = bNodeDef[1];
                //    content[2] = string.Join(" ", rootDef);

                //    bNodeDef[1] = "2";
                //    content[1] = string.Join(" ", bNodeDef);
                //}

                //using (var writer = new StreamWriter("output.txt"))
                //{
                //    writer.WriteLine(content.Length - 1);
                //    for (var i = 1; i < content.Length; ++i)
                //        writer.WriteLine(content[i]);
                //}
            }
            catch (Exception e)
            {
                File.WriteAllText("output.txt", e.Message);
            }


        }


        public static string[] GetTreeStringRepresentation(Node root)
        {
            if (root.Parent != null)
                throw new ArgumentException("root parent is not null");

            var list = new List<string>();

            var queue = new Queue<Node>();

            queue.Enqueue(root);
            queue.Enqueue(null);

            var height = 1;
            while (queue.Count != 0)
            {
                var elem = queue.Dequeue();
                if (elem == null)
                {
                    if (queue.Count != 0)
                        queue.Enqueue(null);
                    //height++;
                }
                else
                {
                    var nodeKey = elem.Key;
                    var left = elem.Left;
                    var right = elem.Right;

                    if (left != null)
                        queue.Enqueue(left);
                    if (right != null)
                        queue.Enqueue(right);
                    list.Add(string.Join(" ", nodeKey, left != null ? ++height : 0, right != null ? ++height : 0));
                }
            }

            return list.ToArray();
        }

        public static Node ParseNode(int rootIndex, Node parent, string[] treeDef)
        {
            var nodeDef = treeDef[rootIndex].Split(new[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();
            var node = new Node { Key = nodeDef[0], Parent = parent };
            if (nodeDef[1] != 0)
                node.Left = ParseNode(nodeDef[1], node, treeDef);
            if (nodeDef[2] != 0)
                node.Right = ParseNode(nodeDef[2], node, treeDef);
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

            public int Balance
            {
                get
                {
                    return (Right != null ? Right.Height : 0) - (Left != null ? Left.Height : 0);
                }
            }


            public static void DeleteNode(int key)
            {

            }

            public static Node BalanceNode(Node node)
            {
                var balance = node.Balance;
                if (balance == 2)
                {
                    var bNode = node.Right;
                    var rightBalance = bNode.Balance;
                    if (rightBalance == -1) // big right shift
                    {
                        var cNode = bNode.Left;

                        node.Right = cNode.Left;
                        if (cNode.Left != null)
                            cNode.Left.Parent = node;
                        bNode.Left = cNode.Right;
                        if (cNode.Right != null)
                            cNode.Right.Parent = bNode;

                        cNode.Left = node;
                        cNode.Parent = node.Parent;
                        node.Parent = cNode;

                        cNode.Right = bNode;
                        bNode.Parent = cNode;
                        return cNode;

                    }
                    else // right shift
                    {
                        node.Right = bNode.Left;
                        if (bNode.Left != null)
                            bNode.Left.Parent = node;


                        bNode.Left = node;
                        bNode.Parent = node.Parent;
                        node.Parent = bNode;
                        return bNode;
                    }

                }
                else if (balance == -2)
                {
                    var bNode = node.Left;
                    var leftBalance = bNode.Balance;
                    if (leftBalance == 1) //big left shift
                    {
                        var cNode = bNode.Right;

                        node.Left = cNode.Right;
                        if (cNode.Right != null)
                            cNode.Right.Parent = node;
                        bNode.Right = cNode.Left;
                        if (cNode.Left != null)
                            cNode.Left.Parent = bNode;

                        cNode.Right = node;
                        cNode.Parent = node.Parent;
                        node.Parent = cNode;

                        cNode.Left = bNode;
                        bNode.Parent = cNode;
                        return cNode;
                    }
                    else // left shift
                    {
                        node.Left = bNode.Right;
                        if (bNode.Right != null)
                            bNode.Right.Parent = node;


                        bNode.Right = node;
                        bNode.Parent = node.Parent;
                        node.Parent = bNode;
                        return bNode;
                    }
                }
                return node;

            }
        }
    }

    
}
