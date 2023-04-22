
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace tcr_evercraft_2_tests;

public class Tests
{
    private HttpClient _client;
    [SetUp]
    public void SetUp()
    {
        var testingWebAppFactory = new TestingWebAppFactory<Program>();
        _client = testingWebAppFactory.CreateClient();
    }

    [Test]
    public async Task HomeIndexPopulatesPage()
    {
        var response = await _client.GetAsync($"/Home/Index");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task MainIndexPopulatesPage()
    {
        var response = await _client.GetAsync($"/Index");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task CreateIndexPopulatesIdxPage()
    {
        var response = await _client.GetAsync($"/Home/Create/1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task EditIndexPopulatesPage()
    {
        var response = await _client.GetAsync($"/Home/Edit/1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task DeleteIndexPopulatesPage()
    {
        var response = await _client.GetAsync($"/Home/Delete/1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}