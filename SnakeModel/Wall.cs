using System;
using System.Drawing;

namespace SnakeModel
{
    public class Wall : Pnt
    {
        public Wall(Random rnd)
        {
            _x = rnd.Next(SZX);
            _y = rnd.Next(SZY);
        }

        public override void Paint(Graphics g, int width)
        {
            Brush b = new SolidBrush(Color.Black);
            g.FillRectangle(b, _x * width, _y * width, width, width);
        }
    }
}