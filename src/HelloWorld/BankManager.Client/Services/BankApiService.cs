using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankManager.Client.Services
{
    public class BankApiService : IBankApiService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public BankApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration     = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<decimal> GetBalanceAsync(string httpClientName, string clientSecret, string endpoint)
        {
            var content = await this.CreateClientRequest(httpClientName, endpoint);

            return JsonConvert.DeserializeObject<decimal>(content);
        }

        public async Task<List<string>> GetAccountsByIdAsync(string httpClientName, string clientSecret, string endpoint)
        {
            var content = await this.CreateClientRequest(httpClientName, endpoint);

            return JsonConvert.DeserializeObject<List<string>>(content);
        }

        public Task<bool> InvestAsync(string httpClientName, string clientSecret, string endpoint)
        {
            throw new NotImplementedException();
        }

        private async Task<string> CreateClientRequest(string httpClientName, string endpoint)
        {
            var client = this.httpClientFactory.CreateClient(httpClientName);

            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        //private async Task<HttpClient> CreateClientRequest(string httpClientName, string clientSecret)
        //{
        //    HttpClient httpClient = new HttpClient();

        //    DiscoveryDocumentResponse discovery = await httpClient.GetDiscoveryDocumentAsync(this.configuration["AuthServerSettings"]);

        //    ClientCredentialsTokenRequest tokenRequest = new ClientCredentialsTokenRequest();

        //    tokenRequest.ClientId     = httpClientName;
        //    tokenRequest.ClientSecret = clientSecret;
        //    tokenRequest.Address      = discovery.TokenEndpoint;

        //    TokenResponse tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(tokenRequest);
        //    httpClient.SetBearerToken(tokenResponse.AccessToken);

        //    return httpClient;
        //}
    }
}
