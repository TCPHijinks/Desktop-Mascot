using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Actor
{
    public class GameObject
    {
        public Point Position;
        public Dimensions Dimension;

        public GameObject()
        {
            Position = new Point();
            Dimension = new Dimensions();
        }

        /// <summary>
        /// Returns point of left side at actor center height.
        /// </summary>
        /// <returns></returns>
        public Point GetLeftSide()
        {
            return new Point(Position.X, Position.Y + Dimension.Height / 2);
        }

        public Point GetRightSide()
        {
            return new Point(Position.X + Dimension.Width, Position.Y + Dimension.Height / 2);
        }

        public Point GetTop()
        {
            return new Point(Position.X + Dimension.Width / 2, Position.Y);
        }

        public Point GetBottom()
        {
            return new Point(Position.X + Dimension.Width / 2, Position.Y + Dimension.Height);
        }


       


        public struct Dimensions
        {
            public int Width;
            public int Height;
        }
    }
}
