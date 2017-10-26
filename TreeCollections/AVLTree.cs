using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCollections
{
    public class AVLTree: ITree
    {
        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node(int val)
            {
                this.val = val;
            }
        }

        Node root = null;
        public void Init(int[] ini)
        {
            if (ini == null)
                return;

            Clear();
            for (int i = 0; i < ini.Length; i++)
            {
                Add(ini[i]);
            }
        }

        public void Clear()
        {
            root = null;
        }

        #region Rotate
        private int BalanceFactor(Node node)
        {
            int l = GetHeight(node.left);
            int r = GetHeight(node.right);
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
        #endregion

        #region Add
        public void Add(int val)
        {
            Node newNode = new Node(val);
            if (root == null)
                root = newNode;
            else
                root = AddNode(root, newNode);
        }
        private Node AddNode(Node node, Node newNode)
        {
            if (node == null)
            {
                node = newNode;
                return node;
            }

            else if (newNode.val < node.val)
            {
                node.left = AddNode(node.left, newNode);
                node = BalanceTree(node);
            }
            else if (newNode.val > node.val)
            {
                node.right = AddNode(node.right, newNode);
                node = BalanceTree(node);
            }
            return node;
        }

        private Node BalanceTree(Node node)
        {
            int b_factor = BalanceFactor(node);
            if (b_factor > 1)
            {
                if (BalanceFactor(node.left) > 0)
                {
                    node = RotateLL(node);
                }
                else
                {
                    node = RotateLR(node);
                }
            }
            else if (b_factor < -1)
            {
                if (BalanceFactor(node.right) > 0)
                {
                    node = RotateRL(node);
                }
                else
                {
                    node = RotateRR(node);
                }
            }
            return node;
        }
        #endregion

        #region Del
        public void DelLeft(int val)
        {
            DeleteNode(root, val);
        }
        private Node DeleteNode(Node node, int val)
        {
            Node parent;
            if (node == null)
                return null;
            else
            {
                //left subtree
                if (val < node.val)
                {
                    node.left = DeleteNode(node.left, val);
                    if (BalanceFactor(node) == -2)//here
                    {
                        if (BalanceFactor(node.right) <= 0)
                        {
                            node = RotateRR(node);
                        }
                        else
                        {
                            node = RotateRL(node);
                        }
                    }
                }
                //right subtree
                else if (val > node.val)
                {
                    node.right = DeleteNode(node.right, val);
                    if (BalanceFactor(node) == 2)
                    {
                        if (BalanceFactor(node.left) >= 0)
                        {
                            node = RotateLL(node);
                        }
                        else
                        {
                            node = RotateLR(node);
                        }
                    }
                }
                //if val is found
                else
                {
                    if (node.right != null)
                    {
                        //delete its inorder successor
                        parent = node.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        node.val = parent.val;
                        node.right = DeleteNode(node.right, parent.val);
                        if (BalanceFactor(node) == 2)//rebalancing
                        {
                            if (BalanceFactor(node.left) >= 0)
                            {
                                node = RotateLL(node);
                            }
                            else
                            {
                                node = RotateLR(node);
                            }
                        }
                    }
                    else
                    {   //if node.left != null
                        return node.left;
                    }
                }
            }
            return node;
        }
        #endregion

        #region Height
        public int Height()
        {
            return GetHeight(root);
        }
        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;

            return Math.Max(GetHeight(node.left), GetHeight(node.right)) + 1;
        }
        #endregion

        #region Width
        public int Width()
        {
            if (root == null)
                return 0;

            int[] ret = new int[Height()];
            GetWidth(root, ret, 0);
            return ret.Max();
        }
        private void GetWidth(Node node, int[] levels, int level)
        {
            if (node == null)
                return;

            GetWidth(node.left, levels, level + 1);
            levels[level]++;
            GetWidth(node.right, levels, level + 1);
        }
        #endregion

        #region Leaves
        public int Leaves()
        {
            return GetLeaves(root);
        }
        private int GetLeaves(Node node)
        {
            if (node == null)
                return 0;

            int leaves = 0;
            leaves += GetLeaves(node.left);
            if (node.left == null && node.right == null)
                leaves++;
            leaves += GetLeaves(node.right);
            return leaves;
        }

        #endregion

        #region Nodes
        public int Nodes()
        {
            return GetNodes(root);
        }
        private int GetNodes(Node node)
        {
            if (node == null)
                return 0;

            int nodes = 0;
            nodes += GetNodes(node.left);
            if (node.left != null || node.right != null)
                nodes++;
            nodes += GetNodes(node.right);
            return nodes;
        }
        #endregion

        #region Reverse
        public void Reverse()
        {
            SwapSides(root);
        }
        private void SwapSides(Node node)
        {
            if (node == null)
                return;

            SwapSides(node.left);
            Node temp = node.right;
            node.right = node.left;
            node.left = temp;
            SwapSides(node.left);
        }
        #endregion

        #region Size
        public int Size()
        {
            return GetSize(root);
        }
        private int GetSize(Node node)
        {
            if (node == null)
                return 0;

            int count = 0;
            count += GetSize(node.left);
            count++;
            count += GetSize(node.right);
            return count;
        }
        #endregion

        #region ToArray
        public int[] ToArray()
        {
            if (root == null)
                return new int[] { };

            int[] ret = new int[Size()];
            int i = 0;
            NodeToArray(root, ret, ref i);
            return ret;
        }
        private void NodeToArray(Node node, int[] ini, ref int n)
        {
            if (node == null)
                return;

            NodeToArray(node.left, ini, ref n);
            ini[n++] = node.val;
            NodeToArray(node.right, ini, ref n);
        }
        #endregion

        #region ToString
        public override String ToString()
        {
            return NodeToString(root).TrimEnd(new char[] { ',', ' ' });
        }

        private String NodeToString(Node node)
        {
            if (node == null)
                return "";

            String str = "";
            str += NodeToString(node.left);
            str += node.val + ", ";
            str += NodeToString(node.right);
            return str;
        }

        #endregion

        #region Equal

        public bool Equal(ITree tree)
        {
            return CompareNodes(root, (tree as AVLTree).root);
        }

        private bool CompareNodes(Node curTree, Node tree)
        {
            if (curTree == null && tree == null)
                return true;
            if (curTree == null || tree == null)
                return false;

            bool equal = true;
            equal = CompareNodes(curTree.left, tree.left);
            equal = equal & (curTree.val == tree.val);
            equal = CompareNodes(curTree.right, tree.right);
            return equal;
        }
        #endregion
    }
}
