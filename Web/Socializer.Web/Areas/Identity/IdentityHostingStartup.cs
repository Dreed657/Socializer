using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Socializer.Web.Areas.Identity.IdentityHostingStartup))]

namespace Socializer.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
