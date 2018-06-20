using System;
using System.Threading.Tasks;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.IO;

[assembly: OwinStartup(typeof(SecureApi.OAuth.Startup))]

namespace SecureApi.OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var inMemoryManager = new InMemoryManager();

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(inMemoryManager.GetUsers())
                .UseInMemoryScopes(inMemoryManager.GetScopes())
                .UseInMemoryClients(inMemoryManager.GetClients());

            var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);

            var options = new IdentityServerOptions
            {
                SiteName = "RMI Identity Server",
                SigningCertificate = new X509Certificate2(certificate, ConfigurationManager.AppSettings["SigningCertificatePassword"]),
                RequireSsl = false, // Don't do this in production
                Factory = factory
            };

            app.UseIdentityServer(options);
        }
    }
}
