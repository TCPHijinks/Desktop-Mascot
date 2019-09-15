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
        private EnvironmentHandler environment;

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

        bool locked = false;
        protected override void OnPaint(PaintEventArgs eventArgs)
        {
            base.OnPaint(eventArgs);

            // Render actor.
            var gfx = eventArgs.Graphics;
            Animator.RenderActorFrame(gfx);
            gfx.ResetTransform();

            // Update movement position relative to fps.
            Animator.UpdatePositions(Animator.CalculateFPS());
          


            if (!locked)
            {
                environment = new EnvironmentHandler(Animator.gameObject.Dimension.Height, Animator.gameObject.Dimension.Width, Width, Height, gfx);
                locked = true;
            }

            environment.InsideTerrain(Animator.gameObject.Position.X, Animator.gameObject.Position.Y);



            // gfx.Clear(Color.Black);


            Invalidate(); // Force control to be redrawn.
        }

      
    }



}
