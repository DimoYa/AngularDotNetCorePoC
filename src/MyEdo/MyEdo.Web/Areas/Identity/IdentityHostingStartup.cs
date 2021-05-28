using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MyEdo.Areas.Identity.IdentityHostingStartup))]
namespace MyEdo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}