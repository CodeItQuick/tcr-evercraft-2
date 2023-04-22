
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace tcr_evercraft_2_tests;

public class MainPageSmokeTests
{
    private HttpClient _client;
    [SetUp]
    public void SetUp()
    {
        var testingWebAppFactory = new TestingWebAppFactory<Program>();
        _client = testingWebAppFactory.CreateClient();
    }

    [Test]
    public async Task MainPageIndexSmokeTest()
    {
        var response = await _client.GetAsync($"/Index");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task HomeControllerIndexSmokeTest()
    {
        var response = await _client.GetAsync($"/Home/Index");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}