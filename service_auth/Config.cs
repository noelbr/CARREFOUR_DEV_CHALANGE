using Duende.IdentityServer.Models;

namespace service_auth;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("accountapi"),
            new ApiScope("scope2"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "accountapi" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7208/signin-oidc", "https://localhost:7208/swagger/oauth2-redirect.html" },
                FrontChannelLogoutUri = "https://localhost:7208/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7208/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            new Client
                {
                    ClientId = "swagger-local",
                    ClientName = "Cliente de test para o swagger local",

                    RedirectUris = {
                        "https://localhost:7208/swagger/oauth2-redirect.html",
                         "http://localhost:8080/swagger/oauth2-redirect.html"

                    },

                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    RequireConsent = true,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    AllowedCorsOrigins =  new[]
                {
                    "https://localhost:7208",
                    "http://localhost:8080"
                },
                    AllowedScopes =  { "accountapi" },
                } ,
        };
}
