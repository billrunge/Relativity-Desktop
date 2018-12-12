using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop
{
    class AuthHelper
    {
        public string BaseAddress { get; }

        public AuthHelper(string baseAddress)
        {
            BaseAddress = baseAddress;

        }

        public OAuthToken GenerateOAuthToken(string clientId, string clientSecret)
        {
            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "client_id", clientId ),
                            new KeyValuePair<string, string>( "client_secret", clientSecret ),
                            new KeyValuePair<string, string> ( "scope", "SystemUserInfo" ),
                            new KeyValuePair<string, string> ( "grant_type", "client_credentials" )
                        };

            FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
            HttpResponseMessage response = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                response = client.PostAsync($"{ BaseAddress }Relativity/Identity/connect/token", content).Result;
            }

            string jsonString = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(jsonString))
            {
                throw new Exception("Failed to receive OAuth header from Relativity instance");
            }

            JObject jsonObject = JObject.Parse(jsonString);


            string accessToken = jsonObject["access_token"].ToString();
            string tokenType = jsonObject["token_type"].ToString();
            if (!int.TryParse(jsonObject["expires_in"].ToString(), out int expiresIn))
            {
                throw new Exception("Unable to cast expires_in from OAuth provider to Int32");
            }

            return new OAuthToken()
            {
                AccessToken = accessToken,
                TokenType = tokenType,
                ExpirationTime = DateTime.Now.AddSeconds(expiresIn - 30)

            };

        }
    }
}
