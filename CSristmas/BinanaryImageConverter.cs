using SkiaSharp;

namespace CSristmas;

public class BinanaryImageConverter
{
    public static int[,] ConvertToBinaryImage(string imagePath)
    {
        using var image = LoadImage(imagePath);

        int width = image.Width;
        int height = image.Height;
        int[,] binaryArray = new int[width, height];

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                SKColor pixel = image.GetPixel(x, y);
                double scale = (pixel.Red * 0.3 + pixel.Green * 0.59 + pixel.Blue * 0.11);
                binaryArray[y, x] = scale < 128 ? 1 : 0;
            }
        }

        return binaryArray;
    }

    private static SKBitmap LoadImage(string path)
    {
        using var stream = new SKFileStream(path);
        return SKBitmap.Decode(stream);
    }
}

