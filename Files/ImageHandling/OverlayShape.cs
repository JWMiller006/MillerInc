using MillerInc.ML.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using MillerInc.Convert.Files;

namespace MillerInc.Files.ImageHandling
{
    /// <summary>
    /// This class contains the logic in order to overlay an image with shapes
    /// </summary>
    public class OverlayShape
    {

        /// <summary>
        /// This image was indended to be used with AI model output (specifically Roboflow) 
        /// and overlay the image with
        /// the bounding box 
        /// </summary>
        /// <param name="imageOutputPath">The path and name to where you want it output. 
        /// NOTE: If file already exits, 
        /// it will overwrite the file</param>
        /// <param name="jsonFilepath">Path to json file that follows this format. 
        /// The json file should follow the class 
        /// <see cref="InferenceOutput">InferenceOutput</see> </param>
        /// <param name="originFilePath">The path to the file that you want to edit</param>
        /// <param name="overlayColor">The color that you want the marks to be</param>
        /// <param name="penSize">The size of the mark that you want to write with</param>
        public static void AddBox(string imageOutputPath, string jsonFilepath, string originFilePath,
            Color overlayColor, int penSize = 5)
        {
            InferenceOutput output = JSON_Converter.Deserialize<InferenceOutput>(jsonFilepath);

            try
            {
                Bitmap bitmap = new(originFilePath);
                Graphics g = Graphics.FromImage(bitmap);
                Pen pen = new(overlayColor, penSize);
                foreach (Prediction p in output.Predictions)
                {
                    float x1 = (float)(p.X - (0.5 * p.Width));
                    float y1 = (float)(p.Y - (0.5 * p.Height));
                    float x2 = x1 + p.Width;
                    float y2 = y1 + p.Height;
                    g.DrawLine(pen, x1, y1, x2, y1);
                    g.DrawLine(pen, x2, y1, x2, y2);
                    g.DrawLine(pen, x2, y2, x1, y2);
                    g.DrawLine(pen, x1, y2, x1, y1);
                }
                if (File.Exists(imageOutputPath))
                {
                    File.Delete(imageOutputPath);
                }
                bitmap.Save(imageOutputPath);
            }
            catch
            {
                return;
            }
        }


        /// <summary>
        /// Adds a box over the image at the file given
        /// </summary>
        /// <param name="imageOutputPath">The path and name to where you want it output. 
        /// NOTE: If file already exits, 
        /// it will overwrite the file</param>
        /// <param name="originFilePath">The path to the file that you want to edit</param>
        /// <param name="x">The x-coordinate of the center of the box that you want to add</param>
        /// <param name="y">The y-coordinate of the center of the box that you want to add</param>
        /// <param name="height">The total height of the box that you want to add</param>
        /// <param name="width">The total width of the box that you want to add</param>
        /// <param name="overlayColor">The color that you want to overlay</param>
        /// <param name="penSize">The width of the pen that you want to use</param>
        public static void AddBox(string imageOutputPath, string originFilePath,
            int x, int y, int height, int width, Color overlayColor, int penSize = 5)
        {

            try
            {
                Bitmap bitmap = new(originFilePath);
                Graphics g = Graphics.FromImage(bitmap);
                Pen pen = new(overlayColor, penSize);
                float x1 = (float)(x - (0.5 * width));
                float y1 = (float)(y - (0.5 * height));
                float x2 = x1 + width;
                float y2 = y1 + height;
                g.DrawLine(pen, x1, y1, x2, y1);
                g.DrawLine(pen, x2, y1, x2, y2);
                g.DrawLine(pen, x2, y2, x1, y2);
                g.DrawLine(pen, x1, y2, x1, y1);
                if (File.Exists(imageOutputPath))
                {
                    File.Delete(imageOutputPath);
                }
                bitmap.Save(imageOutputPath);
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Adds a box overtop the bitmap that is input through the function
        /// </summary>
        /// <param name="bitmap">the source bitmap </param>
        /// <param name="centerOfBoxX">the x coordinate of the center of the box</param>
        /// <param name="centerOfBoxY">the y coordinate of the center of the box</param>
        /// <param name="width">the width of the box</param>
        /// <param name="height">this height of the box</param>
        /// <param name="overlayColor">the color to overlay the box with</param>
        /// <param name="penSize">this size of the pen to use</param>
        /// <returns></returns>
        public static Bitmap AddBox(Bitmap bitmap, float centerOfBoxX, float centerOfBoxY, float width,
            float height, Color overlayColor, int penSize = 5)
        {
            float x = centerOfBoxX;
            float y = centerOfBoxY;
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new(overlayColor, penSize);
            float x1 = (float)(x - (0.5 * width));
            float y1 = (float)(y - (0.5 * height));
            float x2 = x1 + width;
            float y2 = y1 + height;
            g.DrawLine(pen, x1, y1, x2, y1);
            g.DrawLine(pen, x2, y1, x2, y2);
            g.DrawLine(pen, x2, y2, x1, y2);
            g.DrawLine(pen, x1, y2, x1, y1);
            return bitmap;
        }

        /// <summary>
        /// Adds a box overtop the bitmap that is input through the function
        /// </summary>
        /// <param name="bitmap">the source bitmap </param>
        /// <param name="topLeftX">the x coordinate of the top left corner of the box</param>
        /// <param name="topLeftY">the y coordinate of the top left corner of the box</param>
        /// <param name="width">the width of the box</param>
        /// <param name="height">the height of the box</param>
        /// <param name="penSize">the size of the pen</param>
        /// <param name="overlayColor">the color to  overlay the box as</param>
        /// <returns></returns>
        public static Bitmap AddBox(Bitmap bitmap, float topLeftX, float topLeftY, float width,
            float height, int penSize, Color overlayColor)
        {
            float x = topLeftX;
            float y = topLeftY;
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new(overlayColor, penSize);
            float x1 = x;
            float y1 = y;
            float x2 = x1 + width;
            float y2 = y1 + height;
            g.DrawLine(pen, x1, y1, x2, y1);
            g.DrawLine(pen, x2, y1, x2, y2);
            g.DrawLine(pen, x2, y2, x1, y2);
            g.DrawLine(pen, x1, y2, x1, y1);
            return bitmap;
        }
    }
}
