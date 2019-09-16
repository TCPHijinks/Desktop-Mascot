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
        public List<Rectangle> Environment;

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

            // Default boundary.
            Environment = new List<Rectangle>();
            AddBoundary(new Rectangle(0, -100, Screen.FromControl(this).Bounds.Width, 100));// Screen Top
            AddBoundary(new Rectangle(0, Screen.FromControl(this).Bounds.Height, Screen.FromControl(this).Bounds.Width, 100));// Screen Bottom.
            AddBoundary(new Rectangle(-100, 0, 100, Screen.FromControl(this).Bounds.Height));// Screen Left.
            AddBoundary(new Rectangle(Screen.FromControl(this).Bounds.Width, 0, 100, Screen.FromControl(this).Bounds.Height));// Screen Right.

            // Player char and animator component.
            actor = new GameObject(this, Environment);
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
                       
            Invalidate(); // Force control to be redrawn.
        }


        public void AddBoundary(Rectangle newBoundary)
        {
            Environment.Add(newBoundary);
          
        }

   

    }



}
