using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Mascot
{
	class MascotTerrainCollision
	{
		int remainingScreenHeight;
		int remainingScreenWidth;
		readonly int screenMaxHeight;
		readonly int screenMaxWidth;
		readonly int mascotHeight;
		readonly int mascotWidth;		
		public bool InBoundary { get; private set; }
		public bool OnGround { get; private set; }



		/// <summary>
		/// Setup terrain base-parameters. 
		/// </summary>
		/// <param name="screenBoundsWidth"></param>
		/// <param name="screenBoundsHeight"></param>
		/// <param name="mascotWidth"></param>
		/// <param name="mascotHeight"></param>
		public MascotTerrainCollision(int screenBoundsWidth, int screenBoundsHeight, int mascotWidth, int mascotHeight)
		{	
			screenMaxHeight = screenBoundsHeight;
			screenMaxWidth = screenBoundsWidth;							

			this.mascotWidth = mascotWidth;
			this.mascotHeight = mascotHeight;			
		}
		


		/// <summary>
		/// Moves mascot back inside boundary if leaves and manages dynamic collision with taskbar.
		/// </summary>
		/// <param name="mascotX"></param>
		/// <param name="mascotY"></param>
		/// <param name="screenWorkAreaWidth"></param>
		/// <param name="screenWorkAreaHeight"></param>
		/// <param name="graphic"></param>
		public void KeepMascotInsideBoundary(ref int mascotX, ref int mascotY, int screenWorkAreaWidth, int screenWorkAreaHeight, ref PictureBox graphic)
		{		
			InBoundary = true;
			OnGround = false;

			remainingScreenWidth = screenWorkAreaWidth;
			remainingScreenHeight = screenWorkAreaHeight;
			
			// Right-side.
			if ((mascotX + mascotWidth) > remainingScreenWidth)
			{
				mascotX = (remainingScreenWidth - mascotWidth);
				InBoundary = false;
			}
			// Left-side.
			else if (mascotX < 0)
			{
				mascotX = 0;
				InBoundary = false;
			}
			// Bottom-side.
			if ((mascotY + mascotHeight) >= screenMaxHeight && 
				!CheckTaskbarPos(TaskBarPos.BOTTOM))
			{
				mascotY = (screenMaxHeight - mascotHeight);
				InBoundary = false;
				OnGround = true;
			}

			// Taskbar collision.
			int taskbarWidth;
			if (CheckTaskbarPos(TaskBarPos.TOP) || CheckTaskbarPos(TaskBarPos.BOTTOM))
			{
				taskbarWidth = (screenMaxHeight - screenWorkAreaHeight);
								
				if (mascotY < taskbarWidth && CheckTaskbarPos(TaskBarPos.TOP))
				{
					mascotY = taskbarWidth;
					InBoundary = false;
				}
				else if ((mascotY + mascotHeight) >= (remainingScreenHeight) && CheckTaskbarPos(TaskBarPos.BOTTOM))
				{
					mascotY = ((remainingScreenHeight - mascotHeight));
					InBoundary = false;
					OnGround = true;
				}
			}
			else
			{
				taskbarWidth = (screenMaxWidth - screenWorkAreaWidth);
				if (CheckTaskbarPos(TaskBarPos.LEFT) && mascotX < taskbarWidth)
				{
					mascotX = taskbarWidth;
					InBoundary = false;
				}
				else if (CheckTaskbarPos(TaskBarPos.RIGHT) && (mascotX + mascotWidth) > (screenMaxWidth - taskbarWidth))
				{
					mascotX = (remainingScreenWidth - mascotWidth);
					InBoundary = false;
				}		
			}
		

			// Update mascot position.
			graphic.Location = new Point(mascotX, mascotY);
		}

			  
		//private void 

		
		
		private enum TaskBarPos { TOP, BOTTOM, LEFT, RIGHT }
		private bool CheckTaskbarPos(TaskBarPos checkIf)
		{
			TaskBarPos taskBarLocation = TaskBarPos.BOTTOM;
			bool taskBarOnTopOrBottom = (Screen.PrimaryScreen.WorkingArea.Width == Screen.PrimaryScreen.Bounds.Width);
			if (taskBarOnTopOrBottom)
			{
				if (Screen.PrimaryScreen.WorkingArea.Top > 0) taskBarLocation = TaskBarPos.TOP;
			}
			else
			{
				if (Screen.PrimaryScreen.WorkingArea.Left > 0)
				{
					taskBarLocation = TaskBarPos.LEFT;
				}
				else
				{
					taskBarLocation = TaskBarPos.RIGHT;
				}
			}
		
			if(taskBarLocation.Equals(checkIf))
			{
				return true;
			}
			return false;
		}

		
	}
}
