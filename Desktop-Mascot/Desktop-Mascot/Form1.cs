using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Desktop_Mascot
{
	public partial class Form : System.Windows.Forms.Form
	{
		private bool falling = true; // Try remove
		
		// Mascot movement force.
		private int moveForceX = 0;
		private int moveForceY = 0;

		// Mascot velocity.
		private int velocityX = 0;
		private int velocityY = 0;

		// Amount initial velocity is affected by.
		int decelerationX = 0;
		int decelerationY = 0;

		// Rate at which velocity is decreased.
		int decelerationRateX = 0;
		int decelerationRateY = 0;

		// Maximum mascot velocity.
		int maxVelocityX = 0;
		int maxVelocityY = 0;

		private int gravity = 0;

		// Position of mouse cursor.
		private int cursorPosX = 0;
		private int	cursorPosY = 0;
		
		

		public Form()
		{			
			InitializeComponent();

			// Set window transparent.
			BackColor = Color.LimeGreen;
			TransparencyKey = Color.LimeGreen;
			FormBorderStyle = FormBorderStyle.None;

			// Make mascot draggable. 
			ControlExtension.Draggable(Mascot, true);
						
			TopMost = true;
			ShowInTaskbar = false;

			// Load default mascot xml settings.
			GetXmlSettings();
		}

		
		private void Mascot_MouseDown(object sender, MouseEventArgs e)
		{			
			falling = false;
		}

		
		private void Mascot_MouseUp(object sender, MouseEventArgs e)
		{
			falling = true;
			
			// Velocity of thrown mascot.
			velocityX = ((cursorPosX - Cursor.Position.X) / decelerationX) * -1;
			velocityY = ((cursorPosY - Cursor.Position.Y) / decelerationY) * -1;			
		}
		

		int timePeriod = 0;
		private void Timer_Tick(object sender, EventArgs e)
		{			
			timePeriod++;
			if (timePeriod >= 9)
			{
				// Update cursor position every X milliseconds.
				cursorPosX = Cursor.Position.X;
				cursorPosY = Cursor.Position.Y;

				// Limit velocity of X axis.
				if(velocityX > maxVelocityX)
				{
					velocityX = maxVelocityX;
				}
				else if(velocityX < -maxVelocityX)
				{
					velocityX = -maxVelocityX;
				}

				// Limit velocity of Y axis.
				if (velocityY > maxVelocityY)
				{
					velocityY = maxVelocityY;
				}
				else if (velocityY < -maxVelocityY)
				{
					velocityY = -maxVelocityY;
				}

				// Apply gravity to Y axis.
				if (velocityY < 0)
				{
					velocityY += decelerationRateY;
				}

				// Decelerate x axis.
				if (velocityX > 0)
				{
					velocityX -= decelerationRateX;
				}
				else if (velocityX < 0)
				{
					velocityX += decelerationRateX;
				}
				
				// Reset update timer.
				timePeriod = 0;
			}
			

			// Falling movement and animation.
			if (Mascot.Location.Y + Mascot.Size.Height >= Height)
			{
				// Stop falling when on floor.
				falling = false;
				UpdateFrame("idle");
			}
			else if (falling)
			{
				// Fall if not touching floor.				
				Mascot.Location = new Point(Mascot.Location.X + (moveForceX + velocityX), 
					Mascot.Location.Y + (moveForceY + velocityY + gravity));
				UpdateFrame("fall");
			}

			// Prevent mascot leaving screen window.
			if (Mascot.Location.X + Mascot.Size.Width >= Width)
			{
				moveForceX = 0;
				velocityX = 0;
				
			}
			else if(Mascot.Location.X <= 0)
			{
				moveForceX = 0;
				velocityX = 0;
			
			}
			if(Mascot.Location.Y <= 0)
			{
				velocityY = 0;
			}
		}

		
		/// <summary>
		/// Update mascot image frame.
		/// </summary>
		/// <param name="actionName"></param>
		private void UpdateFrame(string actionName)
		{
			string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\conf\\actions.xml";

			// Open actions xml file.
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);

			// Open node of given "action name". 
			XmlNode node = doc.DocumentElement.SelectSingleNode("/Mascot/ActionArray/Action[@type='" + actionName + "']/Animation/Frame");

			// Get name of image/frame from node for image path.
			string imgFrame = node.Attributes["image"].Value;
			string imgPath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), @"Data\img\Mascot" + imgFrame);

			string velocityStr = node.Attributes["velocity"].Value;

			// Update action velocity.
			Int32.TryParse(velocityStr.Substring(0, velocityStr.IndexOf(",")), out moveForceX);
			Int32.TryParse(velocityStr.Substring(velocityStr.IndexOf(",") + 1), out moveForceY);

			// Update mascot frame & remove semi-transparent pixels.
			Mascot.Image = new Bitmap(imgPath); //  UnSemi(new Bitmap(imgPath));
			
		}

		private void GetXmlSettings()
		{
			string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\conf\\actions.xml";

			// Open actions xml file.
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);

			// Get gravity from physics node.
			XmlNode node = doc.DocumentElement.SelectSingleNode("/Mascot/Environment/Physics");
			gravity = Int32.Parse(node.Attributes["gravity"].Value);

			// Get x axis physics values.			
			node = doc.DocumentElement.SelectSingleNode("/Mascot/Environment/Physics/xAxis");
			decelerationX = Int32.Parse(node.Attributes["deceleration"].Value);
			decelerationRateX = Int32.Parse(node.Attributes["rateOfDeceleration"].Value);
			maxVelocityX = Int32.Parse(node.Attributes["maxVelocity"].Value);

			
			node = doc.DocumentElement.SelectSingleNode("/Mascot/Environment/Physics/yAxis");
			decelerationY = Int32.Parse(node.Attributes["deceleration"].Value);
			decelerationRateY = Int32.Parse(node.Attributes["rateOfDeceleration"].Value);
			maxVelocityY = Int32.Parse(node.Attributes["maxVelocity"].Value);
		}	
	}
}
