using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Desktop_Actor
{
    public class Animator
    {
        public readonly GameObject gameObject = new GameObject();
        
        public Animator(Control animControl)
        {
            // Subscribe to key press events.
            animControl.MouseDown += MouseClick;
            animControl.MouseUp += MouseUp;

            // Starting position.
            gameObject.Dimension.Width = animControl.Width / 3;
            gameObject.Dimension.Height = animControl.Height / 3;
        }



        public DateTime PrevFrameTime;
        public DateTime FrameTime;
        public double CalculateFPS()
        {
            // Default starting prev.
            if (PrevFrameTime == FrameTime)
                PrevFrameTime = DateTime.Now;
            else
                PrevFrameTime = FrameTime;

            FrameTime = DateTime.Now;
            return (FrameTime - PrevFrameTime).TotalMilliseconds / 1000;           
        }



        public void UpdatePositions(double framesPerSecond)
        {
            // Ensure that the motion is moving at a
            var speed = 200; // X units within 1 second
            var moveDistPerSecond = (int)(speed * framesPerSecond);

            if (_grabbingGameObject)
                CursorDragActor();
            else
                Physics(moveDistPerSecond);
        }



        #region Event Methods

        private bool _grabbingGameObject;

        private void MouseClick(object sender, MouseEventArgs e)
        {
            _grabbingGameObject = true;
            Console.WriteLine("CALL");
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            _grabbingGameObject = false;
            Console.WriteLine("RELEASE");
        }

        #endregion Event Methods



        private void CursorDragActor()
        {          
            // Position actor so centered with cursor.
            gameObject.Position.X = (int)(Cursor.Position.X - (gameObject.Dimension.Width / 2));
            gameObject.Position.Y = (int)(Cursor.Position.Y - (gameObject.Dimension.Height / 2));
        }

        

        public void Physics(int moveDistPerSecond)
        {
            gameObject.Position.Y += moveDistPerSecond; // Gravity.
        }



        /// <summary>
        /// Clamps speed but limiting maximum movement each update from exceeding maximum.
        /// </summary>
        /// <param name="prevPosition"></param>
        /// <param name="moveDistPerSecond"></param>
        private void ClampSpeed(Point prevPosition, float moveDistPerSecond)
        {
            // Get distance from positions before and after movement during this update.
            var distance = new Point(prevPosition.X - gameObject.Position.X, prevPosition.Y - gameObject.Position.X);
            var clampSpeedAmount = (int)(moveDistPerSecond * .32f); // Amount to reduce speed by.

            if (distance.X <= 0 || distance.Y <= 0) return;
            // Apply clamp to X axis.
            if (gameObject.Position.X < 0)
                gameObject.Position.X -= clampSpeedAmount;
            else
                gameObject.Position.X += clampSpeedAmount;
            // Apply clamp to Y axis.
            if (gameObject.Position.Y < 0)
                gameObject.Position.Y -= clampSpeedAmount;
            else
                gameObject.Position.Y += clampSpeedAmount;
        }



        // Render target image.
        public void RenderActorFrame(Graphics gfx)
        {
            var img = FromFileImage("C:\\Users\\CautiousDev\\Pictures\\Game Dev\\Sprites\\Red Cloak\\actor1.gif");
            gameObject.Dimension.Width = img.Width;
            gameObject.Dimension.Height = img.Height;
            
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.DrawImage(img, gameObject.Position.X, gameObject.Position.Y);
        }



        // Return target image.
        private Image FromFileImage(string filePath)
        {
            return Image.FromFile(filePath);
        }
    }
}