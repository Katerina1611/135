using System.Drawing;

namespace SnakeModel
{
    public class Head : Pnt
    {
        private int _dir;

        public Head()
        {
            _dir = 0;
            _x = SZX / 2;
            _y = SZY / 2;
        }

        public override void Paint(Graphics g, int width)
        {
            var p = new Pen(Color.GreenYellow);
            g.DrawRectangle(p, _x * width, _y * width, width, width);
        }

        public void Move()
        {
            if (_dir == 0)
                _y++;
            else if (_dir == 1)
                _x++;
            else if (_dir == 2)
                _y--;
            else
                _x--;
            _x = (_x + SZX) % SZX;
            _y = (_y + SZY) % SZY;
        }

        public void Change(int nd)
        {
            if (nd != (_dir + 2) % 4)
                _dir = nd;
        }
    }
}