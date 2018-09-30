namespace Desktop_Mascot
{
    partial class Mascot
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.mascotGraphic = new System.Windows.Forms.PictureBox();
			this.Timer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.mascotGraphic)).BeginInit();
			this.SuspendLayout();
			// 
			// mascotGraphic
			// 
			this.mascotGraphic.BackColor = System.Drawing.Color.LimeGreen;
			this.mascotGraphic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.mascotGraphic.Cursor = System.Windows.Forms.Cursors.Hand;
			this.mascotGraphic.Image = global::Desktop_Mascot.Properties.Resources.idle;
			this.mascotGraphic.Location = new System.Drawing.Point(0, 0);
			this.mascotGraphic.Name = "mascotGraphic";
			this.mascotGraphic.Size = new System.Drawing.Size(128, 128);
			this.mascotGraphic.TabIndex = 1;
			this.mascotGraphic.TabStop = false;
			this.mascotGraphic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mascotGraphic_MouseDown);
			this.mascotGraphic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mascotGraphic_MouseUp);
			// 
			// Timer
			// 
			this.Timer.Enabled = true;
			this.Timer.Interval = 1;
			this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
			// 
			// Mascot
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.LimeGreen;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.mascotGraphic);
			this.Cursor = System.Windows.Forms.Cursors.AppStarting;
			this.Name = "Mascot";
			this.Size = new System.Drawing.Size(127, 128);
			((System.ComponentModel.ISupportInitialize)(this.mascotGraphic)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.PictureBox mascotGraphic;
		private System.Windows.Forms.Timer Timer;
	}
}
