
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using EvercraftWebsite.Views.Home;
using Microsoft.EntityFrameworkCore;
namespace tcr_evercraft_2_tests;

public class AdapterSmokeTests
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
        var response = await _client.GetAsync($"/Home/Home");

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
        var queryKeys = response.RequestMessage?.RequestUri?.Query;
        Assert.That(queryKeys, Is.EqualTo("?characterName=defaultCharacterName"));
    }
    [Test]
    public async Task HomeIndexPostWithCharacterNameRequestPopulatesPage()
    {
        var response = await _client.PostAsync($"/Home/Create", new FormUrlEncodedContent(
            new List<KeyValuePair<string, string>>()
            {
                new("characterName", "HelloWorld")
            }));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var queryKeys = response.RequestMessage?.RequestUri?.Query;
        Assert.That(queryKeys, Is.EqualTo("?characterName=HelloWorld"));
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
        var response = await _client.PostAsync($"/Home/Create", 
            new FormUrlEncodedContent(new []{ new KeyValuePair<string, string>() })
            );

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
    [Test]
    public async Task CanRetrieveViewFromIndex()
    {
        var homeController = new HomeController(null, new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("TemporaryDatabase").Options);

        var viewResult = homeController.Home() as ViewResult;
        
        Assert.IsNotNull(viewResult);
    }
    [Test]
    public async Task CanRetrieveNewCharacterFromIndexes()
    {
        var homeController = new HomeController(null, new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("TemporaryDatabase").Options);

        homeController.Create("create character test");
        var viewResult = homeController.Home() as ViewResult;
        var viewResultModel = viewResult?.Model as HomeModel;

        Assert.IsNotNull(viewResult);
        Assert.AreEqual(1, viewResultModel?.DnDCharacters?.Count);
        Assert.AreEqual("create character test", viewResultModel?.DnDCharacters?.Last().CharacterName);
    }
}