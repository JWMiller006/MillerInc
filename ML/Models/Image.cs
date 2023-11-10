using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.ML.Models
{
    public class Image
    {
        public Image() { }

        public int Width { get; set; } = -1;

        public int Height { get; set; } = -1;

        public override string ToString()
        {
            return "\"image\": { \"width\": " + this.Width.ToString() + ", \"height\": " + this.Height.ToString() + " }";
        }
    }
}
