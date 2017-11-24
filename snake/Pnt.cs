using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace snake
{
    abstract class Pnt
    {
        protected int _x, _y;
        protected const int SZX = 40;
        protected const int SZY = 40;
       
        public Pnt()
        {
           
        }
        public int X
        {
            get
            {
                return _x;
            }
        }
        public int Y
        {
            get
            {
                return _y;
            }
        }  
        public abstract void Paint(Graphics g, int width);
    }
}
