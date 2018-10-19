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
		readonly int decelerationX, decelerationY;
		readonly int maxForceX, maxForceY;
		int mascotForceX, mascotForceY;

		public MascotPhysics(XmlMascotReader xmlReader)
		{
			// Apply default physics settings.
			gravity = xmlReader.MascotGravity;
			maxForceX = xmlReader.MascotMaxForceX;
			maxForceY = xmlReader.MascotMaxForceY;
			decelerationX = xmlReader.MascotDecelerationX;
			decelerationY = xmlReader.MascotDecelerationY;
		}
		

		public void AppyForces(ref int mascotPosX, ref int mascotPosY, ref int cursorSpeedX, ref int cursorSpeedY, ref PictureBox graphic)
		{
			
			// Reset force.
			mascotForceX = 0;
			mascotForceY = 0;

			// Calculate move forces.
			cursorSpeedY += gravity;
			mascotForceY = (cursorSpeedY / decelerationY);
			mascotForceX = (cursorSpeedX / decelerationX);

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

			graphic.Location = graphic.Location = new Point(mascotPosX + mascotForceX, mascotPosY + mascotForceY);		
		}
	}
}
