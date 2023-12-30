using AgsLauncherV4.AveryGameApi;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.System;
using System.IO;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections;
using System.Collections.Generic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AgsLauncherV4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            socialPanelLoading_Border.Visibility = Visibility.Visible;
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
            TestTheStuff();

        }

        public async void TestTheStuff()
        {
            var result = await Main.InitializeInstance("AveryMadness", "Averyfd19");
            Variables.authenticationKey = JObject.Parse(result).GetValue("key")?.ToString();
            Variables.LoggedInUser = Data.GetUserData(JObject.Parse(result).GetValue("userId")?.ToString()).Result;
            Variables.ProfilePicture = await DownloadImageFromUrl(Variables.LoggedInUser.profilePhoto);
            ProfilePicture.ProfilePicture = Variables.ProfilePicture;
            ProfileAvatar.ProfilePicture = Variables.ProfilePicture;
            ProfileName.Text = Variables.LoggedInUser.username;
            await HandleIncomingFriendRequest();
            await LoadExistingFriends();
            sneaky.Focus(FocusState.Programmatic);
            FadeOutThemeAnimation b = new FadeOutThemeAnimation();
            b.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            Storyboard sb = new Storyboard();
            Storyboard.SetTarget(b, socialPanelLoading_Border);
            Storyboard.SetTargetProperty(b, "Opacity");
            sb.Children.Add(b);
            sb.Begin();
            await Task.Delay(300);
            socialPanelLoading_Border.Visibility = Visibility.Collapsed;
            sb.Stop();
            sb.Children.Remove(b);
            var b2 = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                From = 0,
                To = 1
            };
            Storyboard.SetTarget(b2, LauncherBackgroundElement);
            Storyboard.SetTargetProperty(b2, "Opacity");
            sb.Children.Add(b2);
            sb.Begin();
            //await AveryGameApi.Main.InitializeInstance("me", "1245Qwsa@");
        }

        private static async Task<BitmapImage> DownloadImageFromUrl(string url)
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

        private async Task HandleIncomingFriendRequest()
        {
            Variables.FriendRequests = SocialFunctions.GetIncomingFriendRequests(Variables.LoggedInUser.id);
            if (Variables.FriendRequests.Count > 0)
            {
                var request = Variables.FriendRequests[0];
                var requestUser = Data.GetUserData(request).Result;
                var requestUserImage = await DownloadImageFromUrl(requestUser.profilePhoto);
                IncomingFriendRequestAvatar.ProfilePicture = requestUserImage;
                IncomingFriendRequestName.Text = requestUser.username;
                IncomingFriendRequest.Visibility = Visibility.Visible;
            }
            else if (Variables.FriendRequests.Count == 0)
            {
                IncomingFriendRequest.Visibility = Visibility.Collapsed;
            }
            return;
        }

        private async Task LoadExistingFriends()
        {
            var friends = SocialFunctions.GetFriends(Variables.LoggedInUser.id);
            var count = friends.Count;
            if (count == 0) return;
            if (count > 0)
            {
                var friend1 = friends[0];
                var friend1User = Data.GetUserData(friend1).Result;
                var friend1UserImage = await DownloadImageFromUrl(friend1User.profilePhoto);
                var friend1Status = await SocialFunctions.GetAccountStatus(friend1);
                Friend1Avatar.ProfilePicture = friend1UserImage;
                Friend1Name.Text = friend1User.username;
                Friend1.Visibility = Visibility.Visible;
                Friend1Status.Text = friend1Status.status;
                Friend1StatusIcon.Fill = friend1Status.statusBrush;
                Friend1StatusIcon.Stroke = friend1Status.statusBrush;
            }
            if (count > 1)
            {
                var friend2 = friends[1];
                var friend2User = Data.GetUserData(friend2).Result;
                var friend2UserImage = await DownloadImageFromUrl(friend2User.profilePhoto);
                var friend2Status = await SocialFunctions.GetAccountStatus(friend2);
                Friend2Avatar.ProfilePicture = friend2UserImage;
                Friend2Name.Text = friend2User.username;
                Friend1.Visibility = Visibility.Visible;
                Friend2.Visibility = Visibility.Visible;
                Friend2StatusIcon.Fill = friend2Status.statusBrush;
                Friend2StatusIcon.Stroke = friend2Status.statusBrush;
            }
            if (count > 2)
            {
                var friend3 = friends[2];
                var friend3User = Data.GetUserData(friend3).Result;
                var friend3UserImage = await DownloadImageFromUrl(friend3User.profilePhoto);
                var friend3Status = await SocialFunctions.GetAccountStatus(friend3);
                Friend3Avatar.ProfilePicture = friend3UserImage;
                Friend3Name.Text = friend3User.username;
                Friend3.Visibility = Visibility.Visible;
                Friend3StatusIcon.Fill = friend3Status.statusBrush;
                Friend3StatusIcon.Stroke = friend3Status.statusBrush;
            }
            if (count > 3)
            {
                var friend4 = friends[3];
                var friend4User = Data.GetUserData(friend4).Result;
                var friend4UserImage = await DownloadImageFromUrl(friend4User.profilePhoto);
                var friend4Status = await SocialFunctions.GetAccountStatus(friend4);
                Friend4Avatar.ProfilePicture = friend4UserImage;
                Friend4Name.Text = friend4User.username;
                Friend4.Visibility = Visibility.Visible;
                Friend4StatusIcon.Fill = friend4Status.statusBrush;
                Friend4StatusIcon.Stroke = friend4Status.statusBrush;
            }
            if (count > 4)
            {
                var friend5 = friends[4];
                var friend5User = Data.GetUserData(friend5).Result;
                var friend5UserImage = await DownloadImageFromUrl(friend5User.profilePhoto);
                var friend5Status = await SocialFunctions.GetAccountStatus(friend5);
                Friend5Avatar.ProfilePicture = friend5UserImage;
                Friend5Name.Text = friend5User.username;
                Friend5.Visibility = Visibility.Visible;
                Friend5StatusIcon.Fill = friend5Status.statusBrush;
                Friend5StatusIcon.Stroke = friend5Status.statusBrush;
            }
            if (count > 5)
            {
                var friend6 = friends[5];
                var friend6User = Data.GetUserData(friend6).Result;
                var friend6UserImage = await DownloadImageFromUrl(friend6User.profilePhoto);
                var friend6Status = await SocialFunctions.GetAccountStatus(friend6);
                Friend6Avatar.ProfilePicture = friend6UserImage;
                Friend6Name.Text = friend6User.username;
                Friend6.Visibility = Visibility.Visible;
                Friend6StatusIcon.Fill = friend6Status.statusBrush;
                Friend6StatusIcon.Stroke = friend6Status.statusBrush;
            }
            if (count > 6)
            {
                var friend7 = friends[6];
                var friend7User = Data.GetUserData(friend7).Result;
                var friend7UserImage = await DownloadImageFromUrl(friend7User.profilePhoto);
                var friend7Status = await SocialFunctions.GetAccountStatus(friend7);
                Friend7Avatar.ProfilePicture = friend7UserImage;
                Friend7Name.Text = friend7User.username;
                Friend7.Visibility = Visibility.Visible;
                Friend7StatusIcon.Fill = friend7Status.statusBrush;
                Friend7StatusIcon.Stroke = friend7Status.statusBrush;
            }
            if (count > 7)
            {
                var friend8 = friends[7];
                var friend8User = Data.GetUserData(friend8).Result;
                var friend8UserImage = await DownloadImageFromUrl(friend8User.profilePhoto);
                var friend8Status = await SocialFunctions.GetAccountStatus(friend8);
                Friend8Avatar.ProfilePicture = friend8UserImage;
                Friend8Name.Text = friend8User.username;
                Friend8.Visibility = Visibility.Visible;
                Friend8StatusIcon.Fill = friend8Status.statusBrush;
                Friend8StatusIcon.Stroke = friend8Status.statusBrush;
            }
            if (count > 8)
            {
                var friend9 = friends[8];
                var friend9User = Data.GetUserData(friend9).Result;
                var friend9UserImage = await DownloadImageFromUrl(friend9User.profilePhoto);
                var friend9Status = await SocialFunctions.GetAccountStatus(friend9);
                Friend9Avatar.ProfilePicture = friend9UserImage;
                Friend9Name.Text = friend9User.username;
                Friend9.Visibility = Visibility.Visible;
                Friend9StatusIcon.Fill = friend9Status.statusBrush;
                Friend9StatusIcon.Stroke = friend9Status.statusBrush;
            }
            if (count > 9)
            {
                var friend10 = friends[9];
                var friend10User = Data.GetUserData(friend10).Result;
                var friend10UserImage = await DownloadImageFromUrl(friend10User.profilePhoto);
                var friend10Status = await SocialFunctions.GetAccountStatus(friend10);
                Friend10Avatar.ProfilePicture = friend10UserImage;
                Friend10Name.Text = friend10User.username;
                Friend10.Visibility = Visibility.Visible;
                Friend10StatusIcon.Fill = friend10Status.statusBrush;
                Friend10StatusIcon.Stroke = friend10Status.statusBrush;
            }
            return;
        }

        private string DevBuildPath = "F:\\AveryGame\\WindowsNoEditor\\";
        private async void Launch_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(DevBuildPath);
            if (folder == null)
            {
                ContentDialog d = new ContentDialog()
                {
                    Title = "Build not Found",
                    Content = "Build at path " + folder.Path + " was not found.",
                    CloseButtonText = "Close"
                };
                await d.ShowAsync();
                return;
            }

            Windows.Storage.StorageFile File = await Windows.Storage.StorageFile.GetFileFromPathAsync(folder.Path + "\\AveryGame\\Binaries\\Win64\\AveryGameLauncher.exe");
            ContentDialog d2 = new ContentDialog()
            {
                Title = "asd",
                Content = File.Path,
                CloseButtonText = "text",
            };
            await d2.ShowAsync();
            bool success = await Windows.System.Launcher.LaunchFileAsync(File);
            ContentDialog d3 = new ContentDialog()
            {
                Title = "asd",
                Content = success.ToString(),
                CloseButtonText = "Close",
            };
            await d3.ShowAsync();
                
        }

        // Hacky fix but tapped is called twice for some reason. Thanks, Microsoft. Very cool.
        private bool bIsShown = false;
        private async void AddFriend_Confirm_Button_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AddFriend_Input.Text))
                return;
            if (AddFriend_Input.Text == ProfileName.Text)
            {
                if (bIsShown) return;
                bIsShown = true;
                ContentDialog d = new ContentDialog()
                {
                    Title = "Nice try",
                    Content = "F for effort.",
                    CloseButtonText = "Aw man"
                };
                await d.ShowAsync();
                bIsShown = false;
                return;
            }
            // check if AddFriend_Input.Text is an int
            if (bIsShown) return;
            bool isNumeric = int.TryParse(AddFriend_Input.Text, out int userId);
            if (!isNumeric) int.TryParse(Data.GetUserData(AddFriend_Input.Text).Result.id, out userId);
            bIsShown = true;
            var requestResult = await Friend.SendRequest(userId.ToString());
            ContentDialog response = new ContentDialog()
            {
                Content = "Successfully sent friend request.",
                CloseButtonText = "Ok"
            };
            await response.ShowAsync();
            AddFriend_Input.Text = "";
            bIsShown = false;
            return;
            // send friend request
        }

        private void AddFriend_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            //0.4
            //0.85
            if (AddFriend_Input.Text.Length > 0)
            {
                // animate the opacity of AddFriend_Confirm_Button to 0.85
                var storyboard = new Storyboard();
                var animation = new DoubleAnimation()
                {
                    From = AddFriend_Confirm_Button.Opacity,
                    To = 0.85,
                    Duration = new Duration(TimeSpan.FromSeconds(0.15))
                };
                Storyboard.SetTarget(animation, AddFriend_Confirm_Button);
                Storyboard.SetTargetProperty(animation, "Opacity");
                storyboard.Children.Add(animation);
                storyboard.Begin();
            }
            if (AddFriend_Input.Text.Length == 0)
            {
                // animate the opacity of AddFriend_Confirm_Button to 0.4
                var storyboard = new Storyboard();
                var animation = new DoubleAnimation()
                {
                    From = AddFriend_Confirm_Button.Opacity,
                    To = 0.4,
                    Duration = new Duration(TimeSpan.FromSeconds(0.15))
                };
                Storyboard.SetTarget(animation, AddFriend_Confirm_Button);
                Storyboard.SetTargetProperty(animation, "Opacity");
                storyboard.Children.Add(animation);
                storyboard.Begin();
            }
        }
    }
}
