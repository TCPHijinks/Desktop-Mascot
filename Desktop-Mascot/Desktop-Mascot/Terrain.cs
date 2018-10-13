using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Mascot
{
	class Terrain
	{
		readonly int screenWidth;
		readonly int screenHeight;

		readonly int mascotHeight;
		readonly int mascotWidth;

		public int MascotX { get; set; }
		public int MascotY { get; set; }

		public bool InBoundary { get; private set; }

		public Terrain(int screenWidth, int screenHeight, int mascotWidth, int mascotHeight)
		{
			this.screenWidth = screenWidth;
			this.screenHeight = screenHeight;
			this.mascotWidth = mascotWidth;
			this.mascotHeight = mascotHeight;			
		}

		public bool OnGround(int mascotX, int mascotY)
		{
			// Check if on ground.
			if ((mascotY + mascotHeight) == screenHeight)
			{
				return true;
			}
			return false;
		}

		public void CheckBoundary(int mascotX, int mascotY)
		{
			//bool insideBoundary = true;
			InBoundary = true;
			// Set mascot current position.
			MascotX = mascotX;
			MascotY = mascotY;

			if ((MascotX + mascotWidth) > screenWidth)
			{
				MascotX = (screenWidth - mascotWidth);
				InBoundary = false;
			}
			else if (MascotX < 0)
			{
				MascotX = 0;
				InBoundary = false;
			}
			if ((MascotY + mascotHeight) > screenHeight)
			{
				MascotY = (screenHeight - mascotHeight);
				InBoundary = false;
			}
		}

		public string showAllInfo()
		{
			//string i = "Mascot WxH: " + mascotWidth + "x" + mascotHeight + " -- Mascot XY: " + MascotX + "x" + MascotY + " -- Screen WxH: " + screenWidth + "x" + screenHeight;
			return "g";
		}
	}
}
