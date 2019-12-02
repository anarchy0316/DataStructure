using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.BinTree
{
    public class BinTree<T>
    {

        protected int m_size;
        protected BinNode<T> m_root;
        //更新节点x的高度
        // 特殊情况：单个节点：记作 0
        //          空树： 记作 -1
        protected virtual int UpdateHeight(BinNode<T> x)
        {
            return x.height = 1 + Math.Max(stature(x.m_RChild), stature(x.m_LChild));
        }
        int stature(BinNode<T> x) { return x ? x.height : -1; }
        /// <summary>
        /// 更新X及祖先高度  复杂度就是x节点的深度
        /// </summary>
        /// <param name="x"></param>
        protected void UpdateHeightAbove(BinNode<T> x)
        {
            while (x) //可优化。一旦高度未变，即可终止
            {
                UpdateHeight(x);
                x = x.m_Parent;
            }
        }
        public int size() { return m_size; } // 规模
        public bool Empty() { return !m_root; } //判空
        public BinNode<T> Root
        {
            get { return m_root; }
            set
            {
                m_root = value;
            }
        } //树根
        /*子树接入删除分离等接口*/
        public BinNode<T> InsertAsRc(BinNode<T> x, T e)
        {
            m_size++;
            x.InsertAsRC(e);
            UpdateHeightAbove(x);
            return x.m_RChild;
        }
        public BinNode<T> InsertAsLc(BinNode<T> x, T e)
        {
            m_size++;
            x.InsertAsLC(e);
            UpdateHeightAbove(x);
            return x.m_LChild;
        }
        /*遍历接口*/

        //bad
        public void TraversePreorderRecuscive(BinNode<T> x, Action<T> action)
        {
            if (!x) return;
            action(x.m_data);
            TraversePreorderRecuscive(x.m_LChild, action);
            TraversePreorderRecuscive(x.m_RChild, action);
        }

        public void TraversePreorderInterration(BinNode<T> x, Action<T> action)
        {
            Stack<BinNode<T>> s = new Stack<BinNode<T>>();
            if (x)
            {
                s.Push(x);
            }
            while (s.Count > 0)                                                                             
            {
                var temp = s.Pop();
                action(temp.m_data);
                if (temp.m_RChild)
                {
                    s.Push(temp.m_RChild);
                }
                if (temp.m_LChild)
                {
                    s.Push(temp.m_LChild);
                }
            }
        }

        public static void VisitAlongLeftBranch(BinNode<T> x, Action<T> action, ref Stack<BinNode<T>> s)
        {
            while (x)
            {
                action(x.m_data);
                s.Push(x.m_RChild);
                x = x.m_LChild;
            }
        }
        public void TraversePreorderInterration_2(BinNode<T> x, Action<T> action)
        {
            Stack<BinNode<T>> s = new Stack<BinNode<T>>();

            while (true)
            {
                VisitAlongLeftBranch(x, action, ref s); //总之先访问左侧链，遇到一个访问一个，然后把右孩子推入栈中
                if (s.Count == 0)
                {
                    break;
                }
                x = s.Pop(); //将下一个幸运儿弹出
            }
        }

        public static void GoAlongLeftBranch(BinNode<T> x, ref Stack<BinNode<T>> s)
        {
            while (x)
            {
                s.Push(x);
                x = x.m_LChild;
            }
        }
        public void TraverseInorderInterration(BinNode<T> x, Action<T> action)
        {
            Stack<BinNode<T>> ls = new Stack<BinNode<T>>();
            while (true)
            {
                GoAlongLeftBranch(x, ref ls);
                if (ls.Count == 0) break;
                x = ls.Pop();
                action(x.m_data);
                x = x.m_RChild;
            }
        }

        public static void PostorderHelper(BinNode<T> x, ref Stack<BinNode<T>> s)
        {
            while (x)
            {
                if (x) s.Push(x);
                x = x.m_LChild;
            }
        }

        ///需要记录一下上个出栈的东西
        public void TraversePostorderInterration(BinNode<T> x, Action<T> action)
        {
            Stack<BinNode<T>> s = new Stack<BinNode<T>>();

            PostorderHelper(x, ref s);
            BinNode<T> last = null;
            while (true)
            {
                if (s.Count==0)
                {
                    break;
                }
                var temp = s.Peek();//一定没有左孩子
                if (temp.m_RChild) // 右子不为空
                {
                    if (last==temp.m_RChild)
                    {
                        last = s.Pop();
                        action(last.m_data);
                    }
                    else
                    {
                        PostorderHelper(temp.m_RChild, ref s);
                    }
                }
                else
                {
                    last = s.Pop();
                    action(last.m_data);
                }
              
            }
        }

    }
}
