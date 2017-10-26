using System;
using NUnit.Framework;
using TreeCollections;

namespace UnitTestProject1
{
    [TestFixture(typeof(LinkTree))]
    [TestFixture(typeof(BsTreeR_4Del))]
    [TestFixture(typeof(BsTree_4Del))]
    public class UnitTest2<TTree> where TTree : ITreeDel, new()
    {
        ITreeDel lst = new TTree();

        [SetUp]
        public void SetUp()
        {
            lst.Clear();
        }

        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 8, 2, 9 }, new int[] { 3, 8, 9, 1, 0, 2 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 7, 1, 2, 9, 0, 8 }, 3)]
        [TestCase(new int[] { 9, 10, 6, 8, 7, 4, 3 }, new int[] { 9, 10, 8, 4, 7, 3 }, 6)]
        public void TestDelRight(int[] input, int[] res, int val)
        {
            ITreeDel compare = new TTree();
            compare.Init(res);
            lst.Init(input);
            lst.DelRight(val);
            Assert.AreEqual(compare.Size(), lst.Size());
            Assert.IsTrue(lst.Equal(compare));
        }
        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 8, 2, 9 }, new int[] { 3, 8, 9, 1, 0, 2 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 2, 1, 0, 7, 9, 8 }, 3)]
        [TestCase(new int[] { 9, 10, 6, 8, 7, 4, 3 }, new int[] { 9, 10, 8, 4, 7, 3 }, 6)]
        public void TestDelLeft(int[] input, int[] res, int val)
        {
            ITreeDel compare = new TTree();
            compare.Init(res);
            lst.Init(input);
            lst.DelLeft(val);
            Assert.AreEqual(compare.Size(), lst.Size());
            Assert.IsTrue(lst.Equal(compare));
        }

        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 8, 2, 9 }, new int[] { 3, 8, 9, 1, 0, 2 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 1, 0, 2, 7, 9, 8 }, 3)]
        [TestCase(new int[] { 9, 10, 6, 8, 7, 4, 3 }, new int[] { 9, 10, 8, 4, 7, 3 }, 6)]
        [TestCase(new int[] { 5, 3, 1, 4, 8, 9, 12, 10 }, new int[] { 3, 1, 4, 8, 9, 12, 10 }, 5)]
        [TestCase(new int[] { 5, 8, 10, 9 }, new int[] { 8, 10, 9 }, 5)]
        [TestCase(new int[] { 5, 3, 8, 10, 9 }, new int[] { 3, 8, 10, 9 }, 5)]
        public void TestDelLeftRotate(int[] input, int[] res, int val)
        {
            ITreeDel compare = new TTree();
            compare.Init(res);
            lst.Init(input);
            lst.DelLeftRotate(val);
            Assert.AreEqual(compare.Size(), lst.Size());
            Assert.IsTrue(lst.Equal(compare));
        }

        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 8, 2, 9 }, new int[] { 3, 1, 0, 2, 8, 9 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 7, 1, 0, 2, 9, 8 }, 3)]
        [TestCase(new int[] { 9, 10, 6, 8, 7, 4, 3 }, new int[] { 9, 8, 7, 4, 3, 10 }, 6)]
        [TestCase(new int[] { 5, 3, 1, 4, 8, 9, 12, 10 }, new int[] { 8, 3, 1, 4, 9, 12, 10}, 5)]
        [TestCase(new int[] { 5, 8, 10, 9}, new int[] { 8, 10, 9 }, 5)]
        [TestCase(new int[] { 5, 3, 8, 10, 9 }, new int[] { 8, 3, 10, 9 }, 5)]
        public void TestDelRightRotate(int[] input, int[] res, int val)
        {
            ITreeDel compare = new TTree();
            compare.Init(res);
            lst.Init(input);
            lst.DelRightRotate(val);
            Assert.AreEqual(compare.Size(), lst.Size());
            Assert.IsTrue(lst.Equal(compare));
        }
    }
}
