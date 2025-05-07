using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace _25._04._28_PriorityQueue
{
    class BSTreeNode
    {
        private int data;
        
        public BSTreeNode? lChild;
        public BSTreeNode? rChild;

        private BSTreeNode? parent;
        
        private BSTree? treeOwner;

        public BSTreeNode(int _data)
        {
            data = _data;
        }

        public int GetData()
        {
            return data;
        }
        public void SetParent(BSTreeNode _parent)
        {
            parent = _parent;
        }
        public void SetChild(BSTreeNode child)
        {
            if (data > child.data) 
            {
                if (lChild == null)
                {
                    lChild = child;
                    child.SetParent(this);
                }
                else
                {
                    lChild.SetChild(child);
                }
            }
            else 
            {
                if (rChild == null)
                {
                    rChild = child;
                    child.SetParent(this);
                }
                else
                {
                    rChild.SetChild(child); 
                }
            }
        }
        public void SetTreeOwner(BSTree tree)
        {
            treeOwner = tree;
        }
    }

    internal class BSTree
    {
        private BSTreeNode root;
        private int nodeCount;

        public void SetRoot(BSTreeNode node)
        {
            root = node;
        }

        public void AddNode(BSTreeNode node)
        {
            // 루트가 없음
            if (nodeCount == 0)
            {
                SetRoot(node);
            }

            // 루트가 있음
            else
            {
                root.SetChild(node);
            }

            node.SetTreeOwner(this);
            nodeCount++;
        }

        //public void InOrderSearch()
        //{
        //    if (root == null)
        //    {
        //        return;
        //    }

        //    root.InOrderSearch();
        //}

        public void LevelOrderSearch()
        {
            if (root == null)
            {
                return;
            }

            Queue<BSTreeNode> queue = new Queue<BSTreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                BSTreeNode current = queue.Dequeue();
                Console.WriteLine(current.GetData());

                if (current.lChild != null)
                {
                    queue.Enqueue(current.lChild);
                }

                if (current.rChild != null)
                {
                    queue.Enqueue(current.rChild);
                }
            }
        }

        public bool Contains(int data)
        {
            if (root == null)
            {
                return false;
            }

            Stack<BSTreeNode> st = new Stack<BSTreeNode>();
            st.Push(root);

            while (st.Count > 0)
            {
                BSTreeNode current = st.Pop();
                if (current.GetData() == data)
                {
                    return true;
                }
                Console.WriteLine(current.GetData());

                if (current.lChild != null)
                {
                    st.Push(current.lChild);
                }

                if (current.rChild != null)
                {
                    st.Push(current.rChild);
                }
            }
            return false;   
        }
    }
}
