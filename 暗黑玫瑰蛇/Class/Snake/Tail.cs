using System.Drawing;

namespace 暗黑玫瑰蛇.Class
{
    class Tail : SnakeBody
    {
        public Tail(int x, int y, int forward_x, int forward_y) : base(x, y, forward_x, forward_y)
        {
            size = 30;

            image = new Bitmap(image, new Size(size, size));
        }

        public override void Move()
        {
            x += forward_x;
            y += forward_y;

            if (signs.Count != 0 && x == signs[0].x && y == signs[0].y)
            {
                forward_x = signs[0].forward_x;
                forward_y = signs[0].forward_y;

                signs.Remove(signs[0]);
            }
        }
    }
}