using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Kur_rab;

namespace example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private TreeNode<CircleNode> root =
          new TreeNode<CircleNode>(new CircleNode("Корень"));

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode<CircleNode> a_node = new TreeNode<CircleNode>(new CircleNode("А"));
            TreeNode<CircleNode> b_node = new TreeNode<CircleNode>(new CircleNode("Б"));
            TreeNode<CircleNode> c_node = new TreeNode<CircleNode>(new CircleNode("В"));
            TreeNode<CircleNode> d_node = new TreeNode<CircleNode>(new CircleNode("Г"));
            TreeNode<CircleNode> e_node = new TreeNode<CircleNode>(new CircleNode("Д"));
            TreeNode<CircleNode> f_node = new TreeNode<CircleNode>(new CircleNode("Е"));
            TreeNode<CircleNode> g_node = new TreeNode<CircleNode>(new CircleNode("Ж"));
            TreeNode<CircleNode> h_node = new TreeNode<CircleNode>(new CircleNode("З"));

            root.AddChild(a_node);
            root.AddChild(b_node);
            a_node.AddChild(c_node);
            a_node.AddChild(d_node);
            b_node.AddChild(e_node);
            b_node.AddChild(f_node);
            b_node.AddChild(g_node);
            e_node.AddChild(h_node);

            ArrangeTree();
        }

        private void ArrangeTree()
        {
            using (Graphics gr = pictureBox1.CreateGraphics())
            {

                float xmin = 0, ymin = 0;
                root.Draw(gr, ref xmin, ref ymin);
                xmin = (pictureBox1.ClientSize.Width - xmin) / 2;
                ymin = 10;
                root.Draw(gr, ref xmin, ref ymin);

            }

            pictureBox1.Refresh();
        }
        private TreeNode<CircleNode> SelectedNode;

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            FindNodeUnderMouse(e.Location);

            if (SelectedNode != null)
            {
                contextMenuStrip1.Enabled = (SelectedNode != root);
                contextMenuStrip1.Show(this, e.Location);
            }
        }


        private void FindNodeUnderMouse(PointF pt)
        {
            using (Graphics gr = pictureBox1.CreateGraphics())
            {
                SelectedNode = root.NodeAtPoint(gr, pt);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            root.DrawTree(e.Graphics);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                TreeNode<CircleNode> child =
                    new TreeNode<CircleNode>(new CircleNode(form2.textBox1.Text));
                SelectedNode.AddChild(child);
                ArrangeTree();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Действительно хотите удалить?",
                "Удаление", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                root.DeleteNode(SelectedNode);
                ArrangeTree();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
