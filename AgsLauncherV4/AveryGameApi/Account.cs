using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgsLauncherV4.AveryGameApi
{
    public static class Account
    {
        internal static async Task<string> AuthenticateUser() => await Main.WebClient.PostAsync("account/authenticateUser", new StringContent(JsonConvert.SerializeObject(new { Main.AuthenticatedAccount.username, Main.AuthenticatedAccount.password }), Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();

        /// <summary>
        /// Gets the amount of MTX currency the authenticated user has
        /// </summary>
        /// <returns>Returns the amount of MTX currency the authenticated user has</returns>
        public static async Task<string> GetMtxCurrency()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "account/getMtxCurrency");
            requestMessage.Headers.Add("user", Main.AuthenticatedAccount.userid);
            requestMessage.Headers.Add("authkey", Main.AuthenticatedAccount.key);
            var response = await Main.WebClient.SendAsync(requestMessage);
            return JObject.Parse(await response.Content.ReadAsStringAsync()).GetValue("mtx").ToString();
        }

        public static async Task<string> GetStatus(string userid) => JObject.Parse(await Main.WebClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"account/status/{userid}")).Result.Content.ReadAsStringAsync()).GetValue("status").ToString();
    }
}
