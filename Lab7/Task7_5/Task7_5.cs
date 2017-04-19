using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab7.Task7_5
{
    public class Task7_5
    {
        public static void Main(string[] args)
        {
            
            try
            {
                using (var reader = new StreamReader("input.txt"))
                {
                    var size = reader.ReadLine();//commands count
                    if (size == null)
                        throw new ArgumentException("Invalid file format");
                    string line = null;
                    //reader.ReadLine();
                    Node root = null;
                    using (var writer = new StreamWriter("output.txt"))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            var command = line.Split(new[] { ' ' });
                            Node currNode = null;
                            Node foundNode = null;
                            switch (command[0])
                            {

                                case "A":
                                    var stopWatch = new Stopwatch();
                                    stopWatch.Start();
                                    var key = Int32.Parse(command[1]);
                                    if (root == null)
                                    {
                                        root = new Node { Key = key };
                                        writer.WriteLine(root.Balance);
                                    }
                                    else
                                    {
                                        //long insertMs = 0, balancingMs = 0, rootMs = 0, calculationMs = 0;
                                        //var stopWatch = new Stopwatch();
                                        //stopWatch.Start();
                                        currNode = Node.Insert(root, key);

                                        //stopWatch.Stop();
                                        //insertMs = stopWatch.ElapsedMilliseconds;
                                        
                                        if (currNode != null && currNode.Parent != null && currNode.Parent.Parent != null)
                                        {
                                            //stopWatch.Restart();
                                            currNode = currNode.Parent.Parent;
                                            //currNode = Node.BalanceNode(currNode);
                                            //currNode = Node.BalanceNode(currNode);
                                            while (true)
                                            {
                                                var currNodekey = currNode.Key;
                                                currNode = Node.BalanceNode(currNode);
                                                if (currNode.Parent == null || currNode.Key != currNodekey)
                                                    break;
                                                currNode = currNode.Parent;
                                            }
                                            //stopWatch.Stop();
                                            //balancingMs = stopWatch.ElapsedMilliseconds;
                                            //root = currNode;
                                            //stopWatch.Restart();
                                            root = currNode.Root;
                                            //stopWatch.Stop();
                                            //rootMs = stopWatch.ElapsedMilliseconds;
                                        }
                                        //stopWatch.Restart();
                                        writer.WriteLine(root.Balance);
                                        //stopWatch.Stop();
                                        //calculationMs = stopWatch.ElapsedMilliseconds;
                                        //Console.WriteLine($"{line} {insertMs} {balancingMs} {rootMs} {calculationMs}");

                                    }
                                    stopWatch.Stop();
                                    Console.WriteLine($"{line} {stopWatch.ElapsedMilliseconds}");
                                    break;
                                case "D":
                                    foundNode = (root != null ? root.Find(Int32.Parse(command[1])) : null);
                                    if (foundNode != null)
                                    {
                                        currNode = Node.DeleteNode(foundNode);
                                        if (currNode == null)
                                            root = null;
                                        else
                                        {
                                            while (true)
                                            {
                                                currNode = Node.BalanceNode(currNode);
                                                if (currNode.Parent == null)
                                                    break;
                                                currNode = currNode.Parent;
                                            }
                                            root = currNode;
                                        }

                                    }
                                    writer.WriteLine(root != null ? root.Balance : 0);

                                    break;
                                case "C":
                                    foundNode = (root != null ? root.Find(Int32.Parse(command[1])) : null);
                                    writer.WriteLine(foundNode == null ? "N" : "Y");
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException("Unknown command");

                            }
                            //DisplayTree(root);

                        }
                    }
                }
            }
            catch(Exception e)
            {
                File.WriteAllText("output.txt", e.Message);
            }
        }

        public static void DisplayTree(Node root)
        {
            var list = GetTreeStringRepresentation(root);
            Console.WriteLine(list.Length);
            foreach (var node in list)
                Console.WriteLine(node);
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


            public Node Root
            {
                get
                {
                    var curr = this;
                    while (true)
                    {
                        if (curr.Parent == null)
                            return curr;
                        curr = curr.Parent;
                    }
                }
            }

            public Node Find(int key)
            {
                var currNode = this;
                while (true)
                {
                    if (key == currNode.Key)
                        return currNode;
                    else if (key > currNode.Key)
                    {
                        if (currNode.Right != null)
                            currNode = currNode.Right;
                        else
                            return null;
                    }
                    else
                    {
                        if (currNode.Left != null)
                            currNode = currNode.Left;
                        else
                            return null;
                    }

                }
            }

            public static Node Insert(Node root, int key)
            {
                if (root == null)
                    return new Node { Key = key };
                //if (root.Key == key)
                //    return null;//throw new ArgumentException("Dublicate node key");
                var currNode = root;
                while (true)
                {
                    if (key > currNode.Key)
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
                    else if (key < currNode.Key)
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
                    else
                        return null;
                }
            }


            public static Node DeleteNode(Node currNode)
            {
                if (currNode == null)
                    throw new ArgumentException("Node not found");
                var parent = currNode.Parent;
                if (currNode.Left == null && currNode.Right == null)
                {
                    if (parent != null)
                    {
                        if (parent.Key < currNode.Key)
                            parent.Right = null;
                        else
                            parent.Left = null;
                    }
                    currNode = null;
                    return parent;
                }
                else if (currNode.Left == null && currNode.Right != null)
                {
                    var right = currNode.Right;
                    if (parent != null)
                    {
                        if (parent.Key < currNode.Key)
                            parent.Right = right;
                        else
                            parent.Left = right;
                        right.Parent = parent;
                        currNode = null;
                        return right;
                    }
                    else
                    {
                        right.Parent = null;
                        currNode = null;
                        return right;
                    }
                }
                else
                {
                    var right = currNode.Left.GetRightestNode();
                    var rightParent = right.Parent;
                    if (rightParent == currNode)
                    {
                        right.Right = currNode.Right;
                        if (currNode.Right != null)
                            currNode.Right.Parent = right;
                        right.Parent = currNode.Parent;
                        if (currNode.Parent != null)
                        {
                            if (currNode.Parent.Key < right.Key)
                                currNode.Parent.Right = right;
                            else
                                currNode.Parent.Left = right;
                        }
                        currNode = null;
                        return right;
                    }
                    else
                    {
                        rightParent.Right = right.Left;
                        if (right.Left != null)
                            right.Left.Parent = rightParent;

                        right.Left = currNode.Left;
                        currNode.Left.Parent = right;

                        right.Right = currNode.Right;
                        if (currNode.Right != null)
                            currNode.Right.Parent = right;

                        right.Parent = currNode.Parent;
                        if (currNode.Parent != null)
                        {
                            if (currNode.Parent.Key < right.Key)
                                currNode.Parent.Right = right;
                            else
                                currNode.Parent.Left = right;
                        }
                        currNode = null;
                        return rightParent;
                    }
                }
            }


            public Node GetRightestNode()
            {
                var currNode = this;
                while (true)
                {
                    if (currNode.Right == null)
                        return currNode;
                    currNode = currNode.Right;
                }
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
    }
}
