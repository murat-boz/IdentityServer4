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

        public BankApiService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<decimal> GetBalanceAsync(string httpClientName, string clientSecret, string endpoint)
        {
            var client = await this.CreateClientRequest(httpClientName, clientSecret);

            HttpResponseMessage response = await client.GetAsync(endpoint);

            decimal balance = 0;

            if (response.IsSuccessStatusCode)
            {
                balance = JsonConvert.DeserializeObject<decimal>(await response.Content.ReadAsStringAsync());

                Console.WriteLine($"Your Balance : {balance}");
            }

            return balance;
        }

        public async Task<List<string>> GetAccountsByIdAsync(string httpClientName, string clientSecret, string endpoint)
        {
            var client = await this.CreateClientRequest(httpClientName, clientSecret);

            HttpResponseMessage response = await client.GetAsync(endpoint);

            List<string> accounts = null;

            if (response.IsSuccessStatusCode)
            {
                accounts = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());

                foreach (var account in accounts)
                {
                    Console.WriteLine($"Account Number: {account}");
                }
            }

            return accounts;
        }

        public Task<bool> InvestAsync(string httpClientName, string clientSecret, string endpoint)
        {
            throw new NotImplementedException();
        }

        private async Task<HttpClient> CreateClientRequest(string httpClientName, string clientSecret)
        {
            HttpClient httpClient = new HttpClient();

            DiscoveryDocumentResponse discovery = await httpClient.GetDiscoveryDocumentAsync(this.configuration["AuthServerSettings"]);

            ClientCredentialsTokenRequest tokenRequest = new ClientCredentialsTokenRequest();

            tokenRequest.ClientId     = httpClientName;
            tokenRequest.ClientSecret = clientSecret;
            tokenRequest.Address      = discovery.TokenEndpoint;

            TokenResponse tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(tokenRequest);
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            return httpClient;
        }
    }
}
