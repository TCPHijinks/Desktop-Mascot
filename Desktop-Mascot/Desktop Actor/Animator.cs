using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Desktop_Actor
{
    public class Animator
    {
        private readonly GameObject _actor = new GameObject();
        
        public Animator(Control actorControl)
        {
            // Subscribe to key press events.
            actorControl.KeyDown += Actor_KeyDown;
            actorControl.KeyUp += Actor_KeyUp;
            actorControl.MouseDown += MouseClickActor;
            actorControl.MouseUp += MouseUpActor;

            // Starting position.
            _actor.Dimension.Width = actorControl.Width / 3;
            _actor.Dimension.Height = actorControl.Height / 3;
        }

        public DateTime PrevFrameTime;
        public DateTime FrameTime;
        public double FramesPerSecond;

        public void Update()
        {
            PrevFrameTime = FrameTime;
            FrameTime = DateTime.Now;
            FramesPerSecond = (FrameTime - PrevFrameTime).TotalMilliseconds / 1000;
           
        }

        public void UpdatePositions()
        {
            // Ensure that the motion is moving at a
            var speed = 200; // X units within 1 second
            var moveDistPerSecond = (int)(speed * FramesPerSecond);

            Move(moveDistPerSecond);
        }

        #region Event Methods

        private bool _grabbingActor;

        private void MouseClickActor(object sender, MouseEventArgs e)
        {
            _grabbingActor = true;
            Console.WriteLine("CALL");
        }

        private void MouseUpActor(object sender, MouseEventArgs e)
        {
            _grabbingActor = false;
            Console.WriteLine("RELEASE");
        }

        #endregion Event Methods

        public void Move(int moveDistPerSecond)
        {
            if (_grabbingActor)
            {
                // Position actor so centered with cursor.
                _actor.Position.X = (int)(Cursor.Position.X - (_actor.Dimension.Width / 2));
                _actor.Position.Y = (int)(Cursor.Position.Y - (_actor.Dimension.Height / 2));
            }
            else
            {
                // Save original position.
                Point prevPos = new Point(_actor.Position.X, _actor.Position.Y);

                // Apply movement displacement.
                if (_keyMap[Keys.Up])
                    _actor.Position.Y -= moveDistPerSecond;
                if (_keyMap[Keys.Down])
                    _actor.Position.Y += moveDistPerSecond;
                if (_keyMap[Keys.Right])
                    _actor.Position.X += moveDistPerSecond;
                if (_keyMap[Keys.Left])
                    _actor.Position.X -= moveDistPerSecond;

                // Prevent actor from moving faster diagonally.
                ClampSpeed(prevPos, moveDistPerSecond);
            }
        }

        /// <summary>
        /// Clamps speed but limiting maximum movement each update from exceeding maximum.
        /// </summary>
        /// <param name="prevPosition"></param>
        /// <param name="moveDistPerSecond"></param>
        private void ClampSpeed(Point prevPosition, float moveDistPerSecond)
        {
            // Get distance from positions before and after movement during this update.
            var distance = new Point(prevPosition.X - _actor.Position.X, prevPosition.Y - _actor.Position.X);
            var clampSpeedAmount = (int)(moveDistPerSecond * .32f); // Amount to reduce speed by.

            if (distance.X <= 0 || distance.Y <= 0) return;
            // Apply clamp to X axis.
            if (_actor.Position.X < 0)
                _actor.Position.X -= clampSpeedAmount;
            else
                _actor.Position.X += clampSpeedAmount;
            // Apply clamp to Y axis.
            if (_actor.Position.Y < 0)
                _actor.Position.Y -= clampSpeedAmount;
            else
                _actor.Position.Y += clampSpeedAmount;
        }

        // Render target image.
        public void RenderActorFrame(Graphics gfx)
        {
            gfx.DrawRectangle(new Pen(Color.AntiqueWhite), new Rectangle(500,1000,2000,30));
            Boundary bL = new Boundary(500, 1000, 2000, 30);
            if (bL.PointInside(_actor.GetBottom()))0.
            {
                Console.WriteLine("INSIDE ");
            }



            var img = FromFileImage("C:\\Users\\CautiousDev\\Pictures\\Game Dev\\Sprites\\Red Cloak\\actor1.gif");
            _actor.Dimension.Width = img.Width;
            _actor.Dimension.Height = img.Height;
            
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.DrawImage(img, (float)_actor.Position.X, _actor.Position.Y);
        }

        // Return target image.
        private Image FromFileImage(string filePath)
        {
            return Image.FromFile(filePath);
        }

        #region UserInput

        // Movement key map.
        private readonly Dictionary<Keys, bool> _keyMap = new Dictionary<Keys, bool>
        {
            {Keys.Up, false},
            {Keys.Down, false },
            {Keys.Left, false},
            {Keys.Right, false}
        };

        // Set key states.
        private void Actor_KeyDown(object sender, KeyEventArgs e)
        {
            if (_keyMap.ContainsKey(e.KeyCode))
                _keyMap[e.KeyCode] = true;
        }

        private void Actor_KeyUp(object sender, KeyEventArgs e)
        {
            if (_keyMap.ContainsKey(e.KeyCode))
                _keyMap[e.KeyCode] = false;
        }

        #endregion UserInput
    }
}