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
        public Point Position;
        public Dimensions Dimension;
        public bool CursorDragging { private set; get; }

        public GameObject(Control form)
        {
            Position = new Point();
            Dimension = new Dimensions();

            // Subscribe to key press events.
            form.MouseDown += MouseClick;
            form.MouseUp += MouseUp;

            // Starting position.
            Dimension.Width = form.Width / 2;
            Dimension.Height = form.Height / 3;
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
        public Point GetTopLeft()
        {
            return new Point(Position.X, Position.Y);
        }

        public Point GetTopRight()
        {
            return new Point(Position.X + Dimension.Width, Position.Y);
        }

        public Point GetBottomLeft()
        {
            return new Point(Position.X, Position.Y + Dimension.Height);
        }

        public Point GetBottomRight()
        {
            return new Point(Position.X + Dimension.Width, Position.Y + Dimension.Height);
        }


       


        public struct Dimensions
        {
            public int Width;
            public int Height;
        }
    }
}
