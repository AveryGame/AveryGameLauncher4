using AgsLauncherV4.AveryGameApi.ResponseTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgsLauncherV4.AveryGameApi
{
    public class Friend
    {
        /// <summary>
        /// Gets all friends that a user has added
        /// </summary>
        /// <param name="userid">The user ID to check the friends of</param>
        /// <returns>Returns a list of type FriendBase containing ID, pending status, and SBNA status</returns>
        public static async Task<FriendData> GetFriends(string userid) => JsonConvert.DeserializeObject<FriendData>(await Main.WebClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"friend/getFriends?user={userid}")).Result.Content.ReadAsStringAsync());

        /// <summary>
        /// Gets the status of two users' friendship
        /// </summary>
        /// <param name="userid">The id of user 1 to check</param>
        /// <param name="otheruserid">The id of user 2 to check</param>
        /// <returns>Returns the status of the two users' friendship in the form of a True or False string, not JSON</returns>
        public static async Task<string> isFriend(string userid, string otheruserid) => await Main.WebClient.GetAsync($"friend/isFriend?user={userid}&otherUser={otheruserid}").Result.Content.ReadAsStringAsync();

        /// <summary>
        /// Remove a friend from a user's account
        /// </summary>
        /// <param name="otherUser">ID of the user to remove from the authenticated account's friend list</param>
        /// <returns>TODO: lol idk</returns>
        public static async Task<string> RemoveFriend(string otherUser)
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "friend/removeFriend");
            postRequest.Headers.Add("authKey", Main.AuthenticatedAccount.key);
            postRequest.Content = new StringContent(JsonConvert.SerializeObject(new { user = Main.AuthenticatedAccount.userid, otherUser }), Encoding.UTF8, "application/json");
            return await Main.WebClient.SendAsync(postRequest).Result.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Denies an incoming friend request
        /// </summary>
        /// <param name="otherUser">The ID of the incoming request's originating account</param>
        /// <returns>TODO: havent tested friend stuff. dont really want to tbh</returns>
        public static async Task<string> DenyRequest(string otherUser)
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "friend/denyRequest");
            postRequest.Headers.Add("authKey", Main.AuthenticatedAccount.key);
            postRequest.Content = new StringContent(JsonConvert.SerializeObject(new { otherUser }), Encoding.UTF8, "application/json");
            return await Main.WebClient.SendAsync(postRequest).Result.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Sends a friend request to another user
        /// </summary>
        /// <param name="otherUser">The ID of the user to send a friend request to</param>
        /// <returns>TODO: still dont know</returns>
        public static async Task<string> SendRequest(string otherUser)
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "friend/sendRequest");
            postRequest.Headers.Add("authKey", Main.AuthenticatedAccount.key);
            postRequest.Content = new StringContent(JsonConvert.SerializeObject(new { Main.AuthenticatedAccount.userid, otherUser }), Encoding.UTF8, "application/json");
            return await Main.WebClient.SendAsync(postRequest).Result.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Accepts an incoming friend request
        /// </summary>
        /// <param name="otherUser">The ID of the incoming request's originating account</param>
        /// <returns>TODO: still dont know</returns>
        public static async Task<string> AcceptRequest(string otherUser)
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "friend/acceptRequest");
            postRequest.Headers.Add("authKey", Main.AuthenticatedAccount.key);
            postRequest.Content = new StringContent(JsonConvert.SerializeObject(new { otherUser }), Encoding.UTF8, "application/json");
            return await Main.WebClient.SendAsync(postRequest).Result.Content.ReadAsStringAsync();
        }
    }
}
