using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiffToJPG;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            test t = new test();

            string outputFolder = "C:\\Users\\####\\Downloads\\Telegram Desktop\\";
            string fileName = "C:\\Users\\####\\Downloads\\00400001010000000895RE_mensaje.tiff";

            t.convert(fileName, outputFolder);
        }
    }
}
