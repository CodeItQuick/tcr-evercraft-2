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