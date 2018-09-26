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

		private bool falling = true;
		private int velocityX = 0, velocityY = 0;		

		public Form()
		{
			InitializeComponent();

			// Set BG color and set it transparent (bugged).
			BackColor = Color.LightGray;
			TransparencyKey = Color.LightGray;
			
			UnSemi((Bitmap)this.Mascot.Image); // Remove semi-transparent pixels. 

			// Make picture box draggable. 
			ControlExtension.Draggable(Mascot, true);

			// Set window to top layer.
			TopMost = true;
		}

		private void Mascot_MouseDown(object sender, MouseEventArgs e)
		{
			falling = false;
		}

		private void Mascot_MouseUp(object sender, MouseEventArgs e)
		{
			falling = true;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			// Prevent mascot from falling out of bounds.
			if (Mascot.Location.Y + Mascot.Size.Height >= Height)
			{
				falling = false;
				UpdateFrame("idle");
			}
			else if (falling)
			{				
				Mascot.Location = new Point(Mascot.Location.X + velocityX, Mascot.Location.Y + velocityY);
				UpdateFrame("fall");
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
			Int32.TryParse(velocityStr.Substring(0, velocityStr.IndexOf(",")), out velocityX);
			Int32.TryParse(velocityStr.Substring(velocityStr.IndexOf(",") + 1), out velocityY);

			// Update mascot frame & remove semi-transparent pixels.
			Mascot.Image = UnSemi(new Bitmap(imgPath));
		}


		/// <summary>
		/// Remove semi-transparent pixels.
		/// </summary>
		/// <param name="bmp"></param>
		/// <returns>Edited bitmap image.</returns>
		public Bitmap UnSemi(Bitmap bmp)
		{
			Size s = bmp.Size;
			PixelFormat fmt = bmp.PixelFormat;
			Rectangle rect = new Rectangle(Point.Empty, s);
			BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, fmt);
			int size1 = bmpData.Stride * bmpData.Height;
			byte[] data = new byte[size1];
			System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, size1);
			for (int y = 0; y < s.Height; y++)
			{
				for (int x = 0; x < s.Width; x++)
				{
					int index = y * bmpData.Stride + x * 4;
					// alpha,  threshold = 255
					data[index + 3] = (data[index + 3] < 255) ? (byte)0 : (byte)255;
				}
			}
			System.Runtime.InteropServices.Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
			bmp.UnlockBits(bmpData);

			return bmp;
		}		
	}
}
