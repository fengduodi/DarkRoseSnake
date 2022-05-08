using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 暗黑玫瑰蛇.Class
{
    class Food
    {
        int x, y, size;

        Image image;

        public List<SnakeBody> snack;

        public Food()
        {
            size = 40;

            image = Image.FromFile(".\\image\\food.png");
            image = new Bitmap(image, new Size(size, size));
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            int x2 = Form1.windowsWidth / 2 - size / 2 + x;
            int y2 = Form1.windowsHeight / 2 - size / 2 + y;

            G.DrawImage(image, x2, y2);
        }

        public void Check()
        {
            SnakeBody head = snack[0], tail = snack[4];

            double distance = Math.Sqrt(Math.Pow(x - head.x, 2) + Math.Pow(y - head.y, 2));

            int range = head.size / 2;

            if (distance < range)
            {
                snack.Add(new Body(tail.x, tail.y, tail.forward_x, tail.forward_y));
                snack[snack.Count - 1].signs = tail.signs;

                tail.x -= tail.forward_x * SnakeBody.speed;
                tail.y -= tail.forward_y * SnakeBody.speed;

                Reset();
            }
        }

        public void Reset()
        {
            Random random = new Random();

            int range = Form1.backgroundSize / 2 - size / 2 - 30;

            x = random.Next(-range, range);
            y = random.Next(-range, range);
        }
    }
}