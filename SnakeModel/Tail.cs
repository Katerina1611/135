using System.Drawing;

namespace SnakeModel
{
    public class Tail : Pnt
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

        public override void Paint(Graphics g, int width)
        {
            var p = new Pen(Color.Green);
            g.DrawRectangle(p, _x * width, _y * width, width, width);
        }
    }
}