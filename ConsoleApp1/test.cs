using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TiffToJPG
{
    internal class test
    {
        public test()
        {
        }
        public void convert(string fileName, string outputFolder)
        {
            try
            {
                using (Image imageFile = Image.FromFile(fileName))
                {
                    FrameDimension frameDimensions = new FrameDimension(
                        imageFile.FrameDimensionsList[0]);

                    // Gets the number of pages from the tiff image (if multipage) 
                    int frameNum = imageFile.GetFrameCount(frameDimensions);
                    string[] jpegPaths = new string[frameNum];

                    for (int frame = 0; frame < frameNum; frame++)
                    {
                        // Selects one frame at a time and save as jpeg. 
                        imageFile.SelectActiveFrame(frameDimensions, frame);
                        using (Bitmap bmp = new Bitmap(imageFile))
                        {
                            jpegPaths[frame] = String.Format("{0}\\{1}{2}.jpg",
                                outputFolder,
                                Path.GetFileNameWithoutExtension(fileName),
                                frame);
                            bmp.Save(jpegPaths[frame], ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                //throw;
            }
            Console.WriteLine("End");
        }
    }
}
