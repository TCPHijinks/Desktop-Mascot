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
			DoubleBuffered = true;
			Dock = DockStyle.Fill; // Set size equal to container
			
			// Make mascot draggable. 
			ControlExtension.Draggable(mascotGraphic, true);

			// Set mascot constants.
			mascotHeight = mascotGraphic.Size.Height;
			mascotWidth = mascotGraphic.Size.Width;
			
			// Load mascot settings.
			xmlReader = new XmlMascotReader("default");
			anim = new Animator(xmlReader);

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
			// Update cursor position and speed.
			cursorCurPosX = Cursor.Position.X;
			cursorCurPosY = Cursor.Position.Y;

			TrackCursorThrowSpeed();

			if(!beingDragged)
			{
				CheckInBoundary();
				if (!OnGround())
				{
					AppyForces();
				}
			}		
			
			// Temporary anim state manager. 
			if(OnGround())
			{
				mascotGraphic.Image = anim.UpdateFrame("idle");
			}
			else
			{
				mascotGraphic.Image = anim.UpdateFrame("fall");
			}
		}

		// Cursor x and y-axis values.
		bool calculateSpeedX = false, calculateSpeedY = false;	// If calculate speed again.
		bool cursorMovingLeft = false, cursorMovingUp = false;  // If cursor moving left.

		int timerX = 1, timerY = 1;				// X-axis move time (used to determine speed).
		int cursorOldPosX, cursorOldPosY;		// Cursor last position (used to determine if moving).
		int cursorStartPosX, cursorStartPosY;	// Cursor start position (used to determine distance).
		int cursorCurPosX, cursorCurPosY;		// Cursor current position (used to determine distance)				   
		private void TrackCursorThrowSpeed()
		{
			#region Calculate cursor x-axis speed.
			// Start new calculation if not already and moving.
			if (calculateSpeedX && cursorStartPosX != cursorCurPosX)
			{
				cursorMovingLeft = false;	// Whether mascot dragged left.
				calculateSpeedX = false;	// Whether start new calculation of speed.
				if (cursorCurPosX < cursorStartPosX)
				{
					cursorMovingLeft = true;
				}
				// Update old position to check if move.
				cursorOldPosX = cursorStartPosX;	
			}						
			// Flip values of old and new cursor positions if go right,
			//  allowing calculate of difference between the two values.
			int smallerValueX = cursorCurPosX;		
			int largerValueX = cursorOldPosX;
			if (!cursorMovingLeft)			
			{
				smallerValueX = cursorOldPosX;
				largerValueX = cursorCurPosX;
			}
			// If not new calculation and continuing. 
			if (!calculateSpeedX)	
			{
				// Increase time, then update old cursor pos to current if still moving in one direction.
				// Otherwise if not moving or change direction, reset calculations of speed.
				timerX++;
				if (smallerValueX < largerValueX)	// If still moving same dir, update last cursor pos.
				{
					cursorOldPosX = cursorCurPosX;
				}
				else
				{
					calculateSpeedX = true;
					cursorStartPosX = cursorCurPosX;
					timerX = 1;
				}
			}
			#endregion

			#region Calculate cursor y-axis speed.
			if (calculateSpeedY && cursorStartPosY != cursorCurPosY)
			{
				cursorMovingUp = false;
				calculateSpeedY = false;
				if (cursorCurPosY < cursorStartPosY)
				{
					cursorMovingUp = true;
				}
				cursorOldPosY = cursorStartPosY;
			}
			int smallerValueY = cursorCurPosY;
			int largerValueY = cursorOldPosY;
			if (!cursorMovingUp)
			{
				smallerValueY = cursorOldPosY;
				largerValueY = cursorCurPosY;
			}
			if (!calculateSpeedY)
			{
				timerY++;
				if (smallerValueY < largerValueY)
				{
					cursorOldPosY = cursorCurPosY;
				}
				else
				{
					calculateSpeedY = true;
					cursorStartPosY = cursorCurPosY;
					timerY = 1;
				}
			}
			#endregion
		}


		#region Environment checks.
		/// <summary>
		/// Returns whether mascot is on platform.
		/// </summary>
		/// <returns></returns>
		private bool OnGround()
		{
			// Check if on ground.
			if((mascotPosY + mascotHeight) == Height)
			{
				return true;
			}
			return false;
		}	
		
		/// <summary>
		/// Moves mascot inside screen boundary if outside.
		/// </summary>
		private void CheckInBoundary()
		{
			bool insideBoundary = true;

			// Set mascot current position.
			mascotPosX = mascotGraphic.Location.X;
			mascotPosY = mascotGraphic.Location.Y;
			
			if ((mascotPosX + mascotWidth) > Width)
			{
				mascotPosX = (Width - mascotWidth);				
				insideBoundary = false;				
			}
			else if(mascotPosX < 0)
			{				
				mascotPosX = 0;				
				insideBoundary = false;
			}			
			if((mascotPosY + mascotHeight) > Height)
			{				
				mascotPosY = (Height - mascotHeight);				
				insideBoundary = false;				
			}
			else if(mascotPosY < 0)
			{				
				//scotPosY = 0;				
				//sideBoundary = false;				
			}
						
			if(!insideBoundary)
			{
				StopForceMovement();
				mascotGraphic.Location = new Point(mascotPosX, mascotPosY);
			}
		}
		#endregion

		#region Physics and Force.
		// Physics variables.
		int gravity;						// Force of gravity on mascot.
		int decelerationX, decelerationY;   // Force deceleration.

		private void button1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		int maxForceX, maxForceY;			// Max amount of added force.
		int mascotForceX, mascotForceY;		// Mascot physics forces.
		int cursorSpeedX, cursorSpeedY;		// Speed of cursor movement.		

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
		
		/// <summary>
		/// Removes all external forces (except gravity) from Mascot.
		/// </summary>
		private void StopForceMovement()
		{
			mascotForceY = 0;
			mascotForceX = 0;
			cursorSpeedX = 0;
			cursorSpeedY = 0;
		}
		#endregion

		#region Mouse and Button Clicking.
		/// <summary>
		/// Mouse button (M1) was pressed on Mascot.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MascotGraphic_MouseDown(object sender, MouseEventArgs e)
		{
			cursorStartPosX = cursorCurPosX;
			calculateSpeedX = true;
			beingDragged = true;
		}
		
		/// <summary>
		/// Mouse button (M1) was released on Mascot.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MascotGraphic_MouseUp(object sender, MouseEventArgs e)
		{
			// Calculate cursor speed.
			cursorSpeedX = (cursorCurPosX - cursorStartPosX) / timerX;
			cursorSpeedY = (cursorCurPosY - cursorStartPosY) / timerY;
			
			beingDragged = false;		// Set not being dragged by cursor.
			calculateSpeedX = false;	// Stop calculating cursor speed.		
		}
		#endregion
	}
}