using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgsLauncherV4.AveryGameApi
{
    public class Game
    {
        /// <summary>
        /// Checks if the version of the game is the latest version
        /// </summary>
        /// <param name="version">The version the client has installed</param>
        /// <returns>Returns if the version the client has installed is the latest version or not</returns>
        public static async Task<string> CheckGameVersion(string version) => JObject.Parse(await Main.WebClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"game/checkGameVersion?version={version}")).Result.Content.ReadAsStringAsync()).GetValue("correctVersion").ToString();
    }
}
