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
    public partial class MainForm : Form
    {
        private GameObject actor;
        private readonly Animator Animator;
    

        public MainForm()
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

            // Player char and animator component.
            actor = new GameObject(this);
            Animator = new Animator(actor);

         
        }

     
        protected override void OnPaint(PaintEventArgs eventArgs)
        {
            base.OnPaint(eventArgs);

            // Render actor.
            var gfx = eventArgs.Graphics;
            Animator.RenderActorFrame(gfx);
            gfx.ResetTransform();

            // Update movement position relative to fps.
            Animator.UpdatePositions(Animator.CalculateFPS());
          


          

            //environment.InsideTerrain(Animator.gameObject.Position.X, Animator.gameObject.Position.Y);



            // gfx.Clear(Color.Black);


            Invalidate(); // Force control to be redrawn.
        }

      
    }



}
