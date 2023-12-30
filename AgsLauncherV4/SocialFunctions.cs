using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgsLauncherV4.AveryGameApi;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace AgsLauncherV4
{
    internal class SocialFunctions
    {
        internal static List<string> GetIncomingFriendRequests(string userid)
        {
            var friendList = Friend.GetFriends(userid).Result.friends;
            var pendingList = new List<string>();
            foreach (var friend in friendList)
            {
                if (friend.pending)
                {
                    pendingList.Add(friend.id);
                }
            }
            return pendingList;
        }

        internal static List<string> GetFriends(string userid)
        {
            var friendList = Friend.GetFriends(userid).Result.friends;
            var userIdList = new List<string>();
            foreach (var friend in friendList)
            {
                if (!friend.sbna)
                {
                    userIdList.Add(friend.id);
                }
            }
            return userIdList;
        }

        internal static async Task<GetStatusBase> GetAccountStatus(string userid)
        {
            var status = await Account.GetStatus(userid);
            return new GetStatusBase() { status = GetFriendStatusAsString(status), statusBrush = GetFriendStatusAsBrush(status) };
        }

        internal static string GetFriendStatusAsString(string status)
        {
            switch (status)
            {
                case "0":
                    return "Offline";
                case "1":
                    return "Online";
                case "2":
                    return "Playing";
                case "3":
                    return "Away";
                case "-1":
                    return "Offline";
                default:
                    return "Offline";
            }
        }

        internal static Brush GetFriendStatusAsBrush(string status)
        {
            switch (status)
            {
                case "0":
                    return new SolidColorBrush(Colors.DarkGray);
                case "1":
                    return new SolidColorBrush(Colors.Blue);
                case "2":
                    return new SolidColorBrush(Colors.Green);
                case "3":
                    return new SolidColorBrush(Colors.Yellow);
                case "-1":
                    return new SolidColorBrush(Colors.DarkGray);
                default:
                    return new SolidColorBrush(Colors.DarkGray);
            }
        }

        internal class GetStatusBase
        {
            internal string status { get; set; }
            internal Brush statusBrush { get; set;}
        }

        internal enum UserStatus
        {
            Offline,
            Online,
            Playing,
            Away,
            None = -1
        }
    }
}
