using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop
{
    class GetRestClient
    {
        public string BaseAddress { get; set; } = "https://h-8a3ba60b7c394158903b6bdc6ae2db6f.hopper.relativity.com/";
        public string ClientId { get; set; } = "592ba3d029667c0a890c3ee3cc";
        public string ClientSecret { get; set; } = "7c10c6c4a68340a2607594d4bb88429b2d6cc0a1";
        public HttpClient RestClient { get; private set; }


        public GetRestClient()
        {

        }

        private HttpClient GetHttpClient()
        {
            //Initialize the HttpClient.
            return new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        private string GetHeader()
        {

            //Generate a value for the Authorization header
            AuthHelper authHelper = new AuthHelper(BaseAddress);
            OAuthToken token = authHelper.GenerateOAuthToken(ClientId, ClientSecret);

            return $"{token.TokenType} {token.AccessToken}";
        }

        public HttpClient GenerateRestClient()
        {
            RestClient = GetHttpClient();

            //Add the Accept header.
            RestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            RestClient.DefaultRequestHeaders.Add("Authorization", GetHeader());
            RestClient.DefaultRequestHeaders.Add("X-CSRF-Header", string.Empty);

            return RestClient;

        }



    }
}