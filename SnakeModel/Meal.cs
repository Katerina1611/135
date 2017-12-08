using System;
using System.Drawing;

namespace SnakeModel
{
    public class Meal : Pnt
    {
        public Meal(Random rnd)
        {
            _x = rnd.Next(SZX);
            _y = rnd.Next(SZY);
        }

        public override void Paint(Graphics g, int width)
        {
            var p = new Pen(Color.Red);
            g.DrawRectangle(p, _x * width, _y * width, width, width);
        }
    }
}