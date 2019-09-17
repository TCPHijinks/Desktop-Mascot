using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Actor
{
    public class Box : Control
    {
        public Box()
        {
            // Prevent mouse clicks from 'stealing' focus:
            TabStop = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawText(e.Graphics);
        }

        // Display control text so we know what the button does:
        private void DrawText(Graphics graphics)
        {
            using (var brush = new SolidBrush(this.ForeColor))
            {
                graphics.DrawString(
                    this.Text, this.Font, brush,
                    new PointF()
                    {
                        X = ((float)this.Width / 2f),
                        Y = (((float)this.Height / 2f) - (this.Font.GetHeight(graphics) / 2f))
                    },
                    new StringFormat()
                    {
                        Alignment = StringAlignment.Center
                    });
            }
        }
    }
}
