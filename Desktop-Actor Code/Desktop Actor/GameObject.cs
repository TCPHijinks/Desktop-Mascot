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
        private readonly Rectangle _playArea;
        public Point Position;
        public Dimensions MyDimensions;
        public bool CursorDragging { private set; get; }

        public GameObject(Control form, Rectangle playAreaBoundary)
        {
            this._playArea = playAreaBoundary;

            Position = new Point();
            MyDimensions = new Dimensions();

            // Subscribe to key press events.
            form.MouseDown += MouseClick;
            form.MouseUp += MouseUp;

            // Starting position.
            MyDimensions.Width = form.Width / 2;
            MyDimensions.Height = form.Height / 3;
        }

        /// <summary>
        /// Keeps game object inside of the play area's boundaries.
        /// </summary>
        public void KeepInsidePlayArea()
        {
            // If top of GameObject is higher than top of the boundary.
            if (Position.Y < _playArea.Y)
            {
                Position.Y = _playArea.Y;
            }
            // If bottom of GameObject is lower than bottom of the boundary.
            else if ((Position.Y + MyDimensions.Height) > (_playArea.Y + _playArea.Height))
            {
                Position.Y = _playArea.Y + (_playArea.Height - MyDimensions.Height);
            }

            // If left of GameObject is outside of boundary's left side.
            if (Position.X < _playArea.X)
            {
                Position.X = _playArea.X;
            }
            // If right of GameObject is outside of boundary's right side.
            else if ((Position.X + MyDimensions.Width) > (_playArea.X + _playArea.Width))
            {
                Position.X = (_playArea.X + _playArea.Width) - MyDimensions.Width;
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
            return new Point(Position.X + MyDimensions.Width / 2, Position.Y);
        }

        public Point Bottom()
        {
            return new Point(Position.X + MyDimensions.Width / 2, Position.Y + MyDimensions.Height);
        }

        public Point Left()
        {
            return new Point(Position.X, Position.Y + MyDimensions.Height / 2);
        }

        public Point Right()
        {
            return new Point(Position.X + MyDimensions.Width, Position.Y + MyDimensions.Height / 2);
        }

        public struct Dimensions
        {
            public int Width;
            public int Height;
        }
    }
}