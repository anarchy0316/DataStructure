
using DataStructure.BinTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var BinTree = new BinTree<int>();
            BinTree.Root = new BinNode<int>(0,null);
           var b1= BinTree.Root.InsertAsLC(1);
            var b2=  BinTree.Root.InsertAsRC(2);
            b1.InsertAsLC(3);
            b1.InsertAsRC(4);
            b2.InsertAsLC(5);
            b2.InsertAsRC(6);
            BinTree.TraversePostorderInterration(BinTree.Root,(x) => Console.WriteLine(x));
        }
    }
}
