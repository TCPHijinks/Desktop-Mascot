using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Mascot
{
	class MascotPhysics
	{      
        readonly int gravity;		
		readonly int maxForceX, maxForceY;
		public int mascotForceX, mascotForceY;
        public bool physicsEnabled = true;

		public MascotPhysics(XmlMascotReader xmlReader)
		{
			// Apply default physics settings.
			gravity = xmlReader.MascotGravity;
			maxForceX = xmlReader.MascotMaxForceX;
			maxForceY = xmlReader.MascotMaxForceY;
		}
		

		public void AppyForces(ref int mascotPosX, ref int mascotPosY, ref int cursorSpeedX, ref int cursorSpeedY, ref PictureBox graphic)
		{
			if(physicsEnabled)
            {
                // Set move forces.
                cursorSpeedY += gravity;
                mascotForceY = cursorSpeedY;
                mascotForceX = cursorSpeedX;

                // Restrict max x-axis speed.
                if (mascotForceX > maxForceX)
                {
                    mascotForceX = maxForceX;
                }
                else if (mascotForceX < (maxForceX * -1))
                {
                    mascotForceX = maxForceX * -1;
                }
                if (mascotForceY > maxForceY)
                {
                    mascotForceY = maxForceY;
                }
                else if (mascotForceY < (maxForceY * -1))
                {
                    mascotForceY = maxForceY * -1;
                }

                graphic.Location = new Point(mascotPosX + mascotForceX, mascotPosY + mascotForceY);
            }				
		}

        public void MoveMascot(int moveSpeedX, int moveSpeedY, ref int mascotPosX, ref int mascotPosY, ref PictureBox graphic)
        {
            AppyForces(ref mascotPosX, ref mascotPosY, ref moveSpeedX, ref moveSpeedY, ref graphic);
        }
	}
}
