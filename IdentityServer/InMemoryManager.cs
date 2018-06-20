using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace SecureApi.OAuth
{
    public class InMemoryManager
    {
        public List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "1",
                    Username = "aaron@rminc.com",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Aaron"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Salazar"),
                        new Claim(Constants.ClaimTypes.Email, "aaron@rminc.com")
                    }
                }
            };
        }

        public IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "secureapi",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "SecureApi",
                    Flow = Flows.Implicit,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        "read"
                    },
                    Enabled = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:57981/popup.html/"
                    },

                    // This allows IdentityServer to only accept browser-based requests from
                    // registered URLs.
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:57981"
                    },

                    // TODO AS: Narrow the scope
                    // The client has access to all scopes. For production applications we need to
                    // narrow this down to only the scopes it’s expected to access with the
                    // AllowedScopes property.
                    AllowAccessToAllScopes = true
                }
            };
        }
    }
}