using System;
using System.Drawing;
using System.Windows.Forms;

namespace 暗黑玫瑰蛇.Class
{
    class AnalogStick
    {
        int borderX, borderY, borderSize;

        int x, y, size;

        public AnalogStick()
        {
            borderX = 650;
            borderY = 650;
            borderSize = 100;

            ResetStick();
            size = 50;
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            Color color = Color.FromArgb(80, 0, 0, 255);

            G.DrawEllipse(new Pen(color, 4), borderX - borderSize / 2 + 2, borderY - borderSize / 2 + 2, borderSize - 4, borderSize - 4);

            G.FillEllipse(new SolidBrush(color), x - size / 2, y - size / 2, size, size);
        }

        public void ResetStick()
        {
            x = borderX;
            y = borderY;
        }

        public void StickMove(int ex, int ey)
        {
            int distance = (int)Math.Sqrt(Math.Pow(ex - borderX, 2) + Math.Pow(ey - borderY, 2));
            
            int range = borderSize / 2 - size / 2;

            if (distance < range)
            {
                x = ex;
                y = ey;
            }
            else
            {
                x = borderX + (ex - borderX) * range / distance;
                y = borderY + (ey - borderY) * range / distance;
            }
        }

        public void SetTargetDegree(int ex, int ey)
        {
            double distance = Math.Sqrt(Math.Pow(ex - borderX, 2) + Math.Pow(ey - borderY, 2));

            if (distance != 0)
            {
                int targetDegree = (int)(Math.Acos((ex - borderX) / distance) * 180 / Math.PI);

                if (ey > borderY)
                    targetDegree = 359 - targetDegree;

                Form1.targetDegree = targetDegree;
            }
        }
    }
}