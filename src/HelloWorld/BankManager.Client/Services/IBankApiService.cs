using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankManager.Client.Services
{
    public interface IBankApiService
    {
        Task<decimal> GetBalanceAsync(string httpClientName, string clientSecret, string endpoint);
        Task<List<string>> GetAccountsByIdAsync(string httpClientName, string clientSecret, string endpoint);
        Task<bool> InvestAsync(string httpClientName, string clientSecret, string endpoint);

    }
}
