using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColourAtlas
{
    /// <summary>
    /// Логика взаимодействия для ImageShow.xaml
    /// </summary>
    public partial class ImageShow : Window
    {



        public ImageShow()
        {
            InitializeComponent();
        }

        private void imageSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Save current canvas transform
                Transform transform = imageCanvas.LayoutTransform;
                // reset current transform (in case it is scaled or rotated)
                imageCanvas.LayoutTransform = null;

                // Get the size of canvas
                Size size = new Size(imageCanvas.Width, imageCanvas.Height);

                // Measure and arrange the imageCanvas
                // VERY IMPORTANT
                imageCanvas.Measure(size);
                imageCanvas.Arrange(new Rect(size));

                double maxWidth = Math.Round(size.Width / 2.54 * 96.0); // A4 width in pixels at 96 dpi
                double maxHeight = Math.Round(size.Height / 2.54 * 96.0); // A4 height in pixels at 96 dpi
                double scale = 1.0;
                scale = Math.Min(scale, maxWidth / size.Width);
                scale = Math.Min(scale, maxHeight / size.Height);




                // Create a render bitmap and push the imageCanvas to it
                RenderTargetBitmap renderBitmap =
                  new RenderTargetBitmap(
                    (int)size.Width * (int)scale,
                    (int)size.Height * (int)scale,
                    96d,
                    96d,
                    PixelFormats.Pbgra32);

                Rect bounds = VisualTreeHelper.GetDescendantBounds(imageCanvas);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext ctx = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(imageCanvas);
                    ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
                }

                renderBitmap.Render(dv);
                string path = Environment.CurrentDirectory + @"\Image1.jpg";
                // Create a file stream for saving image
                using (FileStream outStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // Use png encoder for our data
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    // push the rendered bitmap to it
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    // save the data to the stream
                    encoder.Save(outStream);
                }
                // Restore previously saved layout
                imageCanvas.LayoutTransform = transform;
                // i++;

            }
            //}
            catch (Exception)
            {

            }
        }

    }

}
