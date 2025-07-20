using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImgColorReplace {

  internal class Program {

    static void Main() {

      Color oldColor = Color.FromArgb(38,28,26); //todo: from console
      Color newColor = Color.FromArgb(43,43,43); //todo: from console

      while (true) {
        Console.WriteLine("Enter base64 image to replace color or empty string to exit:");
        var base64Image = Console.ReadLine();
        if (string.IsNullOrEmpty(base64Image))
          return;

        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream ms = new MemoryStream(imageBytes))
        using (Bitmap bitmap = new Bitmap(ms)) {
          for (int y = 0; y < bitmap.Height; y++) {
            for (int x = 0; x < bitmap.Width; x++) {
              Color pixel = bitmap.GetPixel(x, y);
              if (pixel.R == oldColor.R && pixel.G == oldColor.G && pixel.B == oldColor.B) {
                bitmap.SetPixel(x, y, newColor);
              }
            }
          }

          using (MemoryStream output = new MemoryStream()) {
            bitmap.Save(output, ImageFormat.Png);
            string newBase64 = Convert.ToBase64String(output.ToArray());
            Console.WriteLine("New base64:");
            Console.WriteLine(newBase64);
          }
        }
      }
    }
  }
}
