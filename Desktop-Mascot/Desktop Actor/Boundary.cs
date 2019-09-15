using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Desktop_Actor
{
    public class Boundary
    {
        private Rectangle _boundary;
        public Boundary(int posX, int posY, int width, int height)
        {
            _boundary = new Rectangle(posX,posY,width,height);
         
            //gfx.DrawRectangle(new Pen(Color.AliceBlue), r);
           


        }

        public bool Inside(Point point)
        {
            return _boundary.Contains((point));
        }

        
        
       
    }
}
