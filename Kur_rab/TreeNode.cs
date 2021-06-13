using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Kur_rab
{
    public class TreeNode<T> where T : IDrawable
    { 
        public T Data;

        // дочерние узлы в дереве
        public List<TreeNode<T>> Children = new List<TreeNode<T>>();

        public float HOffset = 5;
        public float VOffset = 10; 
        public float Indent = 20;
        public float SpotRadius = 5; 
        private PointF DataCenter;  
        public Font MyFont = null;
        public Pen MyPen = Pens.Black;
        public Brush FontBrush = Brushes.Black;
        public Brush BgBrush = Brushes.White;

        // установить параметры отрисовки.
        public void SetTreeDrawingParameters(float h_offset, float v_offset, float indent, float node_radius)
        {
            HOffset = h_offset;
            VOffset = v_offset;
            Indent = indent;
            SpotRadius = node_radius; 
             
            foreach (TreeNode<T> child in Children)
                child.SetTreeDrawingParameters(h_offset, v_offset,
                    indent, node_radius);
        }
         
        public TreeNode(T new_data)
            : this(new_data, new Font("Times New Roman", 12))
        {
            Data = new_data;
        }
        public TreeNode(T new_data, Font fg_font)
        {
            Data = new_data;
            MyFont = fg_font;
        } 
        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }

        // Отрисовка 
        public void Draw(Graphics gr, ref float xmin, ref float ymin)
        { 
            SizeF my_size = Data.GetSize(gr, MyFont); 
            float x = xmin;
            float biggest_ymin = ymin + my_size.Height;
            float subtree_ymin = ymin + my_size.Height + VOffset;
            foreach (TreeNode<T> child in Children)
            { 
                float child_ymin = subtree_ymin;
                child.Draw(gr, ref x, ref child_ymin); 
                if (biggest_ymin < child_ymin) biggest_ymin = child_ymin; 
                x += HOffset;
            }
             
            if (Children.Count > 0) x -= HOffset; 
            float subtree_width = x - xmin;
            if (my_size.Width > subtree_width)
            { 
                x = xmin + (my_size.Width - subtree_width) / 2;
                foreach (TreeNode<T> child in Children)
                { 
                    child.Draw(gr, ref x, ref subtree_ymin); 
                    x += HOffset;
                } 
                subtree_width = my_size.Width;
            }
             
            DataCenter = new PointF(
                xmin + subtree_width / 2,
                ymin + my_size.Height / 2); 
            xmin += subtree_width; 
            ymin = biggest_ymin;
        }
        // Отрисовка дерева
        public void DrawTree(Graphics gr, ref float x, float y)
        { 
            Draw(gr, ref x, ref y); 
            DrawTree(gr);
        }

        // Отрисовка дерева.
        public void DrawTree(Graphics gr)
        { 
            DrawSubtreeLinks(gr); 
            DrawSubtreeNodes(gr);
        }

        // Отрисовка линий 
        private void DrawSubtreeLinks(Graphics gr)
        {
            foreach (TreeNode<T> child in Children)
            { 
                gr.DrawLine(MyPen, DataCenter, child.DataCenter); 
                child.DrawSubtreeLinks(gr);
            }
        }


        // Отрисовка дочерних узлов.
        private void DrawSubtreeNodes(Graphics gr)
        { 
            Data.Draw(DataCenter.X, DataCenter.Y, gr, MyPen, BgBrush, FontBrush, MyFont);
             
            foreach (TreeNode<T> child in Children)
            {
                child.DrawSubtreeNodes(gr);
            }
        }

        // возвращает узел по точке
        public TreeNode<T> NodeAtPoint(Graphics gr, PointF target_pt)
        { 
            if (Data.IsAtPoint(gr, MyFont, DataCenter, target_pt)) return this;
             
            foreach (TreeNode<T> child in Children)
            {
                TreeNode<T> hit_node = child.NodeAtPoint(gr, target_pt);
                if (hit_node != null) return hit_node;
            }

            return null;
        }

        public bool DeleteNode(TreeNode<T> target)
        { 
            foreach (TreeNode<T> child in Children)
            { 
                if (child == target)
                { 
                    Children.Remove(child);
                    return true;
                }
                 
                if (child.DeleteNode(target)) return true;
            }
             
            return false;
        }
    }
}
