using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCollections
{
    public interface ITreeDel
    {
        void Init(int[] ini);
        void Add(int val);
        void DelLeft(int val);
        void DelRight(int val);
        void DelLeftRotate(int val);
        void DelRightRotate(int val);
        int[] ToArray();
        int Size();
        int Height();
        int Width();
        int Nodes();
        int Leaves();
        void Reverse();
        void Clear();
        bool Equal(ITreeDel tree);
    }
}
