using System;
using System.Collections.Generic;

namespace DataStructure.BinTree
{
    public class BinNode<T>
    {
        public BinNode<T> m_Parent, m_LChild, m_RChild;
        public T m_data;
        public int height;

        public static implicit operator bool(BinNode<T> exists)
        {
            return exists != null;
        }
        /// <summary>
        /// 规模 以自己为root的子树，包括自己在内所有节点的的总数
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            int s = 1;
            if (m_LChild) s += m_LChild.Size();
            if (m_RChild) s += m_RChild.Size();
            return s;

        }
        public BinNode(T data, BinNode<T> parent)
        {
            m_data = data;
            m_Parent = parent;
        }
        public BinNode<T> InsertAsLC(T data)
        {
            return m_LChild = new BinNode<T>(data, this);
        }

        public BinNode<T> InsertAsRC(T data)
        {
            return m_RChild = new BinNode<T>(data, this);
        }
        //返回中序意义上的直接后继
        BinNode<T> Succ()
        {
            return null;
        }

        public void TravelLevel(Action<T> visit)
        {
            Queue<BinNode<T>> q = new Queue<BinNode<T>>();
            q.Enqueue(this);//根节点入栈
            while (q.Count>0)
            {
                BinNode<T> a = q.Dequeue();
                visit(a.m_data);
                if (a.m_LChild)                                                                                                                     
                {
                    q.Enqueue(a.m_LChild);
                }

                if (a.m_RChild)
                {
                    q.Enqueue(a.m_RChild);
                }
            }
        }

    }
}
