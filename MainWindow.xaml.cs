using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.IO;

namespace photo_converter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            toCombo.ItemsSource = formats_list; // changing itemsSource to list 
        }
        List<ImageFormat> formats_list = new List<ImageFormat>() // list of all formats of photos
        {
            ImageFormat.Bmp,
            ImageFormat.Jpeg,
            ImageFormat.Png,
            ImageFormat.Tiff,
            ImageFormat.Wmf,
            ImageFormat.Emf,
            ImageFormat.Gif
        };
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) // move window
        {
            this.DragMove(); // 
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Exit button
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  // Minimize app
        {
            this.WindowState = WindowState.Minimized;
        }

        private void bConvert_Click(object sender, RoutedEventArgs e) // convert button
        {
            var selected_format = Formats_To(formats_list[toCombo.SelectedIndex]);
            if (files != null)
            {
                FileInfo file;
                Task.Run(() =>
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        var opened_file = (Bitmap)Image.FromFile(files[i]);
                        file = new FileInfo(files[i]);
                        opened_file.Save(AppDomain.CurrentDomain.BaseDirectory + $"{file.Name}." + selected_format.ToString(), selected_format);
                    }
                });
            }
        }
        string[] files = null;
        /// <summary>
        /// Function worked with formats
        /// </summary>
        /// <param name="format"></param>
        /// <returns>Image format</returns>
        private ImageFormat Formats_To(ImageFormat format) 
        {
            if (format == ImageFormat.Png)
                return ImageFormat.Png;
            else if (format == ImageFormat.Jpeg)
                return ImageFormat.Jpeg;
            else if (format == ImageFormat.Bmp)
                return ImageFormat.Bmp;
            else if (format == ImageFormat.Tiff)
                return ImageFormat.Tiff;
            else if (format == ImageFormat.Wmf)
                return ImageFormat.Wmf;
            else if (format == ImageFormat.Emf)
                return ImageFormat.Emf;
            else
                return null;

        }
        private void RDropZone_Drop(object sender, DragEventArgs e) // drag'n'drop zone 
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                files = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < files.Length; i++)
                {
                    filesBlock.Text += files[i] + '\n';
                }
            }
        }

        private void bClear_Click(object sender, RoutedEventArgs e) // clear files list
        {
            files = null;
            filesBlock.Text = "";
        }
    }
}
