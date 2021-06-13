using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing; 

namespace Kur_rab
{
    public interface IDrawable
    {
        //Возвращает необходимый рназмер
        SizeF GetSize(Graphics gr, Font font);

        // вернет истину если узел на точке
        bool IsAtPoint(Graphics gr, Font font, PointF center_pt, PointF target_pt);

        //рисует обьект  с центром х у
        void Draw(float x, float y, Graphics gr, Pen pen,
            Brush bg_brush, Brush text_brush, Font font);
    }
}
