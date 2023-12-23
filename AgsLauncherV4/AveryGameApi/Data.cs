using AgsLauncherV4.AveryGameApi.ResponseTypes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace AgsLauncherV4.AveryGameApi
{
    public class Data
    {
        /// <summary>
        /// Gets content from the server's "data" folder in the backend
        /// </summary>
        /// <param name="contentSubPage">The name of the json file in the folder to get</param>
        /// <returns>Returns the content of the json file</returns>
        public static async Task<string> GetContent(string contentSubPage) => JObject.Parse(await Main.WebClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"data/content/{contentSubPage}")).Result.Content.ReadAsStringAsync()).GetValue("content").ToString();

        /// <summary>
        /// Gets all information of a users profile excluding password and token
        /// </summary>
        /// <param name="userQuery">The username or ID to get the data of</param>
        /// <returns>The queried profile data</returns>
        public static async Task<UserData> GetUserData(string userQuery) => JsonConvert.DeserializeObject<UserData>(await Main.WebClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"data/user/{userQuery}")).Result.Content.ReadAsStringAsync());
    }
}
