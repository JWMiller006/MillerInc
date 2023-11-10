using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.ML.Models
{
    public class Prediction
    {
        public Prediction() { }

        public float X { get; set; } = -1;

        public float Y { get; set; } = -1;

        public float Width { get; set; } = -1;

        public float Height { get; set; } = -1;

        public double Confidence { get; set; } = -1;

        public string Class { get; set; } = "";

        public override string ToString()
        {
            return " { \"x\": " + this.X.ToString() + ",\"y\": " + this.Y.ToString() + ",\"width\": " + this.Width.ToString() +
                ",\"height\": " + this.Height.ToString() + ",\"confidence\": " + this.Confidence.ToString() + ",\"class\": \"" + this.Class.ToString() + "\" },";
        }
    }
}
