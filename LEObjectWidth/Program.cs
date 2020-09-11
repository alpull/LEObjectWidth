using static LEObjectWidth.ImageAnalyzer;

namespace LEObjectWidth {

    class Program {

        static void Main(string[] args) {
            const string path = @"D:\Desktop\ObjectWidth\Image__2019-11-29__13-43-06.bmp";
            // const string path = @"D:\Desktop\ObjectWidth\Image__2019-11-29__13-44-27.bmp";

            GetAverageObjectWidth(path);
        }

    }

}
