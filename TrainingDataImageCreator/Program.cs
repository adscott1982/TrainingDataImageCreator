using System.IO;
using System.Drawing;
using System;

namespace TrainingDataImageCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var directory = new DirectoryInfo(@"D:\Training Images");
            directory.Create();

            var index = 0;

            foreach(var line in File.ReadAllLines("mnist_test.csv"))
            {
                index++;
                SaveBitmapFromTrainingLine(directory, index, line);
            }
        }

        private static void SaveBitmapFromTrainingLine(DirectoryInfo directory, int index, string imageString)
        {
            var arraySquare = 28;
            var numberRepresented = int.Parse(imageString[0].ToString());
            imageString = imageString.Substring(2);
            var imageArray = imageString.Split(',');

            var fileName = Path.Combine(directory.FullName, $"{index:D5}-{numberRepresented}.png");
            Console.Write($"Writing {fileName}... ");
            Bitmap bitmap = new Bitmap(arraySquare, arraySquare);

            for (var y = 0; y < arraySquare; y++)
            {
                for (var x = 0; x < arraySquare; x++)
                {
                    var position = y * arraySquare + x;
                    var intensity = 255 - int.Parse(imageArray[position]);
                    var color = Color.FromArgb(intensity, intensity, intensity);
                    bitmap.SetPixel(x, y, color);
                }
            }

            bitmap.Save(fileName);
            Console.WriteLine("done.");
        }
    }
}
