using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCollections
{
    public class BsTree_4Del: ITreeDel
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

        public Node root = null;

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

        #region Add
        public void Add(int val)
        {
            if (root == null)
                root = new Node(val);
            else
                AddNode(root, val);
        }
        private void AddNode(Node node, int val)
        {
            if (val < node.val)
            {
                if (node.left == null)
                    node.left = new Node(val);
                else
                    AddNode(node.left, val);
            }
            else if (val > node.val)
            {
                if (node.right == null)
                    node.right = new Node(val);
                else
                    AddNode(node.right, val);
            }
        }
        #endregion

        #region DelRight
        public void DelRight(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            if (FindNode(root, val) == null)
                throw new ValueNotFoundEx();
            if (Size() == 1)
                root = null;

            DeleteNodeRight(root, val);
        }
        private Node FindNode(Node node, int val)
        {
            if (node == null || val == node.val)
                return node;
            if (val < node.val)
                return FindNode(node.left, val);
            else
                return FindNode(node.right, val);
        }
        private Node DeleteNodeRight(Node node, int val)
        {
            if (node == null)
                return node;

            if (val < node.val)
            {
                node.left = DeleteNodeRight(node.left, val);
            }
            else if (val > node.val)
            {
                node.right = DeleteNodeRight(node.right, val);
            }
            else if (node.left != null && node.right != null)
            {
                node.val = MinL(node.right).val;
                node.right = DeleteNodeRight(node.right, node.val);
            }
            else
            {
                if (node.left != null)
                    node = node.left;
                else
                    node = node.right;
            }
            return node;
        }
        private Node MinL(Node node)
        {
            if (node.left == null)
                return node;

            return MinL(node.left);
        }
        #endregion
        
        #region DelLeft
        public void DelLeft(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            if (FindNode(root, val) == null)
                throw new ValueNotFoundEx();
            if (Size() == 1)
                root = null;

            DeleteNodeLeft(root, val);
        }
        
        private Node DeleteNodeLeft(Node node, int val)
        {
            if (node == null)
                return node;

            if (val < node.val)
            {
                node.left = DeleteNodeLeft(node.left, val);
            }
            else if (val > node.val)
            {
                node.right = DeleteNodeLeft(node.right, val);
            }
            else if (node.left != null && node.right != null)
            {
                node.val = MaxR(node.left).val;
                node.left = DeleteNodeLeft(node.left, node.val);
            }
            else
            {
                if (node.right != null)
                    node = node.right;
                else
                    node = node.left;
            }
            return node;
        }
        private Node MaxR(Node node)
        {
            if (node.right == null)
                return node;

            return MaxR(node.right);
        }
        #endregion

        #region DelLeftRotate

        public void DelLeftRotate(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();

            root = DeleteNodeLeftRotate(root, val);
        }

        private Node DeleteNodeLeftRotate(Node node, int val)
        {
            if (node == null)
                return node;

            if (val < node.val)
                node.left = DeleteNodeLeftRotate(node.left, val);
            else
                node.right = DeleteNodeLeftRotate(node.right, val);

            if (val == node.val)
            {
                if (node.left != null && node.right != null)
                {
                    Node p = node.right;
                    node.right = null;
                    node = node.left;
                    MaxR(node).right = p;
                }
                else
                {
                    if (node.left != null)
                        node = node.left;
                    else
                        node = node.right;
                }
            }
            return node;
        }

        #endregion

        #region DelRightRotate

        public void DelRightRotate(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            
            root = DeleteNodeRightRotate(root, val);
        }

        private Node DeleteNodeRightRotate(Node node, int val)
        {
            if (node == null)
                return node;

            if (val < node.val)
                node.left = DeleteNodeRightRotate(node.left, val);
            else
                node.right = DeleteNodeRightRotate(node.right, val);

            if (val == node.val)
            { 
                if (node.left != null && node.right != null)
                {
                    Node p = node.left;
                    node.left = null;
                    node = node.right;
                    MinL(node).left = p;
                }
                else
                {
                    if (node.right != null)
                        node = node.right;
                    else
                        node = node.left;
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

        public void Clear()
        {
            root = null;
        }
        #endregion

        #region Equal

        public bool Equal(ITreeDel tree)
        {
            return CompareNodes(root, (tree as BsTree_4Del).root);
        }

        private bool CompareNodes(Node curTree, Node tree)
        {
            if (curTree == null && tree == null)
                return true;
            if (curTree == null || tree == null)
                return false;

            bool equal = false;
            equal = CompareNodes(curTree.left, tree.left);
            equal = equal & (curTree.val == tree.val);
            equal = CompareNodes(curTree.right, tree.right);
            return equal;
        }
        #endregion
    }
}
