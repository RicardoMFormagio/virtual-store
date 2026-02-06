using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShop.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("vshop", "VShop Server"),
            new ApiScope("read", "Read Data"),
            new ApiScope("write", "Write Data"),
            new ApiScope("delete", "Delete Data"),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            //cliente genérico
            new Client
            {
                ClientId = "cliente",
                ClientSecrets = { new Secret("abracadabra#simsalabim".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials, //precisa das credenciais do usuário
                AllowedScopes = { "read", "write", "profile" }
            },

            new Client
            {
                ClientId = "vshop",
                ClientSecrets = { new Secret("abracadabra#simsalabim".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code, //via código
                RedirectUris = { "https://localhost:7165/signin-oidc" }, //login
                PostLogoutRedirectUris = { "https://localhost:7165/signout-callback-oidc" }, //login
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                }
            }

        };
}