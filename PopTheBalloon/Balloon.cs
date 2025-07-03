using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PopTheBalloon
{
    public class Balloon
    {
        public Rectangle Bounds;
        public Color Color;
        public Point Direction;
        public bool IsDanger;
        public Image BalloonImage;

        public Balloon(Rectangle bounds, Color color, Point direction, Image balloonImage)
        {
            Bounds = bounds;
            Color = color;
            Direction = direction;
            IsDanger = color == Color.Black;
            BalloonImage = balloonImage;
        }

        public void Move()
        {
            Bounds = new Rectangle(Bounds.X + Direction.X, Bounds.Y + Direction.Y, Bounds.Width, Bounds.Height);
        }

        public void Draw(Graphics g)
        {
            //using (Brush b = new SolidBrush(Color))
            //{
            //    g.FillEllipse(b, Bounds);
            //}
            g.DrawImage(BalloonImage, Bounds);
        }
    }
}

