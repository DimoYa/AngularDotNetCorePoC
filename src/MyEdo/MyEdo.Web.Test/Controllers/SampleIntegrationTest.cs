using MyEdo.Web.Test.Common;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyEdo.Web.Test
{
    public class SampleIntegrationTest : BaseTest
    {
        [Test]
        [Category("IntegrationTest")]
        public async Task ApiUnauthorizedRequestToHomePage_ShouldReturnOK()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/Training");
            var response = await this.Client.SendAsync(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);
        }
    }
}