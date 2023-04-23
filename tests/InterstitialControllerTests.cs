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
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("RetrieveExistingCharacters").Options;
        var homeRepository = new HomeRepository(new EvercraftDbContext(dbContextOptions));
        var homeController = new HomeController(homeRepository);

        var viewResult = homeController.Home() as ViewResult;
        var viewResultModel = viewResult?.Model as HomeModel;
        
        Assert.IsNotNull(viewResult);
        Assert.GreaterOrEqual(viewResultModel?.DnDCharacters?.Count, 0);
    }
    [Test]
    public async Task CanCreateNewCharacterFromIndexes()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("Create New Character Interstitial").Options;
        var homeRepository = new HomeRepository(new EvercraftDbContext(dbContextOptions));
        var homeController = new HomeController(homeRepository);

        homeController.Create("create character test");
        var viewResult = homeController.Home() as ViewResult;
        var viewResultModel = viewResult?.Model as HomeModel;

        Assert.IsNotNull(viewResult);
        Assert.That(viewResultModel?.DnDCharacters?.Count, Is.EqualTo(1));
        Assert.That(viewResultModel?.DnDCharacters?.Last().CharacterName, Is.EqualTo("create character test"));
    }
    [Test]
    public async Task CanRemoveNewCharacterFromIndexes()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("Remove New Character Interstitial").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(evercraftDbContext);
        var homeController = new HomeController(homeRepository);
        homeController.Create("remove character test");
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(1));
        
        homeController.Delete(1);

        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(0));
    }
}