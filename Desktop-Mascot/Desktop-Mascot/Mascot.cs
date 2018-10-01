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
		private int cursorPosY = 0;

		public Mascot()
		{
			InitializeComponent();
			Dock = DockStyle.Fill; // Set size equal to container.

			// Make mascot draggable. 
			ControlExtension.Draggable(mascotGraphic, true);	

			// Load default mascot xml settings.
			GetXmlSettings();
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
				if (velocityX > maxVelocityX)
				{
					velocityX = maxVelocityX;
				}
				else if (velocityX < -maxVelocityX)
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
			if (mascotGraphic.Location.Y + mascotGraphic.Size.Height >= Height)
			{
				// Stop falling when on floor.
				falling = false;
				UpdateFrame("idle");
				mascotGraphic.Region = MakeNonTransparentRegion((Bitmap)mascotGraphic.Image);
			}
			else if (falling)
			{
				// Fall if not touching floor.				
				mascotGraphic.Location = new Point(mascotGraphic.Location.X + (moveForceX + velocityX),
					mascotGraphic.Location.Y + (moveForceY + velocityY + gravity));

				UpdateFrame("fall");
				mascotGraphic.Region = MakeNonTransparentRegion((Bitmap)mascotGraphic.Image);
			}

			// Prevent mascot leaving screen window.
			if (mascotGraphic.Location.X + mascotGraphic.Size.Width >= Width)
			{
				moveForceX = 0;
				velocityX = 0;

			}
			else if (mascotGraphic.Location.X <= 0)
			{
				moveForceX = 0;
				velocityX = 0;

			}
			if (mascotGraphic.Location.Y <= 0)
			{
				velocityY = 0;
			}
		}

		private void mascotGraphic_MouseDown(object sender, MouseEventArgs e)
		{
			falling = false;
		}

		private void mascotGraphic_MouseUp(object sender, MouseEventArgs e)
		{
			falling = true;

			// Velocity of thrown mascot.
			velocityX = ((cursorPosX - Cursor.Position.X) / decelerationX) * -1;
			velocityY = ((cursorPosY - Cursor.Position.Y) / decelerationY) * -1;
		}

		/// <summary>
		/// Update mascot image frame.
		/// </summary>
		/// <param name="actionName"></param>
		string oldAction = null;
		private void UpdateFrame(string actionName)
		{
			if(oldAction == null)
			{
				oldAction = actionName;
			}

			if(actionName != oldAction)
			{
				oldAction = actionName;
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
				mascotGraphic.Image = new Bitmap(imgPath);
				mascotGraphic.Region = MakeNonTransparentRegion((Bitmap)mascotGraphic.Image);
			}
		}

		// Make a region representing the
		// image's non-transparent pixels.
		// Code snippet from: https://goo.gl/ZmtDDx
		public static Region MakeNonTransparentRegion(Bitmap bm)
		{
			if (bm == null) return null;

			// Make the result region.
			Region result = new Region();
			result.MakeEmpty();

			Rectangle rect = new Rectangle(0, 0, 1, 1);
			bool in_image = false;
			for (int y = 0; y < bm.Height; y++)
			{
				for (int x = 0; x < bm.Width; x++)
				{
					if (!in_image)
					{
						// We're not now in the non-transparent pixels.
						if (bm.GetPixel(x, y).A != 0)
						{
							// We just started into non-transparent pixels.
							// Start a Rectangle to represent them.
							in_image = true;
							rect.X = x;
							rect.Y = y;
							rect.Height = 1;
						}
					}
					else if (bm.GetPixel(x, y).A == 0)
					{
						// We are in the non-transparent pixels and
						// have found a transparent one.
						// Add the rectangle so far to the region.
						in_image = false;
						rect.Width = (x - rect.X);
						result.Union(rect);
					}
				}

				// Add the final piece of the rectangle if necessary.
				if (in_image)
				{
					in_image = false;
					rect.Width = (bm.Width - rect.X);
					result.Union(rect);
				}
			}
			return result;
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

