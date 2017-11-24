using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace snake
{
   
    class head : Pnt
    {
        
        int dir;
        public head()
        {
            dir = 0;
            _x = SZX / 2;
            _y = SZY / 2;
        }
        public override void Paint(Graphics g, int width)
        {
            Pen p = new Pen(Color.GreenYellow);
            g.DrawRectangle(p, _x * width, _y * width, width, width);
        }
        public void Move()
        {
            if (dir == 0)
            {
                _y++; 
            } 
            else if (dir == 1)
            {
                _x++;
            }
            else if(dir == 2)
            {
                _y--;
            }
            else 
            {
                _x--;    
            }
            _x = (_x + SZX) % SZX;
            _y = (_y + SZY) % SZY;
        }
        public void change(int nd)
        {
            if (nd != (dir + 2) % 4)
                dir = nd;
        }
    }
}
