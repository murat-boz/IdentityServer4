using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BankManager.Client.HttpHandlers
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var accessToken =
                await this.httpContextAccessor.HttpContext
                    .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.SetBearerToken(accessToken);
                }
            }
            catch (Exception e)
            {

            }

            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
