using IdentityServer4.Models;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace AuthServer
{
    public static class Config
    {
        //The permissions that will use on the APIs are defined.
        public static IEnumerable<ApiScope> GetApiScopes =>
            new List<ApiScope> 
            { 
                new ApiScope("BankA.Write", "BankA write permission"),
                new ApiScope("BankA.Read" , "BankA read permission"),
                new ApiScope("BankB.Write", "BankA write permission"),
                new ApiScope("BankB.Read" , "BankA read permission"),
            };

        // The clients that will use the APIs are defined.
        public static IEnumerable<ApiResource> GetApiResources =>
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

        public static IEnumerable<Client> GetClients =>
            new List<Client>
            {
                new Client
                {
                    ClientId          = "BankA",
                    ClientName        = "BankA",
                    ClientSecrets     = { new Secret("banka".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes     = { "BankA.Write", "BankA.Read" }
                },
                new Client
                {
                    ClientId          = "BankB",
                    ClientName        = "BankB",
                    ClientSecrets     = { new Secret("bankb".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes     = { "BankB.Write", "BankB.Read" }
                }
            };
    }
}
