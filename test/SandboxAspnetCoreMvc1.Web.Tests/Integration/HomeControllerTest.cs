using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc1.Web.Tests.Integration {

[TestClass]
public class HomeControllerTest : TestIntegrationBase<Startup>
{
    [TestMethod]
    public async Task Index_works()
    {
        // Arrange.
        var requestUrl = "/";

        // Act.
        var response = await _client.GetAsync(requestUrl);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert.
        Assert.IsTrue(response.IsSuccessStatusCode);
        Assert.IsFalse(String.IsNullOrEmpty(responseString));
    }

    [TestMethod]
    public async Task Blog_works()
    {
        // Arrange.
        var requestUrl = "/Home/Blog";

        // Act.
        var response = await _client.GetAsync(requestUrl);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert.
        Assert.IsTrue(response.IsSuccessStatusCode);
        Assert.IsFalse(String.IsNullOrEmpty(responseString));
    }
}

}
