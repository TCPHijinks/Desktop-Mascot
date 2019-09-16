using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Actor
{
    public class Animations
    {
        public string AnimsPath { get; private set; }
        public Animations()
        {
            AnimsPath = (Directory.GetCurrentDirectory() + "\\Data\\Frames");
        }


     
            public float FrameLength { get; set; }
            public string[] Idle { get; set; }
            public string[] Carry_left { get; set; }
            public string[] Carry_right { get; set; }
            public string[] Air_vertical { get; set; }
            public string[] Air_horizontal { get; set; }
            public string[] Walk_left { get; set; }
        



    }
}
