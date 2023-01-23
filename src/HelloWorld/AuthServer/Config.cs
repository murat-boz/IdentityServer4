using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace AuthServer
{
    public static class Config
    {
        //The permissions that will use on the APIs are defined.
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> 
            { 
                new ApiScope("BankA.Write", "BankA write permission"),
                new ApiScope("BankA.Read" , "BankA read permission"),
                new ApiScope("BankB.Write", "BankA write permission"),
                new ApiScope("BankB.Read" , "BankA read permission"),
            };

        // The clients that will use the APIs are defined.
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("BankA")
                {
                    Scopes =
                    {
                        "BankA.Write",
                        "BankA.Read"
                    }
                },
                new ApiResource("BankB")
                {
                    Scopes =
                    {
                        "BankB.Write",
                        "BankB.Read"
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //new Client
                //{
                //    ClientId          = "BankA",
                //    ClientName        = "BankA",
                //    ClientSecrets     = { new Secret("banka".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    AllowedScopes     = { "BankA.Write", "BankA.Read" }
                //},
                //new Client
                //{
                //    ClientId          = "BankB",
                //    ClientName        = "BankB",
                //    ClientSecrets     = { new Secret("bankb".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    AllowedScopes     = { "BankB.Write", "BankB.Read" }
                //},
                new Client
                {
                    ClientId          = "BankManager",
                    ClientName        = "BankManager",
                    ClientSecrets     = { new Secret("bankmanager".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowedScopes     = 
                    { 
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
                        "BankA.Write", 
                        "BankA.Read",
                        "BankB.Write",
                        "BankB.Read"
                    },
                    RedirectUris           = { "https://localhost:4000/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:4000/signout-callback-oidc" },
                    RequirePkce            = false,
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "murat",
                    Username  = "murat",
                    Password  = "m1",
                    Claims    = {
                        new Claim("name"   , "murat"),
                        new Claim("website", "https://wwww.example.com")
                    }
                }
            };
    }
}
