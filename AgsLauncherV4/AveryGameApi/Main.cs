using System.Security.Principal;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace AgsLauncherV4.AveryGameApi
{
    public static class Main
    {
        internal static readonly HttpClient WebClient = new HttpClient();
        internal static AccountData AuthenticatedAccount = new AccountData();


        /// <summary>
        /// Initializes the instance of the API to interact with AveryGame's backend.
        /// </summary>
        /// <param name="username">The username of the account to use with the AveryGame backend</param>
        /// <param name="password">The password of the account to use with the AveryGame backend</param>
        /// <returns>Returns the success of the authentication, true or false</returns>
        public static async Task<string> InitializeInstance(string username, string password)
        {
            AuthenticatedAccount.username = username;
            AuthenticatedAccount.password = password;
            WebClient.BaseAddress = new Uri("https://agbackend.cutetw.ink/api/v1/");
            var authResponse = await Account.AuthenticateUser();
            if (JObject.Parse(authResponse).GetValue("message").ToString() != "Success") return "Login failed!";
            AuthenticatedAccount.key = JObject.Parse(authResponse).GetValue("key").ToString();
            AuthenticatedAccount.userid = JObject.Parse(authResponse).GetValue("userId").ToString();
            //continue login flow
            return authResponse;
        }
    }
}