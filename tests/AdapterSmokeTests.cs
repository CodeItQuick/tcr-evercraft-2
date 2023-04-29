
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
    private static readonly TestingWebAppFactory Application = new();

    private static HttpClient Client =>
        Application.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });

    [Test]
    public async Task HomeIndexPopulatesPage()
    {
        var response = await Client.GetAsync($"/Home/Home");

        response.EnsureSuccessStatusCode();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task HomeIndexPostRequestPopulatesPage()
    {
        var response = await Client.PostAsync($"/Home/Create", new FormUrlEncodedContent(
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
        var response = await Client.PostAsync($"/Home/Create", new FormUrlEncodedContent(
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
        var response = await Client.GetAsync($"/Index");

        response.EnsureSuccessStatusCode();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task CreateIndexPopulatesIndexPage()
    {
        var response = await Client.PostAsync($"/Home/Create", 
            new FormUrlEncodedContent(new []{ new KeyValuePair<string, string>() })
            );

        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task EditIndexPopulatesIndexPage()
    {
        var response = await Client.GetAsync($"/Home/Edit/1");

        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task DeleteIndexPopulatesIndexPage()
    {
        var response = await Client.GetAsync($"/Home/Delete/1");

        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task CharacterAttackedPopulatesIndexPage()
    {
        var response = await Client.PostAsync($"/Home/CharacterAttacked/1", null);
        
        var redirectLocation = response!.Headers.Location;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Found));
        Assert.That(redirectLocation!.OriginalString, Is.EqualTo("/Home/Home"));
    }
    [Test]
    public async Task HomePagePopulates()
    {
        var response = await Client.GetAsync("/");

        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        
        StringAssert.Contains("Evercraft", stringResponse);
    }
    [Test]
    public async Task MainCharacterPagePopulates()
    {
        var response = await Client.GetAsync("/Home/Home");

        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        
        StringAssert.Contains("Evercraft", stringResponse);
    }
}