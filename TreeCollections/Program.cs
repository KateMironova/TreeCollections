using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            BsTreeRedBlack lst = new BsTreeRedBlack();
            //lst.Init(new int[] {5, 3, 1, 4, 8, 9, 12, 10});
            //lst.Init(new int[] { 5, 8, 10, 9 });
            //lst.Init(new int[] { 3, 7, 1, 0, 9, 2, 8 });
            lst.Init(new int[] {2, 10, 8, 12, 6, 9 });
            lst.Add(5);

        }
    }
}
