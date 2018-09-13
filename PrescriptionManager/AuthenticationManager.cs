﻿using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrescriptionManager
{
    class AuthenticationManager
    {
        private static readonly Lazy<AuthenticationManager> Lazy =
    new Lazy<AuthenticationManager>(() => new AuthenticationManager());

        public static AuthenticationManager Instance => Lazy.Value;

        private readonly AuthenticationContext _authContext;

        private AuthenticationManager()
        {
            _authContext = new AuthenticationContext(Settings.Authority);
        }

        public enum LoginMode
        {
            Silent,
            Interactive
        }

        public async Task<bool> RefreshTokenAsync()
        {
            var loggedIn = await LoginAsync(LoginMode.Silent);
            if (!loggedIn)
            {
                loggedIn = await LoginAsync(LoginMode.Interactive);
            }

            return loggedIn;
        }

        public AuthenticationResult AuthResult { get; private set; }

        public string ServiceAccessToken { get; private set; }

        public async Task<bool> LoginAsync(LoginMode loginMode)
        {
            try
            {
                AuthResult = await _authContext.AcquireTokenAsync(
                    Settings.GraphResourceId,
                    Settings.ClientId,
                    Settings.RedirectUri,
                    new PlatformParameters(loginMode == LoginMode.Silent ?
                                                        PromptBehavior.Always :
                                                        PromptBehavior.SelectAccount));
                //Set PromptBehavior to Never to enforce silent login for domain users.

                ServiceAccessToken = 
                    (await _authContext.AcquireTokenAsync(
                        Settings.ServicesClientId,
                        Settings.ClientId,
                        Settings.RedirectUri,
                        new PlatformParameters(PromptBehavior.Auto)))
                        .AccessToken;

                return true;
            }
            catch (AdalException ex)
            {
                if (ex.ErrorCode != "user_interaction_required")
                {
                    // An unexpected error occurred.
                    MessageBox.Show(ex.Message);
                }
                return false;
            }
        }

        public async Task<bool> IsUserInGroup(string groupId)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", AuthResult.AccessToken);

                var body = new JObject
                {
                    ["groupId"] = groupId,
                    ["memberId"] = AuthResult.UserInfo.UniqueId
                };

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"{Settings.GraphApiEndpoint}{Settings.Tenant}/isMemberOf?api-version={Settings.GraphApiVersion}"),
                    Method = HttpMethod.Post
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

                var responseMessage = await client.SendAsync(request);
                responseMessage.EnsureSuccessStatusCode();

                var jsonResult = await responseMessage.Content.ReadAsStringAsync();

                var result = JObject.Parse(jsonResult);
                var isMemberOf = result["value"]?.Value<bool>();
                return isMemberOf ?? false;
            }
            catch
            {
                return false;
            }
        }
    }
}
