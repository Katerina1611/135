using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace snake
{
    class Tail : Pnt
    {
        public Tail(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public void Move(int x, int y)
        {
            _x = x;
            _y = y;
        }
        
        public override void Paint(System.Drawing.Graphics g, int width)
        {
            Pen p = new Pen(Color.Green);
            g.DrawRectangle(p, _x * width, _y * width, width, width);
        }
        
    }
}
