using System.Drawing;

namespace 暗黑玫瑰蛇.Class
{
    class Head : SnakeBody
    {
        public Head(int x, int y, int forward_x, int forward_y) : base(x, y, forward_x, forward_y)
        {
            size = 50;

            image = new Bitmap(image, new Size(size, size));
        }

        public override void Move()
        {
            foreach (var sign in signs)
                if (x == sign.x && y == sign.y)
                {
                    forward_x = sign.forward_x;
                    forward_y = sign.forward_y;

                    break;
                }

            x += forward_x;
            y += forward_y;
        }
    }
}