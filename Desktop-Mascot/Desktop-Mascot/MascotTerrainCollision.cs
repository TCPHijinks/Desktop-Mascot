using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Mascot
{
	class MascotTerrainCollision
	{
		readonly int screenWidth;
		readonly int screenHeight;

		readonly int mascotHeight;
		readonly int mascotWidth;

		public int NewMascotX { get; private set; }
		public int NewMascotY { get; private set; }

		public bool InBoundary { get; private set; }

		public MascotTerrainCollision(int screenWidth, int screenHeight, int mascotWidth, int mascotHeight)
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
			InBoundary = true;

			// Set mascot current position.
			NewMascotX = mascotX;
			NewMascotY = mascotY;

			if ((NewMascotX + mascotWidth) > screenWidth)
			{
				NewMascotX = (screenWidth - mascotWidth);
				InBoundary = false;
			}
			else if (NewMascotX < 0)
			{
				NewMascotX = 0;
				InBoundary = false;
			}
			if ((NewMascotY + mascotHeight) > screenHeight)
			{
				NewMascotY = (screenHeight - mascotHeight);
				InBoundary = false;
			}
		}
	}
}
