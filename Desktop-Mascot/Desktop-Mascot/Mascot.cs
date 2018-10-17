using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Desktop_Mascot
{
	public partial class Mascot : UserControl
	{
		Animator anim;
		XmlMascotReader xmlReader;
		MascotTerrainCollision terrain;

		bool beingDragged = false;

		readonly int mascotHeight;
		readonly int mascotWidth;		

		// Mascot location.
		int mascotPosX = 0;
		int mascotPosY = 0;
		
		/// <summary>
		/// Mascot character constructor.
		/// </summary>
		public Mascot()
		{
			InitializeComponent();		
			Dock = DockStyle.Fill; // Set size equal to container
			
			// Make mascot draggable. 
			ControlExtension.Draggable(mascotGraphic, true);

			// Set mascot constants.
			mascotHeight = mascotGraphic.Size.Height;
			mascotWidth = mascotGraphic.Size.Width;
			
			// Load mascot settings.
			xmlReader = new XmlMascotReader("default");
			anim = new Animator(xmlReader);
			terrain = new MascotTerrainCollision(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height + 30, mascotWidth, mascotHeight);

			// Apply default physics settings.
			gravity = xmlReader.MascotGravity;
			maxForceX = xmlReader.MascotMaxForceX;
			maxForceY = xmlReader.MascotMaxForceY;
			decelerationX = xmlReader.MascotDecelerationX;
			decelerationY = xmlReader.MascotDecelerationY;
					
		 
			//mascotGraphic.Region = anim.MakeNonTransparentRegion((Bitmap) mascotGraphic.Image);
		}
				
		/// <summary>
		/// Mascot update timer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Timer_Tick(object sender, EventArgs e)
		{			
			// Get current position.
			mascotPosX = mascotGraphic.Location.X;
			mascotPosY = mascotGraphic.Location.Y;
			
			if (beingDragged)
			{
				TrackCursorMovement();
			}	
			else
			{
				// Check if in boundary and set position.
				terrain.CheckBoundary(mascotPosX, mascotPosY);

				if (!terrain.InBoundary)
				{
					cursorSpeedX = 0;
					cursorSpeedY = 0;

					mascotPosX = terrain.NewMascotX;
					mascotPosY = terrain.NewMascotY;	

					mascotGraphic.Location = new Point(terrain.NewMascotX, terrain.NewMascotY);
				}

				if (!terrain.OnGround(mascotPosX, mascotPosY))
				{
					AppyForces();
				}
			}
			
			// Temporary anim state manager. 
			if(terrain.OnGround(mascotPosX,mascotPosY))
			{
				mascotGraphic.Image = anim.UpdateFrame("idle");
			}
			else
			{
				mascotGraphic.Image = anim.UpdateFrame("fall");
			}
		}
		

		// Cursor movement tracking.
		bool calculateNewSpeedX = true;
		bool calculateNewSpeedY = true;
		int cursorSpeedTimerX;
		int cursorSpeedTimerY;
		int initialCursorPosX;
		int initialCursorPosY;
		int lastCursorPosX;
		int lastCursorPosY;
		int curCursorPosX;
		int curCursorPosY;
		private void TrackCursorMovement()
		{			
			curCursorPosX = Cursor.Position.X;
			curCursorPosY = Cursor.Position.Y;						
			cursorSpeedTimerX++;
			cursorSpeedTimerY++;

			#region Cursor x-axis.
			// Restart cursor movement tracking.
			if (calculateNewSpeedX)
			{				
				calculateNewSpeedX = false;
				initialCursorPosX = curCursorPosX;
				lastCursorPosX = curCursorPosX - 1;
				cursorSpeedTimerX = 1;				
			}	
			else
			{
				// Check if moving in same direction and
				// reset if change direction or stop.				
				if (GetDistance(curCursorPosX, initialCursorPosX) > GetDistance(lastCursorPosX, initialCursorPosX))
				{
					lastCursorPosX = curCursorPosX;
				}
				else
				{
					calculateNewSpeedX = true;
				}
			}
			#endregion

			#region Cursor y-axis.
			if (calculateNewSpeedY)
			{
				calculateNewSpeedY = false;
				initialCursorPosY = curCursorPosY;
				lastCursorPosY = curCursorPosY - 1;
				cursorSpeedTimerY = 1;				
			}
			else
			{
				if (GetDistance(curCursorPosY, initialCursorPosY) > GetDistance(lastCursorPosY, initialCursorPosY))
				{
					lastCursorPosY = curCursorPosY;
				}
				else
				{
					calculateNewSpeedY = true;
				}
			}
			#endregion
		}

		int GetDistance(int Pos1, int Pos2)
		{
			return Math.Abs(Pos1 - Pos2);
		}

		private void CalculateCursorSpeed()
		{			
			cursorSpeedX = (curCursorPosX - initialCursorPosX) / cursorSpeedTimerX;
			cursorSpeedY = (curCursorPosY - initialCursorPosY) / cursorSpeedTimerY;
		}
		

		#region Physics and Force.
		// Physics variables.
		int gravity;						// Force of gravity on mascot.
		int decelerationX, decelerationY;   // Force deceleration.
		int maxForceX, maxForceY;			// Max amount of added force.
		int mascotForceX, mascotForceY;		// Mascot physics forces.
		int cursorSpeedX, cursorSpeedY;     // Speed of cursor movement.		

	
		private void AppyForces()
		{
			// Reset force.
			mascotForceX = 0;
			mascotForceY = 0;

			// Calculate move forces.
			cursorSpeedY += gravity;
			mascotForceY = (cursorSpeedY / decelerationY);
			mascotForceX = (cursorSpeedX / decelerationY);		

			// Restrict max x-axis speed.
			if (mascotForceX > maxForceX)
			{
				mascotForceX = maxForceX;
			}
			else if(mascotForceX < (maxForceX * -1))
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

			// Update mascot position on screen.
			mascotGraphic.Location = new Point(mascotPosX + mascotForceX, mascotPosY + mascotForceY);
		}
		#endregion
				
		
		private void MascotGraphic_MouseDown(object sender, MouseEventArgs e)
		{
			beingDragged = true;
		}		
		
		private void MascotGraphic_MouseUp(object sender, MouseEventArgs e)
		{
			beingDragged = false;
			CalculateCursorSpeed();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}		
	}
}