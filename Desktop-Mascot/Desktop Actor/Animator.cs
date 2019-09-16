using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Desktop_Actor
{
    public class Animator
    {
        GameObject gameObject;
        Animations anims;

        public Animator(GameObject gameObject)
        {
            this.gameObject = gameObject;

            // Save anim json data and deserialize an Animation class.
            string jsonData = File.ReadAllText(@Directory.GetCurrentDirectory() + "\\Data\\Anims.json");
            anims = JsonConvert.DeserializeObject<Animations>(jsonData);
            
            ArraySetup(); // Setup arrays for overflow handling.
        }



        public DateTime PrevFrameTime;
        public DateTime FrameTime;
        public double CalculateFPS()
        {
            
            // Default starting prev to earlier time for fps.
            if (PrevFrameTime == FrameTime)            
                PrevFrameTime = DateTime.Now.AddMilliseconds(-2);
            else
                PrevFrameTime = FrameTime;

            FrameTime = DateTime.Now;
            return (FrameTime - PrevFrameTime).TotalMilliseconds / 1000;           
        }

        Point velocity;     // Calculate velocity based on average distance per sec.
        int n = 0, d = 4;   // Array overflows at length 5, d is n-1.
        Point[] prevPos = new Point[5];     // Save previous positions to get distance moved.
        Point[] curPosDif = new Point[5];   // Save distances between previous positions.
        private void ArraySetup()
        {
            for(int i = 0; i < 5; i++)
            {
                prevPos[i] = gameObject.Position;
                curPosDif[i].X = 0;
                curPosDif[i].Y = 0;
            }
        }


        public void UpdatePositions(double framesPerSecond)
        {

            // Ensure that the motion is moving at a
            var speed = 1200; // X units within 1 second
            var moveDistPerSecond = (int)(speed * framesPerSecond);

            // Follow cursor or apply gravity.
            if (gameObject.CursorDragging)
                CursorDragActor();
            else
                Gravity(moveDistPerSecond);

            gameObject.Inside(); // Keep gameobject inside boundaries.
                            
            // Update cur Point and calculate dist moved from last pos.
            prevPos[n] = gameObject.Position;
            curPosDif[n].X = prevPos[n].X - prevPos[d].X;
            curPosDif[n].Y = prevPos[n].Y - prevPos[d].Y;
            
            // Increment so that curPos overflows at array end.
            n = OverflowInt(n);
            d = OverflowInt(d);

            // Calculate avg of total differences between movement in array.
            // Then move gameobject based on that to simulate velocity/physics.
            velocity = SumAvg(curPosDif);
            PhysicsMovement(moveDistPerSecond);
           // Console.WriteLine("L"+anims.Carry_left);
            // --- DEBUG ---
            if (d == 4)
            {
               // Console.WriteLine("X::{0}, Y::{1}",velocity.X, velocity.Y);
            }          
        }



        /// <summary>
        /// Set gameobject position to be centered with cursor.
        /// </summary>
        private void CursorDragActor()
        {           
            gameObject.Position.X = (int)(Cursor.Position.X - (gameObject.Dimension.Width / 2));
            gameObject.Position.Y = (int)(Cursor.Position.Y - (gameObject.Dimension.Height / 2));
        }



        /// <summary>
        /// Apply gravitational force and logic.
        /// </summary>
        /// <param name="moveDistPerSecond">Allowed fall distance per second.</param>
        public void Gravity(int moveDistPerSecond)
        {
            gameObject.Position.Y += moveDistPerSecond; 
        }



        /// <summary>
        /// Return incremented integar with overflow.
        /// </summary>
        /// <param name="i">Integar value to increment or overflow.</param>
        /// <returns></returns>
        int OverflowInt(int i)
        {
            i++;
            if (i < 0)
                return prevPos.Length - 1;
            if (i > prevPos.Length - 1)
               return 0;
            return i;
        }



        /// <summary>
        /// Returns fake velocity based on averaged distance moved per update.
        /// </summary>
        /// <param name="points">Array of distances to average.</param>
        /// <returns></returns>
        Point SumAvg(Point[] points)
        {
            Point sumAvg = new Point(0,0);
            for(int i = 0; i < points.Length - 1; i++)
            {
                sumAvg.X += points[i].X;
                sumAvg.Y += points[i].Y;
            }
            sumAvg.X /= points.Length;
            sumAvg.Y /= points.Length;

            return sumAvg;
        }


        /// <summary>
        /// Apply physics forces on gameobject.
        /// </summary>
        /// <param name="moveDistPerSec">Allowed movement distance per second.</param>
        public void PhysicsMovement(int moveDistPerSec)
        {
            gameObject.Position.X += velocity.X;
            gameObject.Position.Y += velocity.Y;
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
            
            var img = FromFileImage("C:\\Users\\CautiousDev\\Pictures\\Game Dev\\Sprites\\Red Cloak\\use\\idle.gif");
            gameObject.Dimension.Width = img.Width;
            gameObject.Dimension.Height = img.Height;
            
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.DrawImage(img, gameObject.Position.X, gameObject.Position.Y);
        }

        // Return current animation state name based on gameobject.
    

        void PlayAnimation(int frameLength)
        {

        }


        // Return target image.
        private Image FromFileImage(string filePath)
        {
            return Image.FromFile(filePath);
        }
    }
}