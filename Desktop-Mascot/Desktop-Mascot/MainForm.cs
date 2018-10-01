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
		public Form()
		{
			InitializeComponent();

			// Set window transparent.
			BackColor = Color.LimeGreen;
			TransparencyKey = Color.LimeGreen;
			FormBorderStyle = FormBorderStyle.None;

			TopMost = true;
			ShowInTaskbar = false;


			Mascot mascot = new Mascot();
			this.Controls.Add(mascot);
		}
	}
}
