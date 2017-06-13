using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreWebApi.Service.Tests.Integration {

[TestClass]
public class EchosControllerTest : TestIntegrationBase<Startup>
{
    [TestMethod]
    public async Task Get_many()
    {
        // Arrange.
        var requestUrl = "/api/echos";

        // Act.
        var response = await _client.GetAsync(requestUrl);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert.
        Assert.IsTrue(response.IsSuccessStatusCode);
        Assert.IsFalse(String.IsNullOrEmpty(responseString));
    }

    [TestMethod]
    public async Task Get_one()
    {
        // Arrange.
        var requestUrl = "/api/echos/2";

        // Act.
        var response = await _client.GetAsync(requestUrl);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert.
        Assert.IsTrue(response.IsSuccessStatusCode);
        Assert.IsFalse(String.IsNullOrEmpty(responseString));
    }

    [TestMethod]
    public async Task Create_one()
    {
        // Arrange.
        var requestUrl = "/api/echos";
        var requestBody = new System.Net.Http.FormUrlEncodedContent(new System.Collections.Generic.Dictionary<string, string> {
            {"message", "Hello World"}
        });
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

        // Act.
        var response = await _client.PostAsync(requestUrl, requestBody);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert.
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task Update_one()
    {
        // Arrange.
        var requestUrl = "/api/echos/2";
        var requestBody = new System.Net.Http.StringContent("\"Hello World\"", System.Text.Encoding.UTF8, "application/json");

        // Act.
        var response = await _client.PutAsync(requestUrl, requestBody);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert.
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task Delete_one()
    {
        // Arrange.
        var requestUrl = "/api/echos/2";

        // Act.
        var response = await _client.DeleteAsync(requestUrl);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert.
        Assert.IsTrue(response.IsSuccessStatusCode);
    }
}

}
