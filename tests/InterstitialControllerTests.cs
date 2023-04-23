using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using EvercraftWebsite.Views.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace tcr_evercraft_2_tests;

public class InterstitialControllerTests
{
    
    [Test]
    public async Task CanRetrieveViewFromIndex()
    {
        var homeController = new HomeController(
            null, 
            null,
            new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("TemporaryDatabase").Options);

        var viewResult = homeController.Home() as ViewResult;
        var viewResultModel = viewResult?.Model as HomeModel;
        
        Assert.IsNotNull(viewResult);
        Assert.GreaterOrEqual(viewResultModel?.DnDCharacters?.Count, 0);
    }
    [Test]
    public async Task CanRetrieveNewCharacterFromIndexes()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("TemporaryDatabase").Options;
        var homeController = new HomeController(
            null, 
            new HomeRepository(new EvercraftDbContext(dbContextOptions)),
            dbContextOptions);

        homeController.Create("create character test");
        var viewResult = homeController.Home() as ViewResult;
        var viewResultModel = viewResult?.Model as HomeModel;

        Assert.IsNotNull(viewResult);
        Assert.That(viewResultModel?.DnDCharacters?.Count, Is.EqualTo(1));
        Assert.That(viewResultModel?.DnDCharacters?.Last().CharacterName, Is.EqualTo("create character test"));
    }
}