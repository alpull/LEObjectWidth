using System;
using System.Collections.Generic;
using System.Drawing;

namespace LEObjectWidth {

    public static class ImageAnalyzer {

        private const double PIXEL_TO_MM = 0.1;

        // Acceptable difference in brightness ratios between two pixels next to each other for the pixels to be
        // considered to be similarly bright.
        // Depending on the delta accuracy, the width is expected to vary by around 0.5-1 mm.
        private const double BRIGHTNESS_RATIO_DELTA = 10;

        private static List<int> _xCoordinates;

        public static void GetAverageObjectWidth(string imgPath) {
            using Bitmap img = new Bitmap(Image.FromFile(imgPath));
            int height = img.Height;

            double average = 0;

            for (int i = 0; i < height; i++) {
                average += GetObjectWidth(i, img);
            }

            Console.WriteLine("\nAverage width: " + average / height);
        }

        private static double GetObjectWidth(int height, Bitmap img) {
            _xCoordinates = new List<int>();

            for (int i = 0; i < img.Width; i++) {
                Color currentColor = img.GetPixel(i, height);

                if (i > 0) {
                    Color previousColor = img.GetPixel(i - 1, height);

                    double currentBrightnessRatio =
                        (2126 * currentColor.R + 7152 * currentColor.G + 722 * currentColor.B) / 10000;
                    double previousBrightnessRatio =
                        (2126 * previousColor.R + 7152 * previousColor.G + 722 * previousColor.B) / 10000;

                    if (Math.Abs(currentBrightnessRatio - previousBrightnessRatio) > BRIGHTNESS_RATIO_DELTA) {
                        _xCoordinates.Add(i);
                    }
                }
            }

            int pixelWidth = 0;

            if (_xCoordinates.Count > 1) {
                pixelWidth = _xCoordinates[^1] - _xCoordinates[0];
            }

            return pixelWidth * PIXEL_TO_MM;
        }

    }

}
