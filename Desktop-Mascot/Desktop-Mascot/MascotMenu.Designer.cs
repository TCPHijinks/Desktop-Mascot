namespace Desktop_Mascot
{
	partial class MascotMenu
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.MascotOptions = new System.Windows.Forms.ComboBox();
			this.LoadMascot = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Gainsboro;
			this.panel1.Controls.Add(this.MascotOptions);
			this.panel1.Controls.Add(this.LoadMascot);
			this.panel1.Location = new System.Drawing.Point(-6, -4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(168, 168);
			this.panel1.TabIndex = 3;
			// 
			// MascotOptions
			// 
			this.MascotOptions.BackColor = System.Drawing.SystemColors.Control;
			this.MascotOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.MascotOptions.FormattingEnabled = true;
			this.MascotOptions.Location = new System.Drawing.Point(6, 24);
			this.MascotOptions.Name = "MascotOptions";
			this.MascotOptions.Size = new System.Drawing.Size(150, 21);
			this.MascotOptions.TabIndex = 5;
			// 
			// LoadMascot
			// 
			this.LoadMascot.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.LoadMascot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LoadMascot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LoadMascot.Location = new System.Drawing.Point(3, 0);
			this.LoadMascot.Name = "LoadMascot";
			this.LoadMascot.Size = new System.Drawing.Size(162, 27);
			this.LoadMascot.TabIndex = 4;
			this.LoadMascot.Text = "Load New Mascot";
			this.LoadMascot.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.LoadMascot.UseVisualStyleBackColor = true;
			// 
			// MascotMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "MascotMenu";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox MascotOptions;
		private System.Windows.Forms.Button LoadMascot;
	}
}
