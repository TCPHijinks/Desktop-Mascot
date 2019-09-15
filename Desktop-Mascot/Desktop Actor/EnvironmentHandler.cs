using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop_Actor
{
    public class EnvironmentHandler
    {
        private int objectHeight, objectWidth;
        private List<Boundary> environment;
        public EnvironmentHandler(int gameObjectHeight, int gameObjectWidth, int screenHeight, int screenWidth, Graphics gfx)
        {
            objectHeight = gameObjectHeight;
            objectWidth = gameObjectWidth;
            environment = new List<Boundary>();

            AddBoundary(new Boundary(0,-5, screenWidth, 5));// Screen Top
            AddBoundary(new Boundary(0, screenHeight, screenWidth, 5));// Screen Bottom.
            AddBoundary(new Boundary(-5, 0, 5, screenHeight));// Screen Left.
            AddBoundary(new Boundary(screenWidth, 0, 5, screenHeight));// Screen Right.
        }

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

        public void AddBoundary(Boundary newBoundary)
        {
            environment.Add(newBoundary);
        }
    }
}
