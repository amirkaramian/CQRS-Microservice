using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Payscrow.Identity.Api.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api
{
    internal static class Clients
    {
        public static IEnumerable<Client> Get(Dictionary<string, string> clientsUrl)
        {
            return new List<Client>
            {
                //new Client
                //{
                //    ClientId = "oauthClient",
                //    ClientName = "Payment Invite Microservice",
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets = new List<Secret> {new Secret(CustomIdentityServerConstants.IDENTITY_SERVER_SECRET.Sha256())}, // change me!
                //    AllowedScopes = new List<string> { "payment_invite.read", "payment_invite.write" }
                //},
                //new Client
                //{
                //    ClientId = "oidcClient",
                //    ClientName = "Example Client Application",
                //    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())}, // change me!

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RedirectUris = new List<string> {"https://localhost:5002/signin-oidc"},
                //    AllowedScopes = new List<string>
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        "role",
                //        "api1.read"
                //    },

                //    RequirePkce = true,
                //    AllowPlainTextPkce = false
                //},
                new Client
                {
                    ClientId = "Postman_api",
                    ClientName = "Postman Test Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireConsent = false,
                    RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
                    PostLogoutRedirectUris = { "https://www.getpostman.com" },
                    AllowedCorsOrigins = { "https://www.getpostman.com" },
                    ClientSecrets = new List<Secret> {new Secret(CustomIdentityServerConstants.IDENTITY_SERVER_SECRET.Sha256())},
                    //ClientSecrets = new List<Secret>{ new Secret("secret")},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        CustomIdentityServerConstants.Scopes.ROLE,
                        "given_name",
                        "market_place"
                    },
                },
                new Client
                {
                    ClientId = "webUI",
                    ClientName = "Web UI",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientUri = $"{clientsUrl["WebUI"]}",                             // public uri of the client
                    //AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    RequirePkce = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                    {
                        $"{clientsUrl["WebUI"]}/signin-oidc"
                    },
                    FrontChannelLogoutUri = $"{clientsUrl["WebUI"]}/signout-oidc",
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{clientsUrl["WebUI"]}/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "payment_invite",
                        "escrow",
                        "market_place"
                    },
                    AccessTokenLifetime = 60*60, // 2 hours
                    IdentityTokenLifetime= 60*60 // 2 hours
                },
            };
        }
    }

    internal static class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone(),
                new IdentityResource
                {
                    Name = "roles",
                    UserClaims = new List<string> {"role"}
                },
                new IdentityResource{
                    Name = "given_name",
                    DisplayName = "Given Name",
                    UserClaims = new List<string>{ "given_name" }
                },
                new IdentityResource
                {
                    Name = "last_name",
                    DisplayName = "Last Name",
                    UserClaims = new List<string>{ "last_name" }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource {
                    Name = "payment_invite",
                    DisplayName = "Payment Invite Service",
                    //Scopes = new List<string>{ "api.read", "api.write"},
                    //ApiSecrets = new List<Secret>{ new Secret(CustomIdentityServerConstants.IDENTITY_SERVER_SECRET.Sha256()) }
                },
                new ApiResource {
                    Name = "escrow",
                    DisplayName = "Escrow Service",
                    //Scopes = new List<string>{ "api.read", "api.write"},
                    //ApiSecrets = new List<Secret>{ new Secret(CustomIdentityServerConstants.IDENTITY_SERVER_SECRET.Sha256()) }
                },
                new ApiResource {
                    Name = "project_milestone",
                    DisplayName = "Project Milestone Service",
                    //Scopes = new List<string>{ "api.read", "api.write"},
                    //ApiSecrets = new List<Secret>{ new Secret(CustomIdentityServerConstants.IDENTITY_SERVER_SECRET.Sha256()) }
                },
                new ApiResource
                {
                    Name = "market_place",
                    DisplayName = "Market Place",
                    //Scopes = new List<string>{ "api.read", "api.write"},
                    //ApiSecrets = new List<Secret>{ new Secret(CustomIdentityServerConstants.IDENTITY_SERVER_SECRET.Sha256()) }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("project.milestone", "Project Milestone Service"),
                new ApiScope("api.read", "Read Access to API Service"),
                new ApiScope("api.write", "Write Access to API Service")
            };
        }
    }

    internal static class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "scott@scottbrady91.com",
                    Password = "Swordfish1#",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "scott@scottbrady91.com"),
                        new Claim(JwtClaimTypes.GivenName, "Ebimie"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}