using System;
using System.Collections;
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

                    //var tree = new AvlTree<int, int>();
                    //using (var writer = new StreamWriter("output.txt"))
                    //{
                    //    while ((line = reader.ReadLine()) != null)
                    //    {
                    //        var command = line.Split(new[] { ' ' });
                    //        switch (command[0])
                    //        {
                    //            case "A":
                    //                //var key = Int32.Parse(command[1]);
                    //                tree.Insert(Int32.Parse(command[1]), Int32.Parse(command[1]));
                    //                writer.WriteLine(-tree.Root.Balance);
                    //                //writer.WriteLine(-tree.balance_factor(tree.root));
                    //                break;
                    //            case "D":
                    //                tree.Delete(Int32.Parse(command[1]));
                    //                writer.WriteLine(tree.Root != null ? -tree.Root.Balance : 0);
                    //                //writer.WriteLine(tree.root == null ? 0 : -tree.balance_factor(tree.root));
                    //                break;
                    //            case "C":
                    //                int value;
                    //                writer.WriteLine(tree.Search(Int32.Parse(command[1]), out value) ? "Y" : "N");
                    //                //writer.WriteLine(tree.root == null || tree.Find(Int32.Parse(command[1])) == null ? "N" : "Y");
                    //                break;
                    //            default:
                    //                throw new ArgumentOutOfRangeException("Unknown command");
                    //        }
                    //    }
                    //}
                    var tree = new AVL();
                    using (var writer = new StreamWriter("output.txt"))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            var command = line.Split(new[] { ' ' });
                            switch (command[0])
                            {
                                case "A":
                                    //var key = Int32.Parse(command[1]);
                                    tree.Add(Int32.Parse(command[1]));
                                    writer.WriteLine(-tree.balance_factor(tree.root));
                                    break;
                                case "D":
                                    tree.Delete(Int32.Parse(command[1]));
                                    writer.WriteLine(tree.root == null ? 0 : -tree.balance_factor(tree.root));
                                    break;
                                case "C":

                                    writer.WriteLine(tree.root == null || tree.Find(Int32.Parse(command[1])) == null ? "N" : "Y");
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException("Unknown command");
                            }
                        }
                    }
                    //Node root = null;
                    //using (var writer = new StreamWriter("output.txt"))
                    //{
                    //    while ((line = reader.ReadLine()) != null)
                    //    {
                    //        var command = line.Split(new[] { ' ' });
                    //        Node currNode = null;
                    //        Node foundNode = null;
                    //        switch (command[0])
                    //        {

                    //            case "A":
                    //                var stopWatch = new Stopwatch();
                    //                stopWatch.Start();
                    //                var key = Int32.Parse(command[1]);
                    //                if (root == null)
                    //                {
                    //                    root = new Node { Key = key };
                    //                    writer.WriteLine(root.Balance);
                    //                }
                    //                else
                    //                {
                    //                    //long insertMs = 0, balancingMs = 0, rootMs = 0, calculationMs = 0;
                    //                    //var stopWatch = new Stopwatch();
                    //                    //stopWatch.Start();
                    //                    currNode = Node.Insert(root, key);

                    //                    //stopWatch.Stop();
                    //                    //insertMs = stopWatch.ElapsedMilliseconds;

                    //                    if (currNode != null && currNode.Parent != null && currNode.Parent.Parent != null)
                    //                    {
                    //                        //stopWatch.Restart();
                    //                        currNode = currNode.Parent.Parent;
                    //                        //currNode = Node.BalanceNode(currNode);
                    //                        //currNode = Node.BalanceNode(currNode);
                    //                        while (true)
                    //                        {
                    //                            var currNodekey = currNode.Key;
                    //                            currNode = Node.BalanceNode(currNode);
                    //                            if (currNode.Parent == null || currNode.Key != currNodekey)
                    //                                break;
                    //                            currNode = currNode.Parent;
                    //                        }
                    //                        //stopWatch.Stop();
                    //                        //balancingMs = stopWatch.ElapsedMilliseconds;
                    //                        //root = currNode;
                    //                        //stopWatch.Restart();
                    //                        root = currNode.Root;
                    //                        //stopWatch.Stop();
                    //                        //rootMs = stopWatch.ElapsedMilliseconds;
                    //                    }
                    //                    //stopWatch.Restart();
                    //                    writer.WriteLine(root.Balance);
                    //                    //stopWatch.Stop();
                    //                    //calculationMs = stopWatch.ElapsedMilliseconds;
                    //                    //Console.WriteLine($"{line} {insertMs} {balancingMs} {rootMs} {calculationMs}");

                    //                }
                    //                //stopWatch.Stop();
                    //                //Console.WriteLine($"{line} {stopWatch.ElapsedMilliseconds}");
                    //                break;
                    //            case "D":
                    //                foundNode = (root != null ? root.Find(Int32.Parse(command[1])) : null);
                    //                if (foundNode != null)
                    //                {
                    //                    currNode = Node.DeleteNode(foundNode);
                    //                    if (currNode == null)
                    //                        root = null;
                    //                    else
                    //                    {
                    //                        while (true)
                    //                        {
                    //                            currNode = Node.BalanceNode(currNode);
                    //                            if (currNode.Parent == null)
                    //                                break;
                    //                            currNode = currNode.Parent;
                    //                        }
                    //                        root = currNode;
                    //                    }

                    //                }
                    //                writer.WriteLine(root != null ? root.Balance : 0);

                    //                break;
                    //            case "C":
                    //                foundNode = (root != null ? root.Find(Int32.Parse(command[1])) : null);
                    //                writer.WriteLine(foundNode == null ? "N" : "Y");
                    //                break;
                    //            default:
                    //                throw new ArgumentOutOfRangeException("Unknown command");

                    //        }
                    //        //DisplayTree(root);

                    //    }
                    //}
                }
            }
            catch(Exception e)
            {
                File.WriteAllText("output.txt", e.Message);
            }
        }

        public sealed class AvlNode<TKey, TValue>
        {
            public AvlNode<TKey, TValue> Parent;
            public AvlNode<TKey, TValue> Left;
            public AvlNode<TKey, TValue> Right;
            public TKey Key;
            public TValue Value;
            public int Balance;
        }

        public class AvlTree<TKey, TValue> : IEnumerable<AvlNode<TKey, TValue>>
        {
            private IComparer<TKey> _comparer;
            private AvlNode<TKey, TValue> _root;

            public AvlTree(IComparer<TKey> comparer)
            {
                _comparer = comparer;
            }

            public AvlTree()
               : this(Comparer<TKey>.Default)
            {

            }

            public AvlNode<TKey, TValue> Root
            {
                get
                {
                    return _root;
                }
            }

            public IEnumerator<AvlNode<TKey, TValue>> GetEnumerator()
            {
                return new AvlNodeEnumerator<TKey, TValue>(_root);
            }

            public bool Search(TKey key, out TValue value)
            {
                AvlNode<TKey, TValue> node = _root;

                while (node != null)
                {
                    if (_comparer.Compare(key, node.Key) < 0)
                    {
                        node = node.Left;
                    }
                    else if (_comparer.Compare(key, node.Key) > 0)
                    {
                        node = node.Right;
                    }
                    else
                    {
                        value = node.Value;

                        return true;
                    }
                }

                value = default(TValue);

                return false;
            }

            public bool Insert(TKey key, TValue value)
            {
                AvlNode<TKey, TValue> node = _root;

                while (node != null)
                {
                    int compare = _comparer.Compare(key, node.Key);

                    if (compare < 0)
                    {
                        AvlNode<TKey, TValue> left = node.Left;

                        if (left == null)
                        {
                            node.Left = new AvlNode<TKey, TValue> { Key = key, Value = value, Parent = node };

                            InsertBalance(node, 1);

                            return true;
                        }
                        else
                        {
                            node = left;
                        }
                    }
                    else if (compare > 0)
                    {
                        AvlNode<TKey, TValue> right = node.Right;

                        if (right == null)
                        {
                            node.Right = new AvlNode<TKey, TValue> { Key = key, Value = value, Parent = node };

                            InsertBalance(node, -1);

                            return true;
                        }
                        else
                        {
                            node = right;
                        }
                    }
                    else
                    {
                        node.Value = value;

                        return false;
                    }
                }

                _root = new AvlNode<TKey, TValue> { Key = key, Value = value };

                return true;
            }

            private void InsertBalance(AvlNode<TKey, TValue> node, int balance)
            {
                while (node != null)
                {
                    balance = (node.Balance += balance);

                    if (balance == 0)
                    {
                        return;
                    }
                    else if (balance == 2)
                    {
                        if (node.Left.Balance == 1)
                        {
                            RotateRight(node);
                        }
                        else
                        {
                            RotateLeftRight(node);
                        }

                        return;
                    }
                    else if (balance == -2)
                    {
                        if (node.Right.Balance == -1)
                        {
                            RotateLeft(node);
                        }
                        else
                        {
                            RotateRightLeft(node);
                        }

                        return;
                    }

                    AvlNode<TKey, TValue> parent = node.Parent;

                    if (parent != null)
                    {
                        balance = parent.Left == node ? 1 : -1;
                    }

                    node = parent;
                }
            }

            private AvlNode<TKey, TValue> RotateLeft(AvlNode<TKey, TValue> node)
            {
                AvlNode<TKey, TValue> right = node.Right;
                AvlNode<TKey, TValue> rightLeft = right.Left;
                AvlNode<TKey, TValue> parent = node.Parent;

                right.Parent = parent;
                right.Left = node;
                node.Right = rightLeft;
                node.Parent = right;

                if (rightLeft != null)
                {
                    rightLeft.Parent = node;
                }

                if (node == _root)
                {
                    _root = right;
                }
                else if (parent.Right == node)
                {
                    parent.Right = right;
                }
                else
                {
                    parent.Left = right;
                }

                right.Balance++;
                node.Balance = -right.Balance;

                return right;
            }

            private AvlNode<TKey, TValue> RotateRight(AvlNode<TKey, TValue> node)
            {
                AvlNode<TKey, TValue> left = node.Left;
                AvlNode<TKey, TValue> leftRight = left.Right;
                AvlNode<TKey, TValue> parent = node.Parent;

                left.Parent = parent;
                left.Right = node;
                node.Left = leftRight;
                node.Parent = left;

                if (leftRight != null)
                {
                    leftRight.Parent = node;
                }

                if (node == _root)
                {
                    _root = left;
                }
                else if (parent.Left == node)
                {
                    parent.Left = left;
                }
                else
                {
                    parent.Right = left;
                }

                left.Balance--;
                node.Balance = -left.Balance;

                return left;
            }

            private AvlNode<TKey, TValue> RotateLeftRight(AvlNode<TKey, TValue> node)
            {
                AvlNode<TKey, TValue> left = node.Left;
                AvlNode<TKey, TValue> leftRight = left.Right;
                AvlNode<TKey, TValue> parent = node.Parent;
                AvlNode<TKey, TValue> leftRightRight = leftRight.Right;
                AvlNode<TKey, TValue> leftRightLeft = leftRight.Left;

                leftRight.Parent = parent;
                node.Left = leftRightRight;
                left.Right = leftRightLeft;
                leftRight.Left = left;
                leftRight.Right = node;
                left.Parent = leftRight;
                node.Parent = leftRight;

                if (leftRightRight != null)
                {
                    leftRightRight.Parent = node;
                }

                if (leftRightLeft != null)
                {
                    leftRightLeft.Parent = left;
                }

                if (node == _root)
                {
                    _root = leftRight;
                }
                else if (parent.Left == node)
                {
                    parent.Left = leftRight;
                }
                else
                {
                    parent.Right = leftRight;
                }

                if (leftRight.Balance == -1)
                {
                    node.Balance = 0;
                    left.Balance = 1;
                }
                else if (leftRight.Balance == 0)
                {
                    node.Balance = 0;
                    left.Balance = 0;
                }
                else
                {
                    node.Balance = -1;
                    left.Balance = 0;
                }

                leftRight.Balance = 0;

                return leftRight;
            }

            private AvlNode<TKey, TValue> RotateRightLeft(AvlNode<TKey, TValue> node)
            {
                AvlNode<TKey, TValue> right = node.Right;
                AvlNode<TKey, TValue> rightLeft = right.Left;
                AvlNode<TKey, TValue> parent = node.Parent;
                AvlNode<TKey, TValue> rightLeftLeft = rightLeft.Left;
                AvlNode<TKey, TValue> rightLeftRight = rightLeft.Right;

                rightLeft.Parent = parent;
                node.Right = rightLeftLeft;
                right.Left = rightLeftRight;
                rightLeft.Right = right;
                rightLeft.Left = node;
                right.Parent = rightLeft;
                node.Parent = rightLeft;

                if (rightLeftLeft != null)
                {
                    rightLeftLeft.Parent = node;
                }

                if (rightLeftRight != null)
                {
                    rightLeftRight.Parent = right;
                }

                if (node == _root)
                {
                    _root = rightLeft;
                }
                else if (parent.Right == node)
                {
                    parent.Right = rightLeft;
                }
                else
                {
                    parent.Left = rightLeft;
                }

                if (rightLeft.Balance == 1)
                {
                    node.Balance = 0;
                    right.Balance = -1;
                }
                else if (rightLeft.Balance == 0)
                {
                    node.Balance = 0;
                    right.Balance = 0;
                }
                else
                {
                    node.Balance = 1;
                    right.Balance = 0;
                }

                rightLeft.Balance = 0;

                return rightLeft;
            }

            public bool Delete(TKey key)
            {
                AvlNode<TKey, TValue> node = _root;

                while (node != null)
                {
                    if (_comparer.Compare(key, node.Key) < 0)
                    {
                        node = node.Left;
                    }
                    else if (_comparer.Compare(key, node.Key) > 0)
                    {
                        node = node.Right;
                    }
                    else
                    {
                        AvlNode<TKey, TValue> left = node.Left;
                        AvlNode<TKey, TValue> right = node.Right;

                        if (left == null)
                        {
                            if (right == null)
                            {
                                if (node == _root)
                                {
                                    _root = null;
                                }
                                else
                                {
                                    AvlNode<TKey, TValue> parent = node.Parent;

                                    if (parent.Left == node)
                                    {
                                        parent.Left = null;

                                        DeleteBalance(parent, -1);
                                    }
                                    else
                                    {
                                        parent.Right = null;

                                        DeleteBalance(parent, 1);
                                    }
                                }
                            }
                            else
                            {
                                Replace(node, right);

                                DeleteBalance(node, 0);
                            }
                        }
                        else if (right == null)
                        {
                            Replace(node, left);

                            DeleteBalance(node, 0);
                        }
                        else
                        {
                            AvlNode<TKey, TValue> successor = right;

                            if (successor.Left == null)
                            {
                                AvlNode<TKey, TValue> parent = node.Parent;

                                successor.Parent = parent;
                                successor.Left = left;
                                successor.Balance = node.Balance;
                                left.Parent = successor;

                                if (node == _root)
                                {
                                    _root = successor;
                                }
                                else
                                {
                                    if (parent.Left == node)
                                    {
                                        parent.Left = successor;
                                    }
                                    else
                                    {
                                        parent.Right = successor;
                                    }
                                }

                                DeleteBalance(successor, 1);
                            }
                            else
                            {
                                while (successor.Left != null)
                                {
                                    successor = successor.Left;
                                }

                                AvlNode<TKey, TValue> parent = node.Parent;
                                AvlNode<TKey, TValue> successorParent = successor.Parent;
                                AvlNode<TKey, TValue> successorRight = successor.Right;

                                if (successorParent.Left == successor)
                                {
                                    successorParent.Left = successorRight;
                                }
                                else
                                {
                                    successorParent.Right = successorRight;
                                }

                                if (successorRight != null)
                                {
                                    successorRight.Parent = successorParent;
                                }

                                successor.Parent = parent;
                                successor.Left = left;
                                successor.Balance = node.Balance;
                                successor.Right = right;
                                right.Parent = successor;
                                left.Parent = successor;

                                if (node == _root)
                                {
                                    _root = successor;
                                }
                                else
                                {
                                    if (parent.Left == node)
                                    {
                                        parent.Left = successor;
                                    }
                                    else
                                    {
                                        parent.Right = successor;
                                    }
                                }

                                DeleteBalance(successorParent, -1);
                            }
                        }

                        return true;
                    }
                }

                return false;
            }

            private void DeleteBalance(AvlNode<TKey, TValue> node, int balance)
            {
                while (node != null)
                {
                    balance = (node.Balance += balance);

                    if (balance == 2)
                    {
                        if (node.Left.Balance >= 0)
                        {
                            node = RotateRight(node);

                            if (node.Balance == -1)
                            {
                                return;
                            }
                        }
                        else
                        {
                            node = RotateLeftRight(node);
                        }
                    }
                    else if (balance == -2)
                    {
                        if (node.Right.Balance <= 0)
                        {
                            node = RotateLeft(node);

                            if (node.Balance == 1)
                            {
                                return;
                            }
                        }
                        else
                        {
                            node = RotateRightLeft(node);
                        }
                    }
                    else if (balance != 0)
                    {
                        return;
                    }

                    AvlNode<TKey, TValue> parent = node.Parent;

                    if (parent != null)
                    {
                        balance = parent.Left == node ? -1 : 1;
                    }

                    node = parent;
                }
            }

            private static void Replace(AvlNode<TKey, TValue> target, AvlNode<TKey, TValue> source)
            {
                AvlNode<TKey, TValue> left = source.Left;
                AvlNode<TKey, TValue> right = source.Right;

                target.Balance = source.Balance;
                target.Key = source.Key;
                target.Value = source.Value;
                target.Left = left;
                target.Right = right;

                if (left != null)
                {
                    left.Parent = target;
                }

                if (right != null)
                {
                    right.Parent = target;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class AvlNodeEnumerator<TKey, TValue> : IEnumerator<AvlNode<TKey, TValue>>
        {
            private AvlNode<TKey, TValue> _root;
            private Action _action;
            private AvlNode<TKey, TValue> _current;
            private AvlNode<TKey, TValue> _right;

            public AvlNodeEnumerator(AvlNode<TKey, TValue> root)
            {
                _right = _root = root;
                _action = _root == null ? Action.End : Action.Right;
            }

            public bool MoveNext()
            {
                switch (_action)
                {
                    case Action.Right:
                        _current = _right;

                        while (_current.Left != null)
                        {
                            _current = _current.Left;
                        }

                        _right = _current.Right;
                        _action = _right != null ? Action.Right : Action.Parent;

                        return true;

                    case Action.Parent:
                        while (_current.Parent != null)
                        {
                            AvlNode<TKey, TValue> previous = _current;

                            _current = _current.Parent;

                            if (_current.Left == previous)
                            {
                                _right = _current.Right;
                                _action = _right != null ? Action.Right : Action.Parent;

                                return true;
                            }
                        }

                        _action = Action.End;

                        return false;

                    default:
                        return false;
                }
            }

            public void Reset()
            {
                _right = _root;
                _action = _root == null ? Action.End : Action.Right;
            }

            public AvlNode<TKey, TValue> Current
            {
                get
                {
                    return _current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
            }

            enum Action
            {
                Parent,
                Right,
                End
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

        public class AVL
        {
            public class Node
            {
                public int data;
                public Node left;
                public Node right;
                public Node(int data)
                {
                    this.data = data;
                }
            }
            public Node root;
            public AVL()
            {
            }
            public void Add(int data)
            {
                Node newItem = new Node(data);
                if (root == null)
                {
                    root = newItem;
                }
                else
                {
                    root = RecursiveInsert(root, newItem);
                }
            }
            private Node RecursiveInsert(Node current, Node n)
            {
                if (current == null)
                {
                    current = n;
                    return current;
                }
                else if (n.data < current.data)
                {
                    current.left = RecursiveInsert(current.left, n);
                    current = balance_tree(current);
                }
                else if (n.data > current.data)
                {
                    current.right = RecursiveInsert(current.right, n);
                    current = balance_tree(current);
                }
                return current;
            }
            private Node balance_tree(Node current)
            {
                int b_factor = balance_factor(current);
                if (b_factor > 1)
                {
                    if (balance_factor(current.left) > 0)
                    {
                        current = RotateLL(current);
                    }
                    else
                    {
                        current = RotateLR(current);
                    }
                }
                else if (b_factor < -1)
                {
                    if (balance_factor(current.right) > 0)
                    {
                        current = RotateRL(current);
                    }
                    else
                    {
                        current = RotateRR(current);
                    }
                }
                return current;
            }
            public void Delete(int target)
            {//and here
                root = Delete(root, target);
            }
            private Node Delete(Node current, int target)
            {
                Node parent;
                if (current == null)
                { return null; }
                else
                {
                    //left subtree
                    if (target < current.data)
                    {
                        current.left = Delete(current.left, target);
                        if (balance_factor(current) == -2)//here
                        {
                            if (balance_factor(current.right) <= 0)
                            {
                                current = RotateRR(current);
                            }
                            else
                            {
                                current = RotateRL(current);
                            }
                        }
                    }
                    //right subtree
                    else if (target > current.data)
                    {
                        current.right = Delete(current.right, target);
                        if (balance_factor(current) == 2)
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else
                            {
                                current = RotateLR(current);
                            }
                        }
                    }
                    //if target is found
                    else
                    {
                        if (current.right != null)
                        {
                            //delete its inorder successor
                            parent = current.right;
                            while (parent.left != null)
                            {
                                parent = parent.left;
                            }
                            current.data = parent.data;
                            current.right = Delete(current.right, parent.data);
                            if (balance_factor(current) == 2)//rebalancing
                            {
                                if (balance_factor(current.left) >= 0)
                                {
                                    current = RotateLL(current);
                                }
                                else { current = RotateLR(current); }
                            }
                        }
                        else
                        {   //if current.left != null
                            return current.left;
                        }
                    }
                }
                return current;
            }
            public Node Find(int key)
            {
                var res = Find(key, root);
                if (res.data == key)
                {
                    return res;
                }
                else
                {
                    return null;
                }
            }
            private Node Find(int target, Node current)
            {
                if (target == current.data)
                    return current;
                else if (target < current.data)
                {
                    if(current.left != null)
                        return Find(target, current.left);
                    return current;
                }
                else
                {
                    if (current.right != null)
                        return Find(target, current.right);
                    return current;
                }
                //if (target < current.data)
                //{
                //    if (target == current.data)
                //    {
                //        return current;
                //    }
                //    else
                //        return Find(target, current.left);
                //}
                //else
                //{
                //    if (target == current.data)
                //    {
                //        return current;
                //    }
                //    else
                //        return Find(target, current.right);
                //}

            }
            public void DisplayTree()
            {
                if (root == null)
                {
                    Console.WriteLine("Tree is empty");
                    return;
                }
                InOrderDisplayTree(root);
                Console.WriteLine();
            }
            private void InOrderDisplayTree(Node current)
            {
                if (current != null)
                {
                    InOrderDisplayTree(current.left);
                    Console.Write("({0}) ", current.data);
                    InOrderDisplayTree(current.right);
                }
            }
            private int max(int l, int r)
            {
                return l > r ? l : r;
            }
            private int getHeight(Node current)
            {
                int height = 0;
                if (current != null)
                {
                    int l = getHeight(current.left);
                    int r = getHeight(current.right);
                    int m = max(l, r);
                    height = m + 1;
                }
                return height;
            }
            public int balance_factor(Node current)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int b_factor = l - r;
                return b_factor;
            }
            private Node RotateRR(Node parent)
            {
                Node pivot = parent.right;
                parent.right = pivot.left;
                pivot.left = parent;
                return pivot;
            }
            private Node RotateLL(Node parent)
            {
                Node pivot = parent.left;
                parent.left = pivot.right;
                pivot.right = parent;
                return pivot;
            }
            private Node RotateLR(Node parent)
            {
                Node pivot = parent.left;
                parent.left = RotateRR(pivot);
                return RotateLL(parent);
            }
            private Node RotateRL(Node parent)
            {
                Node pivot = parent.right;
                parent.right = RotateLL(pivot);
                return RotateRR(parent);
            }
        }
    }
}
