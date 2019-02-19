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
            this.mascotGraphic = new System.Windows.Forms.PictureBox();
            this.mascotContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noWonderingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disablePhysicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mascotGraphic)).BeginInit();
            this.mascotContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 1;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
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
            // mascotContextMenu
            // 
            this.mascotContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.mascotContextMenu.Name = "mascotContextMenu";
            this.mascotContextMenu.Size = new System.Drawing.Size(115, 48);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noWonderingToolStripMenuItem,
            this.disablePhysicsToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.testToolStripMenuItem.Text = "Actions";
            // 
            // noWonderingToolStripMenuItem
            // 
            this.noWonderingToolStripMenuItem.Name = "noWonderingToolStripMenuItem";
            this.noWonderingToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.noWonderingToolStripMenuItem.Text = "Stop Wandering";
            this.noWonderingToolStripMenuItem.Click += new System.EventHandler(this.NoWonderingToolStripMenuItem_Click);
            // 
            // disablePhysicsToolStripMenuItem
            // 
            this.disablePhysicsToolStripMenuItem.Name = "disablePhysicsToolStripMenuItem";
            this.disablePhysicsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.disablePhysicsToolStripMenuItem.Text = "Disable Physics";
            this.disablePhysicsToolStripMenuItem.Click += new System.EventHandler(this.disablePhysicsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Mascot
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.mascotGraphic);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.Name = "Mascot";
            this.Size = new System.Drawing.Size(129, 129);
            ((System.ComponentModel.ISupportInitialize)(this.mascotGraphic)).EndInit();
            this.mascotContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.PictureBox mascotGraphic;
		private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.ContextMenuStrip mascotContextMenu;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noWonderingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disablePhysicsToolStripMenuItem;
    }
}
