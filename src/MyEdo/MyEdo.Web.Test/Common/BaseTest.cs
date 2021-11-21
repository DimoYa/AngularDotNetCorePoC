namespace MyEdo.Web.Test.Common
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class BaseTest
    {
        [SetUp]
        public void ApiSetUp()
        {
            this.Server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Integration")
                .UseStartup<Startup>());

            this.Client = this.Server.CreateClient();
            this.Client.DefaultRequestHeaders.Add("ContentType", "application/json");
        }

        public TestServer Server { get; set; }

        public HttpClient Client { get; set; }
    }
}