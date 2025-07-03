using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopTheBalloon
{
    public partial class Form1 : Form
    {
        private List<Balloon> balloons = new List<Balloon>();
        private Random rand = new Random();
        private int score = 0;
        private bool ShowStartScreen = true;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            lblScore.Visible = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            balloons.Clear();
            score = 0;
            lblScore.Text = "Score: 0";
            gameTimer.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            btnHelp.Visible = false;

            lblScore.Visible = true;
            


            ShowStartScreen = false;
            balloons.Clear();
            score = 0;
            lblScore.Text = "Score: 0";
            gameTimer.Start();
            lblScore.Visible = true;
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (rand.NextDouble() < 0.1)
                AddRandomBalloon();

            foreach (var balloon in balloons)
                balloon.Move();

            balloons.RemoveAll(b => !ClientRectangle.IntersectsWith(b.Bounds));
            Invalidate();
        }
        private void AddRandomBalloon()
        {

            int size = rand.Next(50, 80);
            int x = rand.Next(ClientSize.Width - size);
            int y = ClientSize.Height;

            int baseSpeed = 20;
            int extraSpeed = score / 5;
            int speed = baseSpeed + rand.Next(0, 2) + extraSpeed;
            Point direction = new Point(0, -speed);

            int type = rand.Next(0, 5);
            Image image;
            Color color;

            switch (type)
            {
                case 0:
                    image = Properties.Resources.red_balloon2;
                    color = Color.Red;
                    break;
                case 1:
                    image = Properties.Resources.blue_balloon_2;
                    color = Color.Blue;
                    break;
                case 2:
                    image = Properties.Resources.green_balloon_2;
                    color = Color.Green;
                    break;
                case 3:
                    image = Properties.Resources.yellow_ballon2;
                    color = Color.Yellow;
                    break;
                default: 
                    image = Properties.Resources.black_balloon;
                    color = Color.Black;
                    break;
            }

            balloons.Add(new Balloon(
                new Rectangle(x, y, size, size),
                color,
                direction,
                image
            ));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (ShowStartScreen)
            {
                g.DrawImage(Properties.Resources.balloons_pop, ClientRectangle);
            }
            foreach (var balloon in balloons)
                balloon.Draw(e.Graphics);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for(int i = balloons.Count - 1;i >= 0; i--)
            {
                if (balloons[i].Bounds.Contains(e.Location))
                {
                    if (balloons[i].IsDanger)
                    {
                        gameTimer.Stop();
                        DialogResult result = MessageBox.Show(
                            "💣 Game Over! You clicked a black balloon.\n\nDo you want to restart?",
                            "Game Over",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );
                        if(result == DialogResult.Yes)
                        {
                            RestartGame();
                        }
                        if(result == DialogResult.No)
                        {
                            Application.Exit();
                        }
                    }
                    else
                    {
                        score++;
                        lblScore.Text = $"Score: {score}";
                        balloons.RemoveAt(i);
                    }
                    break;
                }
            }
        }
        private void RestartGame()
        {
            gameTimer.Stop();

            
            balloons.Clear();            
            score = 0;                   
            lblScore.Text = "Score: 0";

           
            lblScore.Visible = true;
            btnStart.Visible = false;
          
            gameTimer.Start();
            Invalidate();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help instructionForm = new Help();
            instructionForm.ShowDialog();
        }
    }
}
