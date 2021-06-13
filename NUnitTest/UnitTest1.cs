using NUnit.Framework;
using Kur_rab;

namespace NUnitTest
{
    public class Tests
    {
        private TreeNode<CircleNode> root =
         new TreeNode<CircleNode>(new CircleNode("������"));
        TreeNode<CircleNode> a_node = new TreeNode<CircleNode>(new CircleNode("�"));
        TreeNode<CircleNode> b_node = new TreeNode<CircleNode>(new CircleNode("�"));
        TreeNode<CircleNode> c_node = new TreeNode<CircleNode>(new CircleNode("�"));
        TreeNode<CircleNode> d_node = new TreeNode<CircleNode>(new CircleNode("�"));
        TreeNode<CircleNode> e_node = new TreeNode<CircleNode>(new CircleNode("�"));
        TreeNode<CircleNode> f_node = new TreeNode<CircleNode>(new CircleNode("�"));
        TreeNode<CircleNode> g_node = new TreeNode<CircleNode>(new CircleNode("�"));
        TreeNode<CircleNode> h_node = new TreeNode<CircleNode>(new CircleNode("�"));

        [SetUp]
        public void Setup()
        { 
            root.AddChild(a_node);
            root.AddChild(b_node);
            a_node.AddChild(c_node);
            a_node.AddChild(d_node);
            b_node.AddChild(e_node);
            b_node.AddChild(f_node);
            b_node.AddChild(g_node);
            e_node.AddChild(h_node);
        }

        [Test]
        public void Test1()
        {  
            Assert.AreEqual(root.Children.Count,2);
            root.DeleteNode(b_node);
            Assert.AreEqual(root.Children.Count, 1);  
        }

        [Test]
        public void Test2()
        { 
            Assert.AreEqual(a_node.DeleteNode(f_node), false);   
        }

        [Test]
        public void Test3()
        {
            Assert.AreEqual(b_node.DeleteNode(f_node), true); 
        }

        [Test]
        public void Test4()
        {
            b_node.AddChild(new TreeNode<CircleNode>(new CircleNode("�"))); 
            Assert.Pass();
        }
    }
}