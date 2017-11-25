using System.Drawing;

namespace SnakeModel
{
    public abstract class Pnt
    {
        protected const int SZX = 40;
        protected const int SZY = 40;
        protected int _x, _y;

        public int X => _x;

        public int Y => _y;

        public abstract void Paint(Graphics g, int width);
    }
}