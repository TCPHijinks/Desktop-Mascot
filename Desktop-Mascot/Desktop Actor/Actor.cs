using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Desktop_Actor
{
    public partial class Actor : Form
    {
        private readonly Animator Animator;
     

        public Actor()
        {
            InitializeComponent();
            
            DoubleBuffered = true;
            
            Focus();
            BringToFront();
            TabStop = false;
            TopMost = true;
            KeyPreview = true;  // Prioritize parent key press over child.

            BackColor = Color.LimeGreen;
            TransparencyKey = Color.LimeGreen;
           
            ShowInTaskbar = false;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            Animator = new Animator(this);
            
        }

        protected override void OnPaint(PaintEventArgs eventArgs)
        {
            base.OnPaint(eventArgs);
            // Render actor.
            var gfx = eventArgs.Graphics;
            Animator.RenderActorFrame(gfx);
            gfx.ResetTransform();

            Animator.Update();
            Animator.UpdatePositions();

            
            
           // gfx.Clear(Color.Black);
            
         
            Invalidate(); // Force control to be redrawn.
        }

      
    }



}
