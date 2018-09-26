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
			this.label1 = new System.Windows.Forms.Label();
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
			this.Mascot.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Mascot.Image = global::Desktop_Mascot.Properties.Resources.mascot1;
			this.Mascot.Location = new System.Drawing.Point(344, 220);
			this.Mascot.Name = "Mascot";
			this.Mascot.Size = new System.Drawing.Size(128, 128);
			this.Mascot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.Mascot.TabIndex = 0;
			this.Mascot.TabStop = false;
			this.Mascot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Mascot_MouseDown);
			this.Mascot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Mascot_MouseUp);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(134, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 42);
			this.label1.TabIndex = 2;
			this.label1.Text = "label1";
			// 
			// Form
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Mascot);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form";
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.Mascot)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox Mascot;
		private System.Windows.Forms.Timer Timer;
		private System.Windows.Forms.Label label1;
	}
}

