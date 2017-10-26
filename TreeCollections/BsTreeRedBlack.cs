using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCollections
{
    public class BsTreeRedBlack: ITree
    {
        public enum Color { Red, Black };
        public class Node
        {
            public Color color;
            public Node left;
            public Node right;
            public Node parent;
            public int val;

            public Node(int val)
            {
                this.val = val;
            }
            public Node(Color color)
            {
                this.color = color;
            }
            public Node(int val, Color color)
            {
                this.val = val;
                this.color = color;
            }
        }

        private Node root = null;
        
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

        #region Rotate
        public void LeftRotate(Node X)
        {
            Node Y = X.right; // set Y
            X.right = Y.left;//turn Y's left subtree into X's right subtree
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            if (Y != null)
            {
                Y.parent = X.parent;//link X's parent to Y
            }
            if (X.parent == null)
            {
                root = Y;
            }
            else if (X == X.parent.left)
            {
                X.parent.left = Y;
            }
            else
            {
                X.parent.right = Y;
            }
            Y.left = X; //put X on Y's left
            if (X != null)
            {
                X.parent = Y;
            }

        }

        private void RightRotate(Node Y)
        {
            // right rotate is simply mirror code from left rotate
            Node X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            if (X != null)
            {
                X.parent = Y.parent;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;//put Y on X's right
            if (Y != null)
            {
                Y.parent = X;
            }
        }
        #endregion

        #region Add

        public void Add(int val)
        {
            Node newItem = new Node(val);
            if (root == null)
            {
                root = newItem;
                root.color = Color.Black;
                return;
            }
            Node Y = null;
            Node X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.val < X.val)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.val < Y.val)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.color = Color.Red;//color the new node red
            AddFixUp(newItem);//call method to check for violations and fix
        }
        private void AddFixUp(Node val)
        {
            //Checks Red-Black Tree properties
            while (val != root && val.parent.color == Color.Red)
            {
                /*We have a violation*/
                if (val.parent == val.parent.parent.left)
                {
                    Node Y = val.parent.parent.right;
                    if (Y != null && Y.color == Color.Red)//Case 1: uncle is red
                    {
                        val.parent.color = Color.Black;
                        Y.color = Color.Black;
                        val.parent.parent.color = Color.Red;
                        val = val.parent.parent;
                    }
                    else //Case 2: uncle is black
                    {
                        if (val == val.parent.right)
                        {
                            val = val.parent;
                            LeftRotate(val);
                        }
                        //Case 3: recolor & rotate
                        val.parent.color = Color.Black;
                        val.parent.parent.color = Color.Red;
                        RightRotate(val.parent.parent);
                    }

                }
                else
                {
                    //mirror image of code above
                    Node X = null;

                    X = val.parent.parent.left;
                    if (X != null && X.color == Color.Black)//Case 1
                    {
                        val.parent.color = Color.Red;
                        X.color = Color.Red;
                        val.parent.parent.color = Color.Black;
                        val = val.parent.parent;
                    }
                    else //Case 2
                    {
                        if (val == val.parent.left)
                        {
                            val = val.parent;
                            RightRotate(val);
                        }
                        //Case 3: recolor & rotate
                        val.parent.color = Color.Black;
                        val.parent.parent.color = Color.Red;
                        LeftRotate(val.parent.parent);
                    }
                }
                root.color = Color.Black;//re-color the root black as necessary
            }
        }
        #endregion

        private Node Find(int key)
        {
            bool isFound = false;
            Node temp = root;
            Node item = null;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }
                if (key < temp.val)
                {
                    temp = temp.left;
                }
                if (key > temp.val)
                {
                    temp = temp.right;
                }
                if (key == temp.val)
                {
                    isFound = true;
                    item = temp;
                }
            }
            if (isFound)
            {
                return temp;
            }
            else
            {
                return null;
            }
        }


        #region Delete
        public void DelLeft(int val)
        {
            //first find the node in the tree to Del and assign to item pointer/reference
            Node item = Find(val);
            Node X = null;
            Node Y = null;

            if (item == null)
            {
                return;
            }
            if (item.left == null || item.right == null)
            {
                Y = item;
            }
            else
            {
                Y = TreeSuccessor(item);
            }
            if (Y.left != null)
            {
                X = Y.left;
            }
            else
            {
                X = Y.right;
            }
            if (X != null)
            {
                X.parent = Y;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            else if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }
            else
            {
                Y.parent.right = X;
            }
            if (Y != item)
            {
                item.val = Y.val;
            }
            if (Y.color == Color.Black)
            {
                DelFixUp(X);
            }
        }
        private void DelFixUp(Node X)
        {

            while (X != null && X != root && X.color == Color.Black)
            {
                if (X == X.parent.left)
                {
                    Node W = X.parent.right;
                    if (W.color == Color.Red)
                    {
                        W.color = Color.Black; //case 1
                        X.parent.color = Color.Red; //case 1
                        LeftRotate(X.parent); //case 1
                        W = X.parent.right; //case 1
                    }
                    if (W.left.color == Color.Black && W.right.color == Color.Black)
                    {
                        W.color = Color.Red; //case 2
                        X = X.parent; //case 2
                    }
                    else if (W.right.color == Color.Black)
                    {
                        W.left.color = Color.Black; //case 3
                        W.color = Color.Red; //case 3
                        RightRotate(W); //case 3
                        W = X.parent.right; //case 3
                    }
                    W.color = X.parent.color; //case 4
                    X.parent.color = Color.Black; //case 4
                    W.right.color = Color.Black; //case 4
                    LeftRotate(X.parent); //case 4
                    X = root; //case 4
                }
                else //mirror code from above with "right" & "left" exchanged
                {
                    Node W = X.parent.left;
                    if (W.color == Color.Red)
                    {
                        W.color = Color.Black;
                        X.parent.color = Color.Red;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.color == Color.Black && W.left.color == Color.Black)
                    {
                        W.color = Color.Black;
                        X = X.parent;
                    }
                    else if (W.left.color == Color.Black)
                    {
                        W.right.color = Color.Black;
                        W.color = Color.Red;
                        LeftRotate(W);
                        W = X.parent.left;
                    }
                    W.color = X.parent.color;
                    X.parent.color = Color.Black;
                    W.left.color = Color.Black;
                    RightRotate(X.parent);
                    X = root;
                }
            }
            if (X != null)
                X.color = Color.Black;
        }
        private Node Minimum(Node X)
        {
            while (X.left.left != null)
            {
                X = X.left;
            }
            if (X.left.right != null)
            {
                X = X.left.right;
            }
            return X;
        }
        private Node TreeSuccessor(Node X)
        {
            if (X.left != null)
            {
                return Minimum(X);
            }
            else
            {
                Node Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                return Y;
            }
        }
        #endregion
        public bool Equal(ITree tree)
        {
            throw new NotImplementedException();
        }

    }
}
