using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Actor
{
    public class GameObject
    {
        List<Rectangle> boundaries;
        public Point Position;
        public Dimensions Dimension;
        public bool CursorDragging { private set; get; }

        public GameObject(Control form, List<Rectangle> boundaries)
        {
            this.boundaries = boundaries;

            Position = new Point();
            Dimension = new Dimensions();

            // Subscribe to key press events.
            form.MouseDown += MouseClick;
            form.MouseUp += MouseUp;

            // Starting position.
            Dimension.Width = form.Width / 2;
            Dimension.Height = form.Height / 3;
        }


        public void Inside()
        {
           
            foreach (Rectangle boundary in boundaries)
            {      
                if (boundary.Contains(Top() ))
                {
                    Position.Y = boundary.Y + boundary.Height;
                }
                if (boundary.Contains(Bottom() ))
                {
                    Position.Y = boundary.Y - Dimension.Height;
                }
                if (boundary.Contains(Left() ))
                {
                    Position.X = boundary.X + boundary.Width;
                }
                if (boundary.Contains(Right() ))
                {
                    Position.X = boundary.X - Dimension.Width;
                }

            }
           
        }


        #region Event Methods

        private void MouseClick(object sender, MouseEventArgs e)
        {
            CursorDragging = true;
            Console.WriteLine("CALL");
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            CursorDragging = false;
            Console.WriteLine("RELEASE");
        }

        #endregion Event Methods



        /// <summary>
        /// Returns point of left side at actor center height.
        /// </summary>
        /// <returns></returns>
        public Point Top()
        {
            return new Point(Position.X + Dimension.Width/2, Position.Y);
        }

        public Point Bottom()
        {
            return new Point(Position.X + Dimension.Width/2, Position.Y + Dimension.Height);
        }

        public Point Left()
        {
            return new Point(Position.X, Position.Y + Dimension.Height / 2);
        }

        public Point Right()
        {
            return new Point(Position.X + Dimension.Width, Position.Y + Dimension.Height / 2);
        }


       


        public struct Dimensions
        {
            public int Width;
            public int Height;
        }
    }
}
