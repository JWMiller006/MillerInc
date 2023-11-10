using MillerInc.UI.OutputFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.ML.Models
{
    public class InferenceOutput
    {
        public InferenceOutput() { }

        public string FileName { get; set; } = "";

        public double Time { get; set; } = 0;

        public Image Image { get; set; } = new();

        public List<Prediction> Predictions { get; set; } = new();

        public bool Equals(InferenceOutput obj)
        {
            if (obj == null) return false;
            try
            {
                return this.Image.Equals(obj.Image);
            }
            // Just for this case
            catch { return true; }
        }

        public override string ToString()
        {
            return "{ \"fileName\": \"" + this.FileName + "\", \"time\": " + this.Time.ToString() + ", " + this.Image.ToString() +
                ", \"predictions\": [ " + this.GetString() + " ] }";
        }

        private string GetString()
        {
            string output = "";
            foreach (var item in this.Predictions)
            {
                output += item.ToString();
                output += ",";
            }
            try
            {
                output = output.Remove(output.Length - 2);
            }
            catch (Exception e)
            {
                Output outputter = new(AppDomain.CurrentDomain.BaseDirectory + "\\output.txt");
                outputter.WriteLine("\n\n Error @InferenceOutput.cs\n" + e.Message);
            }
            return output;
        }
    }
}
