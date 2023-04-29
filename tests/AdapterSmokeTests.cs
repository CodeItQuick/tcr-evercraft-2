
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using EvercraftWebsite.Views.Home;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
namespace tcr_evercraft_2_tests;

public class AdapterSmokeTests
{
    private HttpClient _client;
    [SetUp]
    public void SetUp()
    {
        var testingWebAppFactory = new TestingWebAppFactory();
        _client = testingWebAppFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
    }

    [Test]
    public async Task HomeIndexPopulatesPage()
    {
        var response = await _client.GetAsync($"/Home/Home");

        response.EnsureSuccessStatusCode();
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

        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task HomeIndexPostWithCharacterNameRequestPopulatesPage()
    {
        var response = await _client.PostAsync($"/Home/Create", new FormUrlEncodedContent(
            new List<KeyValuePair<string, string>>()
            {
                new("characterName", "HelloWorld")
            }));

        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task MainIndexPopulatesPage()
    {
        var response = await _client.GetAsync($"/Index");

        response.EnsureSuccessStatusCode();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task CreateIndexPopulatesIndexPage()
    {
        var response = await _client.PostAsync($"/Home/Create", 
            new FormUrlEncodedContent(new []{ new KeyValuePair<string, string>() })
            );

        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task EditIndexPopulatesIndexPage()
    {
        var response = await _client.GetAsync($"/Home/Edit/1");

        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task DeleteIndexPopulatesIndexPage()
    {
        var response = await _client.GetAsync($"/Home/Delete/1");

        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task CharacterAttackedPopulatesIndexPage()
    {
        var response = await _client.PostAsync($"/Home/CharacterAttacked/1", null);
        
        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
}