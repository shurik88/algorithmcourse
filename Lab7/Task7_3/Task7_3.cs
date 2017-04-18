using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Task7_3
{
    public class Task7_3
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            if (content.Length < 2)
                throw new ArgumentException("Invalid file format");
            var inserted = Int32.Parse(content[content.Length - 1]);
            if (content.Length == 2)
            {
                using (var writer = new StreamWriter("output.txt"))
                {
                    writer.WriteLine(1);
                    writer.WriteLine(string.Join(" ", inserted, 0, 0));
                }
                return;
            }
            var root = ParseNode(1, null, content);
            var currNode = Node.Insert(root, inserted);
            while(true)
            {
                currNode = Node.BalanceNode(currNode);
                if (currNode.Parent == null)
                    break;
                currNode = currNode.Parent;
            }

            var list = GetTreeStringRepresentation(currNode);

            using (var writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(list.Length);
                foreach (var node in list)
                    writer.WriteLine(node);
            }
            //content[0] = (content.Length - 1).ToString();
            //content[content.Length - 1] = string.Join(" ", content[content.Length - 1], "0", "0");
            //var subTree = InsertLastNode(content);
            //subTree.Reverse();
            //BalanceTree(content, subTree);




            //using (var writer = new StreamWriter("output.txt"))
            //{
            //    writer.WriteLine(content.Length - 1);
            //    for (var i = 1; i < content.Length; ++i)
            //        writer.WriteLine(content[i]);
            //}
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

            public static Node Insert(Node root, int key)
            {
                if (root == null)
                    return new Node { Key = key };
                if (root.Key == key)
                    throw new ArgumentException("Dublicate node key");
                var currNode = root;
                while(true)
                {
                    if(key > currNode.Key)
                    {
                        if (currNode.Right != null)
                            currNode = currNode.Right;
                        else
                        {
                            var newNode = new Node { Key = key, Parent = currNode };
                            currNode.Right = newNode;
                            return newNode;
                        }
                    }
                    else
                    {
                        if (currNode.Left != null)
                            currNode = currNode.Left;
                        else
                        {
                            var newNode = new Node { Key = key, Parent = currNode };
                            currNode.Left = newNode;
                            return newNode;
                        }
                    }
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

                        if (node.Parent != null)
                        {
                            if (node.Parent.Key < cNode.Key)
                                node.Parent.Right = cNode;
                            else
                                node.Parent.Left = cNode;
                        }
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
                        if (node.Parent != null)
                        {
                            if (node.Parent.Key < bNode.Key)
                                node.Parent.Right = bNode;
                            else
                                node.Parent.Left = bNode;
                        }
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
                        if (node.Parent != null)
                        {
                            if (node.Parent.Key < cNode.Key)
                                node.Parent.Right = cNode;
                            else
                                node.Parent.Left = cNode;
                        }
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
                        if (node.Parent != null)
                        {
                            if (node.Parent.Key < bNode.Key)
                                node.Parent.Right = bNode;
                            else
                                node.Parent.Left = bNode;
                        }
                        node.Parent = bNode;
                        return bNode;
                    }
                }
                return node;

            }
        }

        private static void BigLeftShift(string[] treeDef, int node)
        {
            var rootDef = treeDef[node].Split(new[] { ' ' });
            var bNode = Int32.Parse(rootDef[2]);
            var cNode = Int32.Parse(treeDef[bNode].Split(new[] { ' ' })[1]);
            var newRootDef = treeDef[cNode].ToString().Split(new[] { ' ' });

            DownTree(treeDef, node, cNode);

            rootDef = treeDef[node + 1].Split(new[] { ' ' });
            rootDef[2] = newRootDef[1];
            treeDef[node + 1] = string.Join(" ", rootDef);//string.Format("{0} {1} {2}", rootDef[0], rootDef[1], newRootDef[1]);

            var bNodeDef = treeDef[bNode + 1].Split(new[] { ' ' });
            bNodeDef[1] = newRootDef[2];
            treeDef[bNode + 1] = string.Join(" ", bNodeDef);

            newRootDef[1] = (node + 1).ToString();
            newRootDef[2] = (bNode + 1).ToString();
            treeDef[node] = string.Join(" ", newRootDef);
        }

        private static void DownTree(string[] treeDef, int to, int from)
        {
            for (var i = from - 1; i >= 1; --i)
            {
                var nodeDef = treeDef[i].Split(new[] { ' ' });
                var left = Int32.Parse(nodeDef[1]);
                var right = Int32.Parse(nodeDef[2]);
                if (left != 0 && left < from && left > to)
                    left++;
                if (right != 0 && right < from && right > to)
                    right++;
                if(i >= to)
                    treeDef[i + 1] = string.Format("{0} {1} {2}", nodeDef[0], left, right);
                else
                    treeDef[i] = string.Format("{0} {1} {2}", nodeDef[0], left, right);
            }


        }

        private static void LeftShift(string[] treeDef, int node)
        {
            var rootDef = treeDef[node].Split(new[] { ' ' });
            var bNode = Int32.Parse(rootDef[2]);
            var bNodeDef = treeDef[bNode].Split(new[] { ' ' });

            DownTree(treeDef, node, bNode);

            rootDef = treeDef[node + 1].Split(new[] { ' ' });
            rootDef[2] = bNodeDef[1];
            treeDef[node + 1] = string.Join(" ", rootDef);

            bNodeDef[1] = (node + 1).ToString();
            treeDef[node] = string.Join(" ", bNodeDef);
        }

        private static void RightShift(string[] treeDef, int node)
        {
            var rootDef = treeDef[node].Split(new[] { ' ' });
            var bNode = Int32.Parse(rootDef[1]);
            var bNodeDef = treeDef[bNode].Split(new[] { ' ' });

            DownTree(treeDef, node, bNode);

            rootDef = treeDef[node + 1].Split(new[] { ' ' });
            rootDef[1] = bNodeDef[2];
            treeDef[node + 1] = string.Join(" ", rootDef);

            bNodeDef[2] = (node + 1).ToString();
            treeDef[node] = string.Join(" ", bNodeDef);
        }

        private static void BigRightShift(string[] treeDef, int node)
        {
            var rootDef = treeDef[node].Split(new[] { ' ' });
            var bNode = Int32.Parse(rootDef[1]);
            var cNode = Int32.Parse(treeDef[bNode].Split(new[] { ' ' })[2]);
            var newRootDef = treeDef[cNode].ToString().Split(new[] { ' ' });

            DownTree(treeDef, node, cNode);

            rootDef = treeDef[node + 1].Split(new[] { ' ' });
            rootDef[1] = newRootDef[2];
            treeDef[node + 1] = string.Join(" ", rootDef);//string.Format("{0} {1} {2}", rootDef[0], rootDef[1], newRootDef[1]);

            var bNodeDef = treeDef[bNode + 1].Split(new[] { ' ' });
            bNodeDef[2] = newRootDef[1];
            treeDef[bNode + 1] = string.Join(" ", bNodeDef);

            newRootDef[2] = (node + 1).ToString();
            newRootDef[1] = (bNode + 1).ToString();
            treeDef[node] = string.Join(" ", newRootDef);
        }

        private static List<int> InsertLastNode(string[] treeDef)
        {
            var subTree = new List<int>();
            var nodeToInsert = Int32.Parse(treeDef[treeDef.Length - 1].Split(new[] { ' ' })[0]);
            if (treeDef.Length == 2)
            {
                //treeDef[1] = string.Join(" ", treeDef[1], "0", "0");
                subTree.Add(treeDef.Length - 1);
                return subTree;
            }
           
            var currNode = 1;
            while (true)
            {
                subTree.Add(currNode);
                var nodeDef = treeDef[currNode].Split(new[] { ' ' });
                var value = Int32.Parse(nodeDef[0]);
                if (value < nodeToInsert)
                {
                    var right = Int32.Parse(nodeDef[2]);
                    if (right != 0)
                        currNode = right;
                    else
                    {
                        treeDef[currNode] = string.Join(" ", nodeDef[0], nodeDef[1], treeDef.Length - 1);
                        subTree.Add(treeDef.Length - 1);
                        return subTree;
                    }
                }
                else if (value > nodeToInsert)
                {
                    var left = Int32.Parse(nodeDef[1]);
                    if (left != 0)
                        currNode = left;
                    else
                    {
                        treeDef[currNode] = string.Join(" ", nodeDef[0], treeDef.Length - 1, nodeDef[2]);
                        subTree.Add(treeDef.Length - 1);
                        return subTree;
                    }
                }
                else
                    throw new ArgumentException("Dublicate node value");
            }
        }

        private static void BalanceTree(string[] treeDef, List<int> subTreeFromChild)
        {
            var balances = GetBalances(treeDef);
            foreach(var elem in subTreeFromChild)
            {
                var balance = balances[elem];
                
                if (balance == -2 || balance == 2)
                {
                    var rootDef = treeDef[elem].Split(new[] { ' ' });

                    if(balance == 2)
                    {
                        var bNode = Int32.Parse(rootDef[2]);
                        if (balances[bNode] == -1)
                            BigLeftShift(treeDef, elem);
                        else
                            LeftShift(treeDef, elem);
                    }
                    else
                    {
                        var bNode = Int32.Parse(rootDef[1]);
                        if (balances[bNode] == 1)
                            BigRightShift(treeDef, elem);
                        else
                            RightShift(treeDef, elem);
                    }
                    return;
                }
            }
        }


        private static int[] GetBalances(string[] treeDef)
        {
            var heights = new int[treeDef.Length];//Enumerable.Range(0, content.Length).Select(x => -1).ToArray();
            var balances = new int[treeDef.Length];

            for (var i = treeDef.Length - 1; i > 0; i--)
            {
                var nodeDef = treeDef[i].Split(new[] { ' ' });
                var left = Int32.Parse(nodeDef[1]);
                var right = Int32.Parse(nodeDef[2]);
                heights[i] = Math.Max(heights[left], heights[right]) + 1;
                balances[i] = -heights[left] + heights[right];
            }

            return balances;
        }
    }
}
