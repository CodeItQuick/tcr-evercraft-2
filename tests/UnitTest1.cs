
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
    public async Task HomeIndexPostRequestPopulatesPage()
    {
        var response = await _client.PostAsync($"/Home/Create", new FormUrlEncodedContent(
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>() {  }
            }));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.RequestMessage.RequestUri.Query, Is.EqualTo("?Key=characterName"));
    }
    [Test]
    public async Task HomeIndexPostWithCharacterNameRequestPopulatesPage()
    {
        var response = await _client.PostAsync($"/Home/Index", new FormUrlEncodedContent(
            new List<KeyValuePair<string, string>>()
            {
                new("characterName", "Hello World")
            }));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task MainIndexPopulatesPage()
    {
        var response = await _client.GetAsync($"/Index");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task CreateIndexPopulatesIndexPage()
    {
        var response = await _client.GetAsync($"/Home/Create/1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task EditIndexPopulatesIndexPage()
    {
        var response = await _client.GetAsync($"/Home/Edit/1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task DeleteIndexPopulatesIndexPage()
    {
        var response = await _client.GetAsync($"/Home/Delete/1");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}