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
			this.Timer = new System.Windows.Forms.Timer(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.mascotGraphic = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.mascotGraphic)).BeginInit();
			this.SuspendLayout();
			// 
			// Timer
			// 
			this.Timer.Enabled = true;
			this.Timer.Interval = 1;
			this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.White;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(85, 27);
			this.button1.TabIndex = 2;
			this.button1.Text = "Close Test";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// mascotGraphic
			// 
			this.mascotGraphic.BackColor = System.Drawing.Color.LimeGreen;
			this.mascotGraphic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.mascotGraphic.Cursor = System.Windows.Forms.Cursors.Hand;
			this.mascotGraphic.Image = global::Desktop_Mascot.Properties.Resources.idle;
			this.mascotGraphic.Location = new System.Drawing.Point(0, 0);
			this.mascotGraphic.Name = "mascotGraphic";
			this.mascotGraphic.Size = new System.Drawing.Size(129, 129);
			this.mascotGraphic.TabIndex = 1;
			this.mascotGraphic.TabStop = false;
			this.mascotGraphic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MascotGraphic_MouseDown);
			this.mascotGraphic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MascotGraphic_MouseUp);
			// 
			// Mascot
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.LimeGreen;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.mascotGraphic);
			this.Cursor = System.Windows.Forms.Cursors.AppStarting;
			this.Name = "Mascot";
			this.Size = new System.Drawing.Size(129, 129);
			((System.ComponentModel.ISupportInitialize)(this.mascotGraphic)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.PictureBox mascotGraphic;
		private System.Windows.Forms.Timer Timer;
		private System.Windows.Forms.Button button1;
	}
}
