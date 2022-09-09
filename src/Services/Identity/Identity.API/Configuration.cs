using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MRA.Identity
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("my_scope"),
                new ApiScope("ApiScope", "Api Scope"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("MRAGateway", "MRA.GATEWAY",
                    new [] { JwtClaimTypes.Name })
                {
                    Scopes =
                    {
                        "ApiScope"
                    }
                },
                new ApiResource("MRABookings", "MRA.BOOKINGS",
                    new [] { JwtClaimTypes.Name })
                {
                    Scopes =
                    {
                        "ApiScope"
                    }
                },
                new ApiResource("MRARooms", "MRA.ROOMS",
                    new [] { JwtClaimTypes.Name })
                {
                    Scopes =
                    {
                        "ApiScope"
                    }
                },
                new ApiResource("MRAUsers", "MRA.USERS",
                    new [] { JwtClaimTypes.Name })
                {
                    Scopes =
                    {
                        "ApiScope"
                    }
                },
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
            };

        public static IEnumerable<Client> clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "oidcMVCApp",
                    ClientName = "MRA.Mvc",
                    ClientSecrets = new List<Secret> {new Secret("MvcSecret".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> {"https://localhost:44393/signin-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "my_scope"
                    },
                    RequirePkce = true,
                    AllowPlainTextPkce = false
                },
                new Client
                {
                    ClientId = "MRAAngular",
                    ClientName = "MRA.Angular",
                    ClientSecrets = new List<Secret> {new Secret("AngularSecret".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> {"http://localhost:4200/"},
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "my_scope",
                        "MRAApi"
                    },
                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                    AllowedCorsOrigins = {"http://localhost:4200"},
                },
            };
    }
}
