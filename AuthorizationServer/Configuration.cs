using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizationServer
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("EventsWebApp", "Web App")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("EventsWebApp", "Web App", [JwtClaimTypes.Name])
                {
                    Scopes = { "EventsWebApp" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "events-web-app",
                    ClientName = "Events Web App",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "https://localhost:7054/swagger/oauth2-redirect.html"
                    },
                    AllowedCorsOrigins =
                    {
                        "https://localhost:7054"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://.../signin-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "EventsWebApp"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}