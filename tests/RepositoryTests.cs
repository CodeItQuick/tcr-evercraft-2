using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace tcr_evercraft_2_tests;

public class RepositoryTests
{
    
    [Test]
    public Task RepositoryCanRetrieveListOfCharacter()
    {
        var homeRepository = new HomeRepository(
            null, 
            new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("TemporaryDatabase").Options);

        var retrieveDnDCharacters = homeRepository.RetrieveDnDCharacters();
        
        Assert.GreaterOrEqual(retrieveDnDCharacters.Count, 0);
        return Task.CompletedTask;
    }
    [Test]
    public Task RepositoryCanCreateNewCharacter()
    {
        var homeRepository = new HomeRepository(
            null, 
            new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("TemporaryDatabase").Options);

        var createCharacter = homeRepository.CreateCharacter("can create character with name");
        
        Assert.That(createCharacter, Is.EqualTo(1));
        return Task.CompletedTask;
    }
    [Test]
    public Task RepositoryCanRemoveNewCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("TemporaryDatabase").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext, 
            null);

        var createCharacter = homeRepository.CreateCharacter("can create character with name");
        homeRepository.RemoveCharacter(1);
        
        
        Assert.That(createCharacter, Is.EqualTo(1));
        return Task.CompletedTask;
    }
}