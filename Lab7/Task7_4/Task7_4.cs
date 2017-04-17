using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Task7_4
{
    public class Task7_4
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            if(content.Length == 3)
            {
                File.WriteAllText("output.txt","0");
                return;
            }
            //content[0] = (content.Length - 3).ToString();
            //content[content.Length - 1] = string.Join(" ", content[content.Length - 1], "0", "0");
            //var size = Int32.Parse(content[0]);
            var root = DeleteNode(content);
            BalanceTree(content, root);



            var balances = GetBalances(content);
            if(balances.Any(x => x == 2 || x == -2))
            {
                throw new Exception("Invalid balancing");
                //File.WriteAllText("output.txt", "0");
                //return;
            }


            using (var writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(content.Length - 3);
                for (var i = 1; i < content.Length - 2; ++i)
                    writer.WriteLine(content[i]);
            }
        }

        private static int[] GetParents(string[] treeDef)
        {
            var parents = new int[treeDef.Length - 1];
            for(var i = 1; i < treeDef.Length - 1; ++i)
            {
                var nodeDef = treeDef[i].Split(new[] { ' ' });
                var left = Int32.Parse(nodeDef[1]);
                var right = Int32.Parse(nodeDef[2]);
                parents[left] = i;
                parents[right] = i;
            }
            return parents;
        }

        private static int DeleteNode(string[] treeDef)
        {
            var parents = GetParents(treeDef);
            var nodeToDelete = Int32.Parse(treeDef[treeDef.Length - 1].Split(new[] { ' ' })[0]);

            var currNode = 1;
            while (true)
            {
                var nodeDef = treeDef[currNode].Split(new[] { ' ' });
                var value = Int32.Parse(nodeDef[0]);
                var right = Int32.Parse(nodeDef[2]);
                var left = Int32.Parse(nodeDef[1]);
                if (value < nodeToDelete)
                {
                    
                    if (right != 0)
                        currNode = right;
                    else
                        throw new ArgumentException("Value not found");
                }
                else if (value > nodeToDelete)
                {                    
                    if (left != 0)
                        currNode = left;
                    else
                        throw new ArgumentException("Value not found");
                }
                else
                {
                    if(left == 0 && right == 0)
                    {
                        var parent = parents[currNode];
                        var parentDef = treeDef[parent].Split(new[] { ' ' });
                        treeDef[parent] = string.Join(" ", parentDef[0], Int32.Parse(parentDef[1]) == currNode ? "0" : parentDef[1], Int32.Parse(parentDef[2]) == currNode ? "0" : parentDef[2]);
                        UpTree(treeDef, currNode);
                        return parent;
                    }
                    else if(left  == 0 && right != 0)
                    {
                        treeDef[currNode] = treeDef[right];
                        UpTree(treeDef, right);
                        return parents[currNode];
                    }
                    else
                    {
                        var rightOfLeft = FindRightNode(treeDef, left);
                        var rightOfLeftDef = treeDef[rightOfLeft].Split(new[] { ' ' });
                        var parent = parents[rightOfLeft];
                        if (parent != currNode && parent != 0)
                        {
                            var parentDef = treeDef[parent].Split(new[] { ' ' });
                            treeDef[parent] = string.Join(" ", parentDef[0], parentDef[1], rightOfLeftDef[1]);
                        }
                        treeDef[currNode] = string.Join(" ", rightOfLeftDef[0], parent == currNode ? rightOfLeftDef[1] : left.ToString(), right.ToString());
                        //treeDef[currNode] = 

                        UpTree(treeDef, rightOfLeft);
                        return parent;
                    }
                    //delete
                }
            }
        }

        private static int FindRightNode(string[] treeDef, int root)
        {
            var currNode = root;
            while (true)
            {
                var nodeDef = treeDef[currNode].Split(new[] { ' ' });
                var right = Int32.Parse(nodeDef[2]);
                if (right == 0)
                    return currNode;
                currNode = right;
            }
        }

        private static void UpTree(string[] treeDef, int to)
        {
            for (var i = to; i < treeDef.Length - 2; ++i)
            {
                treeDef[i] = treeDef[i + 1];
            }

            for (var i = 1; i < treeDef.Length - 2; ++i)
            {
                var nodeDef = treeDef[i].Split(new[] { ' ' });
                var left = Int32.Parse(nodeDef[1]);
                var right = Int32.Parse(nodeDef[2]);
                if (left != 0 && left > to)
                    left--;
                if (right != 0 && right > to)
                    right--;
                treeDef[i] = string.Format("{0} {1} {2}", nodeDef[0], left, right);

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
                if (i >= to)
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


        private static void BalanceTree(string[] treeDef, int elem)
        {
            //var parents = new int[treeDef.Length - 1];
            //for (var i = 1; i < treeDef.Length - 2; ++i)
            //{
            //    var nodeDef = treeDef[i].Split(new[] { ' ' });
            //    var left = Int32.Parse(nodeDef[1]);
            //    var right = Int32.Parse(nodeDef[2]);
            //    parents[left] = i;
            //    parents[right] = i;
            //}
            var parents = GetParents(treeDef);
            var balances = GetBalances(treeDef);
            while(elem != 0)
            {
                var balance = balances[elem];

                if (balance == -2 || balance == 2)
                {
                    var rootDef = treeDef[elem].Split(new[] { ' ' });

                    if (balance == 2)
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

                    parents = GetParents(treeDef);
                    balances = GetBalances(treeDef);
                }

                elem = parents[elem];
            }
        }


        private static int[] GetBalances(string[] treeDef)
        {
            var heights = new int[treeDef.Length];//Enumerable.Range(0, content.Length).Select(x => -1).ToArray();
            var balances = new int[treeDef.Length];

            for (var i = treeDef.Length - 3; i > 0; i--)
            {
                var nodeDef = treeDef[i].Split(new[] { ' ' });
                var left = Int32.Parse(nodeDef[1]);
                var right = Int32.Parse(nodeDef[2]);
                heights[i] = Math.Max(heights[left], heights[right]) + 1;
                balances[i] = -heights[left] + heights[right];
            }

            return balances;
        }

        public static Node ParseNode(int rootIndex, Node parent, string[] treeDef)
        {
            var nodeDef = treeDef[rootIndex].Split(new[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();
            var node = new Node { Key = nodeDef[0] };
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

            public static void BalanceNode(Node node)
            {
                var balance = node.Balance;
                if (balance == 2)
                {
                    var bNode = node.Right;
                    var rightBalance = bNode.Balance;
                    if (rightBalance == -1)
                    {
                        var cNode = bNode.Left;

                        node.Right = cNode.Left;
                        if(cNode.Left != null)
                            cNode.Left.Parent = node;
                        bNode.Left = cNode.Right;
                        if (cNode.Right != null)
                            cNode.Right.Parent = bNode;

                        cNode.Left = node;
                        cNode.Parent = node.Parent;
                        node.Parent = cNode;

                        cNode.Right = bNode;
                        bNode.Parent = cNode;


                    }
                    else
                    {
                        bNode.Left.Parent = node;
                        node.Right = bNode.Left;

                        bNode.Left = node;
                        bNode.Parent = node.Parent;
                        node.Parent = bNode;
                    }

                }
                else if (balance == -2)
                {
                    var bNode = node.Left;
                    var leftBalance = node.Left.Balance;

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
