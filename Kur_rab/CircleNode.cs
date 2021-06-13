using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Kur_rab
{
    public class CircleNode : IDrawable
    {
        public string Text;
         
        public CircleNode(string new_text)
        {
            Text = new_text;
        }

        // вернет размер элемента
        public SizeF GetSize(Graphics gr, Font font)
        {
            return gr.MeasureString(Text, font) + new SizeF(10, 10);
        }

        // рисует обьект в центре х у
        void IDrawable.Draw(float x, float y, Graphics gr, Pen pen, Brush bg_brush, Brush text_brush, Font font)
        { 
            SizeF my_size = GetSize(gr, font);
            // рисуем круг
            RectangleF rect = new RectangleF(
                x - my_size.Width / 2,
                y - my_size.Height / 2,
                my_size.Width, my_size.Height);
            gr.FillEllipse(bg_brush, rect);
            gr.DrawEllipse(pen, rect);

            // отобразить текст
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Center;
                gr.DrawString(Text, font, text_brush, x, y, string_format);
            }
        }

        // определение принадлежности точки к узлу
        bool IDrawable.IsAtPoint(Graphics gr, Font font, PointF center_pt, PointF target_pt)
        {
            SizeF my_size = GetSize(gr, font);
             
            target_pt.X -= center_pt.X;
            target_pt.Y -= center_pt.Y;
             
            float w = my_size.Width / 2;
            float h = my_size.Height / 2;
            return
                target_pt.X * target_pt.X / w / w +
                target_pt.Y * target_pt.Y / h / h
                <= 1;
        }
    }
}
