using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QRCoder;
using HandyControl.Data;
using HandyControl.Themes;

namespace DLUTToolBox_V2
{
    /// <summary>
    /// QRPayCodeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QRPayCodeWindow : HandyControl.Controls.Window
    {
        public QRPayCodeWindow(string uri)
        {
            InitializeComponent();
            ThemeManager.Current.SystemThemeChanged += OnSystemThemeChanged;
            LoadUrlToQRCode(uri);
        }

        private void OnSystemThemeChanged(object sender, FunctionEventArgs<ThemeManager.SystemTheme> e)
        {
            ThemeManager.Current.ApplicationTheme = e.Info.CurrentTheme;
        }

        void LoadUrlToQRCode(string url)
        {
            try
            {
                QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(1);
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, eccLevel))
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {
                            System.Drawing.Bitmap bmp = qrCode.GetGraphic(20, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                            IntPtr hBitmap = bmp.GetHbitmap();
                            System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            QRCode.Source = WpfBitmap;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteErrLog(e);
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
