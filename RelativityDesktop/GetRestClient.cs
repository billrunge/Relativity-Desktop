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
        public string BaseAddress { get; set; } = "https://h-0f8a1f0eaf7f49c5bdf82fef66270255.hopper.relativity.com/";
        public string ClientId { get; set; } = "a75b04197f6744bedb6520a42f";
        public string ClientSecret { get; set; } = "8db9fa50c2f602321836f59566e84b96283fa0db";
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