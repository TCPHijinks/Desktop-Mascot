using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Desktop_Mascot
{
	class Animator
	{		
		string imgFrame;
		XmlMascotReader mascotXml;
		public Animator(XmlMascotReader xmlReader)
		{
			mascotXml = xmlReader;
		}

		/// <summary>
		/// Update mascot image frame.
		/// </summary>
		/// <param name="actionName"></param>
		string oldAction = null;
		public Bitmap UpdateFrame(string actionType)
		{
			if (actionType != oldAction || oldAction == null)
			{
				oldAction = actionType;

				// Get animation frame image.				
				string nodePath = "//Mascot[@name='"+mascotXml.MascotName+"']//Action[@type='"+actionType+"']//Frame";
				imgFrame = mascotXml.GetSingleNode(nodePath, "image");
			}
			
			return new Bitmap(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), @"Data\img\Mascot" + imgFrame));
		}

		/// <summary>
		/// Make a region representing the
		/// image's non-transparent pixels.
		/// </summary>
		/// <param name="bm"></param>
		/// <returns></returns>
		// Code snippet from: https://goo.gl/ZmtDDx
		public Region MakeNonTransparentRegion(Bitmap bm)
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
	}
}
