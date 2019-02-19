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
		MascotPhysics physics;
		XmlMascotReader xmlReader;
		MascotTerrainCollision terrain;
        MascotMovementLogic mascotLogic;

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
			physics = new MascotPhysics(xmlReader);
			anim = new Animator(xmlReader);
			terrain = new MascotTerrainCollision(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, mascotWidth, mascotHeight);
            mascotLogic = new MascotMovementLogic(physics, mascotWidth, Screen.PrimaryScreen.Bounds.Width, ref mascotGraphic);

            this.mascotGraphic.ContextMenuStrip = this.mascotContextMenu;
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
				// Check if in boundary, update position if not.
				terrain.KeepMascotInsideBoundary(ref mascotPosX, ref mascotPosY, Screen.PrimaryScreen.WorkingArea.Width, 
					Screen.PrimaryScreen.WorkingArea.Height, ref mascotGraphic);

				if (!terrain.InBoundary)
				{
					cursorSpeedX = 0;
					cursorSpeedY = 0;					
				}

				if (!terrain.OnGround)
				{
					physics.AppyForces(ref mascotPosX, ref mascotPosY, ref cursorSpeedX, ref cursorSpeedY, ref mascotGraphic);
				}
			}

            // Set whether physics enabled.
            physics.physicsEnabled = toggleMascotPhysics;

            // Update logic (AI).
            mascotLogic.canWander = toggleMascotWandering;
            mascotLogic.UpdateLogic(terrain.OnGround, ref mascotPosX, ref mascotPosY);

            // Update animator.
            anim.physicsEnabled = toggleMascotPhysics;
            anim.wanderingEnabled = toggleMascotWandering;
            anim.UpdateMascotAnim(terrain.OnGround, beingDragged, physics.mascotForceX, physics.mascotForceY, cursorSpeedX, cursorSpeedY, ref mascotGraphic);        
        }
           

		    
	   	

		// Cursor movement tracking.
		bool calculateNewSpeedX = true, calculateNewSpeedY = true;
		int cursorSpeedTimerX,  cursorSpeedTimerY;
		int initialCursorPosX, initialCursorPosY;
		int lastCursorPosX,	lastCursorPosY;
		int curCursorPosX, curCursorPosY;
		private void TrackCursorMovement()
		{
			curCursorPosX = Cursor.Position.X;
			curCursorPosY = Cursor.Position.Y;
			cursorSpeedTimerX++;
			cursorSpeedTimerY++;
			TrackCursorMovementExtension(ref calculateNewSpeedX, ref initialCursorPosX, ref lastCursorPosX, ref curCursorPosX, ref cursorSpeedTimerX);
			TrackCursorMovementExtension(ref calculateNewSpeedY, ref initialCursorPosY, ref lastCursorPosY, ref curCursorPosY, ref cursorSpeedTimerY);
            CalculateCursorSpeed();
        }
        int t = 0;
		private void TrackCursorMovementExtension(ref bool calculateNewSpeed, ref int initialCursorPos, ref int lastCursorPos, ref int curCursorPos, ref int cursorSpeedTimer)
		{
			if (calculateNewSpeed)
			{
				calculateNewSpeed = false;
				initialCursorPos = curCursorPos;
				cursorSpeedTimer = 1;
			}
			else
			{
				if (GetDistance(curCursorPos, initialCursorPos) > GetDistance(lastCursorPos, initialCursorPos))
				{
                    lastCursorPos = curCursorPos;
				}
				else
				{
					
                    if (t > 2)
                    {
                        calculateNewSpeed = true;
                        t = 0;
                    }
                    else
                    {
                        t++;
                    }
                }
			}
		}
               
        int cursorSpeedX, cursorSpeedY;
        private void CalculateCursorSpeed()
		{		          
			cursorSpeedX = (int)(((curCursorPosX - initialCursorPosX) / cursorSpeedTimerX) / xmlReader.MascotDecelerationX);
			cursorSpeedY = (int)(((curCursorPosY - initialCursorPosY) / cursorSpeedTimerY) / xmlReader.MascotDecelerationY);
		}

		int GetDistance(int Pos1, int Pos2)
		{
            
			return Math.Abs(Pos1 - Pos2);
		}			
		
		private void MascotGraphic_MouseDown(object sender, MouseEventArgs e)
		{
			beingDragged = true;
		}		
		
		private void MascotGraphic_MouseUp(object sender, MouseEventArgs e)
		{
			beingDragged = false;
			CalculateCursorSpeed();
		}





        bool toggleMascotWandering = true;
        private void NoWonderingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toggleMascotWandering)
            {
                toggleMascotWandering = false;
                noWonderingToolStripMenuItem.Text = "Allow wandering";
            }
            else
            {
                toggleMascotWandering = true;
                noWonderingToolStripMenuItem.Text = "Stop wandering";
            }
        }

        bool toggleMascotPhysics = true;
        private void disablePhysicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toggleMascotPhysics)
            {
                toggleMascotPhysics = false;
                disablePhysicsToolStripMenuItem.Text = "Enable mascot physics";
            }
            else
            {
                toggleMascotPhysics = true;
                disablePhysicsToolStripMenuItem.Text = "Disable mascot physics";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}