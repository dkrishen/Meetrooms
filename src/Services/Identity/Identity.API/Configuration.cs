using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace MRA.Identity
{
    public class Configuration
    {
        public IConfiguration AppSettings { get; }

        public Configuration(IConfiguration appSettings)
        {
            AppSettings = appSettings;
        }

        public IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("my_scope"),
                new ApiScope("ApiScope", "Api Scope"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public IEnumerable<ApiResource> ApiResources =>
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

        public IEnumerable<Client> clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "MRAAngular",
                    ClientName = "MRA.Angular",
                    ClientSecrets = new List<Secret> {new Secret("AngularSecret".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> {AppSettings.GetValue<string>("AngularUrl") + "/" },
                    PostLogoutRedirectUris = new List<string> { AppSettings.GetValue<string>("AngularUrl") + "/" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "my_scope",
                        "ApiScope"
                    },
                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                    AllowedCorsOrigins = { AppSettings.GetValue<string>("AngularUrl")},
                }
            };
    }
}
