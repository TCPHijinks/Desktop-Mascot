using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop_Actor
{
    public static class EnvironmentHandler
    {
        public static int screenHeight = 1080;
        public static int screenWidth = 2080;
        public static List<Rectangle> environment { private set; get; }

        static EnvironmentHandler()
        {
            AddBoundary(new Rectangle(0,-5, screenWidth, 5));// Screen Top
            AddBoundary(new Rectangle(0, screenHeight, screenWidth, 5));// Screen Bottom.
            AddBoundary(new Rectangle(-5, 0, 5, screenHeight));// Screen Left.
            AddBoundary(new Rectangle(screenWidth, 0, 5, screenHeight));// Screen Right.
        }
        /*
        public void InsideTerrain(int posX, int posY)
        {
            var cornerPoints = new List<Point>();
            cornerPoints.Add(new Point(posX, posY));                // Top left.
            cornerPoints.Add(new Point(posX+objectWidth, posY));    // Top right.
            cornerPoints.Add(new Point(posX, posY+objectHeight));   // Bottom left.
            cornerPoints.Add(new Point(posX+objectWidth, posY+objectHeight)); // Bottom right.

            foreach (Boundary boundary in environment)
            {
                foreach(Point corner in cornerPoints)
                    if(boundary.Inside(corner))
                        Console.WriteLine("INSIDE!");
            }
         //   return true;
        }
        */
        public static void AddBoundary(Rectangle newBoundary)
        {
            environment.Add(newBoundary);
        }

        public static bool Inside(Point point)
        {
            foreach(Rectangle boundary in environment)
            {
                if (boundary.Contains(point))
                    return true;
            }
            return false;
        }
        
    }
}
