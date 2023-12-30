using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgsLauncherV4.AveryGameApi;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Media.Imaging;

namespace AgsLauncherV4
{
    internal static class Variables
    {
        internal static AveryGameApi.ResponseTypes.UserData LoggedInUser = null;
        internal static BitmapImage ProfilePicture = null;
        internal static List<string> FriendRequests = new List<string>();
    }
}
