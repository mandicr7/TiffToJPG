using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
//using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TiffToJPG
{
    //Note that these attributes are localized so you need to localize this attribute for Studio languages other than English

    //Dots allow for hierarchy. App Integration.Excel is where Excel activities are.
    [Category("Tools")]
    [DisplayName("Tif to JPEG converter")]
    [Description("Convert TIF files into JPG")]
    public class TiffToJPG : CodeActivity
    {
        //Note that these attributes are localized so you need to localize this attribute for Studio languages other than English
        [Category("Input")]
        [DisplayName("TIF File Path")]
        [Description("Enter file path to covert")]
        [RequiredArgument]
        public InArgument<string> tifFilePath { get; set; }
        [Category("Input")]
        [DisplayName("Output folder")]
        [Description("Output folder")]
        [RequiredArgument]
        public InArgument<string> outputFolder { get; set; }
        //[Category("Output")]
        //public OutArgument<bool> ResultNumber { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                using (Image imageFile = Image.FromFile(tifFilePath.Get(context)))
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
                                outputFolder.Get(context),
                                Path.GetFileNameWithoutExtension(tifFilePath.Get(context)),
                                frame);
                            bmp.Save(jpegPaths[frame], ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
