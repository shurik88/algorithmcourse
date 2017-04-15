using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                var heights = new int[content.Length];//Enumerable.Range(0, content.Length).Select(x => -1).ToArray();
                var balances = new int[content.Length];

                for (var i = content.Length - 1; i > 0; i--)
                {
                    var nodeDef = content[i].Split(new[] { ' ' });
                    var left = Int32.Parse(nodeDef[1]);
                    var right = Int32.Parse(nodeDef[2]);
                    heights[i] = Math.Max(heights[left], heights[right]) + 1;
                    balances[i] = -heights[left] + heights[right];
                }

                if (balances[1] != 2)
                    throw new ArgumentException(string.Format("Invalid balance of root: {0}", balances[1]));
                var rootDef = content[1].Split(new[] { ' ' });
                var bNode = Int32.Parse(rootDef[2]);
                if(balances[bNode] == -1)//big left turn
                {
                    var cNode = Int32.Parse(content[bNode].Split(new[] { ' ' })[1]);
                    var newRootDef = content[cNode].ToString().Split(new[] { ' ' });
                    for(var i = cNode - 1; i >= 1; --i)
                    {
                        var nodeDef = content[i].Split(new[] { ' ' });
                        var left = Int32.Parse(nodeDef[1]);
                        var right = Int32.Parse(nodeDef[2]);
                        if (left != 0 && left < cNode)
                            left++;
                        if (right != 0 && right < cNode)
                            right++;
                        content[i + 1] = string.Format("{0} {1} {2}", nodeDef[0], left, right);
                    }

                    rootDef = content[2].Split(new[] { ' ' });
                    rootDef[2] = newRootDef[1];
                    content[2] = string.Join(" ", rootDef);//string.Format("{0} {1} {2}", rootDef[0], rootDef[1], newRootDef[1]);

                    var bNodeDef = content[bNode + 1].Split(new[] { ' ' });
                    bNodeDef[1] = newRootDef[2];
                    content[bNode + 1] = string.Join(" ", bNodeDef);

                    newRootDef[1] = "2";
                    newRootDef[2] = (bNode + 1).ToString();
                    content[1] = string.Join(" ", newRootDef);
                }
                else //small left turn
                {
                    var bNodeDef = content[bNode].Split(new[] { ' ' });
                    for (var i = bNode - 1; i >= 1; --i)
                    {
                        var nodeDef = content[i].Split(new[] { ' ' });
                        var left = Int32.Parse(nodeDef[1]);
                        var right = Int32.Parse(nodeDef[2]);
                        if (left != 0 && left < bNode)
                            left++;
                        if (right != 0 && right < bNode)
                            right++;
                        content[i + 1] = string.Format("{0} {1} {2}", nodeDef[0], left, right);
                    }
                    rootDef = content[2].Split(new[] { ' ' });
                    rootDef[2] = bNodeDef[1];
                    content[2] = string.Join(" ", rootDef);

                    bNodeDef[1] = "2";
                    content[1] = string.Join(" ", bNodeDef);
                }

                using (var writer = new StreamWriter("output.txt"))
                {
                    writer.WriteLine(content.Length - 1);
                    for (var i = 1; i < content.Length; ++i)
                        writer.WriteLine(content[i]);
                }
            }
            catch (Exception e)
            {
                File.WriteAllText("output.txt", e.Message);
            }


        }
    }
}
