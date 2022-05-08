using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 暗黑玫瑰蛇.Class
{
    class SnakeBody
    {
        public int x, y, size, forward_x, forward_y;

        public static int speed = 5;

        protected Image image;

        public List<Sign> signs;

        public SnakeBody(int x, int y, int forward_x, int forward_y)
        {
            this.x = x;
            this.y = y;

            this.forward_x = forward_x;
            this.forward_y = forward_y;

            image = Image.FromFile(".\\image\\head.png");
        }

        public virtual void Move() { }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            int x2 = Form1.windowsWidth / 2 - size / 2 + x;
            int y2 = Form1.windowsHeight / 2 - size / 2 + y;

            G.DrawImage(image, x2, y2);
        }
    }
}