namespace Desktop_Mascot
{
	partial class Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.Timer = new System.Windows.Forms.Timer(this.components);
			this.Mascot = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.Mascot)).BeginInit();
			this.SuspendLayout();
			// 
			// Timer
			// 
			this.Timer.Enabled = true;
			this.Timer.Interval = 1;
			this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
			// 
			// Mascot
			// 
			this.Mascot.BackColor = System.Drawing.Color.LimeGreen;
			this.Mascot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Mascot.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Mascot.Image = global::Desktop_Mascot.Properties.Resources.idle;
			this.Mascot.Location = new System.Drawing.Point(344, 220);
			this.Mascot.Name = "Mascot";
			this.Mascot.Size = new System.Drawing.Size(128, 128);
			this.Mascot.TabIndex = 0;
			this.Mascot.TabStop = false;
			this.Mascot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Mascot_MouseDown);
			this.Mascot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Mascot_MouseUp);
			// 
			// Form
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.LimeGreen;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.Mascot);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.TransparencyKey = System.Drawing.Color.LimeGreen;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.Mascot)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox Mascot;
		private System.Windows.Forms.Timer Timer;
	}
}

