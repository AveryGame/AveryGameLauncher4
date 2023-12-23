using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AgsLauncherV4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var size = new Size(1021, 500);
            ApplicationView.PreferredLaunchViewSize = size;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            Window.Current.CoreWindow.SizeChanged += (s, e) => {
                ApplicationView.GetForCurrentView().TryResizeView(size);
            };
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationView.PreferredLaunchViewSize = size;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            TestTheStuff();

        }

        public async void TestTheStuff()
        {
            var result = await AveryGameApi.Main.InitializeInstance("me", "1245Qwsa@");
            ContentDialog d = new ContentDialog()
            {
                Title = "debug data",
                Content = result.ToString(),
                CloseButtonText = "close"
            };
            await d.ShowAsync();

            string mtxres = await AveryGameApi.Account.GetMtxCurrency();
            ContentDialog mtxd = new ContentDialog()
            {
                Title = "debug data",
                Content = mtxres.ToString(),
                CloseButtonText = "close"
            };
            await mtxd.ShowAsync();
            ProfilePicture.ProfilePicture = await DownloadImageFromUrl(AveryGameApi.Data.GetUserData(JObject.Parse(result).GetValue("userId").ToString()).Result.profilePhoto);
            //await AveryGameApi.Main.InitializeInstance("me", "1245Qwsa@");
        }

        public async Task<BitmapImage> DownloadImageFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var buffer = await client.GetByteArrayAsync(url);
                using (var stream = new InMemoryRandomAccessStream())
                {
                    await stream.WriteAsync(buffer.AsBuffer());
                    stream.Seek(0);
                    var image = new BitmapImage();
                    await image.SetSourceAsync(stream);
                    return image;
                }
            }
        }


    }
}
