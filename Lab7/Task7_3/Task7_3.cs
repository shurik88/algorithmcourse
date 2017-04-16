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
            content[0] = (content.Length - 1).ToString();
            content[content.Length - 1] = string.Join(" ", content[content.Length - 1], "0", "0");
            //var size = Int32.Parse(content[0]);
            var subTree = InsertLastNode(content);
            subTree.Reverse();
            BalanceTree(content, subTree);




            using (var writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(content.Length - 1);
                for (var i = 1; i < content.Length; ++i)
                    writer.WriteLine(content[i]);
            }
            //try
            //{
            //    var content = File.ReadAllLines("input.txt");
            //    content[0] = content.Length.ToString();
            //    content[content.Length - 1] = string.Join(" ", content[content.Length - 1], "0", "0");
            //    //var size = Int32.Parse(content[0]);
            //    var subTree = InsertLastNode(content);
            //    subTree.Reverse();
            //    BalanceTree(content, subTree);




            //    using (var writer = new StreamWriter("output.txt"))
            //    {
            //        writer.WriteLine(content.Length - 1);
            //        for (var i = 1; i < content.Length; ++i)
            //            writer.WriteLine(content[i]);
            //    }
            //}
            //catch (Exception e)
            //{
            //    //File.WriteAllText("output.txt", e.Message);
            //}


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
