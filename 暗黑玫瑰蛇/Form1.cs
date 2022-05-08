using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using 暗黑玫瑰蛇.Class;

namespace 暗黑玫瑰蛇
{
    public partial class Form1 : Form
    {
        public static int windowsWidth, windowsHeight;

        public static int backgroundSize;

        Image backgroundImage;

        List<SnakeBody> snake;

        Food food;

        AnalogStick analogStick;

        int currentDegree;
        public static int targetDegree;

        bool mouseUp;

        List<Sign> signs;

        Timer updateTimer, turnTimer;

        public Form1()
        {
            InitializeComponent();

            Size = new Size(800 , 800);

            int bias = 15;
            windowsWidth = Width - bias;
            windowsHeight = Height - SystemInformation.ToolWindowCaptionHeight - bias;

            backgroundSize = 700;

            backgroundImage = Image.FromFile(".\\image\\background.jpg");
            backgroundImage = new Bitmap(backgroundImage, new Size(backgroundSize, backgroundSize));

            snake = new List<SnakeBody>();

            food = new Food();
            food.snack = snake;

            analogStick = new AnalogStick();

            signs = new List<Sign>();

            Setting();

            updateTimer = new Timer();
            updateTimer.Interval = 10;
            updateTimer.Tick += new EventHandler(Run);
            updateTimer.Start();

            turnTimer = new Timer();
            turnTimer.Interval = 10;
            turnTimer.Tick += new EventHandler(Turn);
            turnTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.DrawImage(backgroundImage, windowsWidth / 2 - backgroundSize / 2, windowsHeight / 2 - backgroundSize / 2);

            SnakeBody tail = snake[4];

            tail.Show(e);

            for (int i = snake.Count - 1; i >= 0; i--)
                if (snake[i] != tail)
                    snake[i].Show(e);

            food.Show(e);

            analogStick.Show(e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseUp = false;

            analogStick.StickMove(e.X, e.Y);

            analogStick.SetTargetDegree(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseUp)
            {
                analogStick.StickMove(e.X, e.Y);

                analogStick.SetTargetDegree(e.X, e.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp = true;

            analogStick.ResetStick();
        }

        public void Run(object sender, EventArgs e)
        {
            foreach (var body in snake)
               body.Move();

            food.Check();

            if (GameOver())
            {
                updateTimer.Stop();
                turnTimer.Stop();

                DialogResult dialogResult = MessageBox.Show("暗黑蛇的長度：" + snake.Count.ToString() + "\n\n再玩一次？",
                                                            "遊戲結束",
                                                            MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    Setting();

                    updateTimer.Start();
                    turnTimer.Start();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Environment.Exit(0);
                }
            }

            Invalidate();
        }

        void Turn(object sender, EventArgs e)
        {
            if (currentDegree != targetDegree)
            {
                int degreeDifference = Math.Abs(currentDegree - targetDegree);

                int turnSpeed = 10;

                if (degreeDifference < turnSpeed)
                    currentDegree = targetDegree;
                else if (degreeDifference > 180)
                {
                    if (currentDegree > targetDegree)
                        currentDegree = (currentDegree + turnSpeed) % 360;
                    else
                        currentDegree = (currentDegree + 360 - turnSpeed) % 360;
                }
                else
                {
                    if (currentDegree > targetDegree)
                        currentDegree -= turnSpeed;
                    else
                        currentDegree += turnSpeed;
                }

                double radian = currentDegree * Math.PI / 180;

                SnakeBody head = snake[0];

                int forward_x = (int)(Math.Cos(radian) * SnakeBody.speed);
                int forward_y = (int)(-Math.Sin(radian) * SnakeBody.speed);

                signs.Add(new Sign(head.x, head.y, forward_x, forward_y));
            }
        }

        bool GameOver()
        {
            SnakeBody head = snake[0];

            int border = backgroundSize / 2 - head.size / 2;

            if (head.x < -border || head.x > border ||
                head.y < -border || head.y > border)
                return true;

            for (int i = 1; i < snake.Count; i++)
            {
                double distance = Math.Sqrt(Math.Pow(head.x - snake[i].x, 2) + Math.Pow(head.y - snake[i].y, 2));

                int range = head.size / 2 - snake[i].size / 2;

                if (distance < range)
                    return true;
            }

            return false;
        }

        void Setting()
        {
            snake.Clear();

            int speed = SnakeBody.speed;

            snake.Add(new Head(0, 0, speed, 0));
            snake.Add(new Body(-30, 0, speed, 0));
            snake.Add(new Body(-55, 0, speed, 0));
            snake.Add(new Body(-80, 0, speed, 0));
            snake.Add(new Tail(-105, 0, speed, 0));

            foreach (var body in snake)
                body.signs = signs;

            food.Reset();

            targetDegree = 0;
            currentDegree = 0;

            mouseUp = true;

            signs.Clear();
        }
    }
}