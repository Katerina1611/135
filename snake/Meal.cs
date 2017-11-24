using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace snake
{
    class Meal : Pnt
    {
        public Meal(Random rnd)
        {
            _x = rnd.Next(SZX);
            _y = rnd.Next(SZY);
        }
        
        public override void Paint(Graphics g, int width)
        {
            Pen p = new Pen(Color.Red);
            g.DrawRectangle(p, _x * width, _y * width, width, width);
        }
    }
}
