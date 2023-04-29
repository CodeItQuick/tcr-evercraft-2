using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace tcr_evercraft_2_tests;

public class RepositoryTests
{
    
    [Test]
    public Task RepositoryCanRetrieveListOfCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanRetrieveCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);

        var retrieveDnDCharacters = homeRepository.RetrieveDnDCharacters();
        
        Assert.GreaterOrEqual(retrieveDnDCharacters.Count, 0);
        return Task.CompletedTask;
    }
    [Test]
    public Task RepositoryCanCreateNewCharacters()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanAddCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);

        var createCharacter = homeRepository.CreateCharacter("can create character with name");
        
        Assert.That(createCharacter, Is.EqualTo(1));
        return Task.CompletedTask;
    }
    [Test]
    public Task RepositoryCanRemoveNewCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanRemoveCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        var createCharacter = homeRepository.CreateCharacter("can create character with name");
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(1));

        homeRepository.RemoveCharacter(1);
        
        Assert.That(createCharacter, Is.EqualTo(1));
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(0));
        return Task.CompletedTask;
    }
    [Test]
    public Task RepositoryCannotRemoveNonPresentCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanRemoveCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.RemoveCharacter(1);
        
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(0));
        return Task.CompletedTask;
    }

    [Test]
    public Task RepositoryCanRenameCurrentCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("Can Edit Character").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        var createCharacter = homeRepository.CreateCharacter("can create character with name");
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(1));

        homeRepository.EditCharacter(1, "edited name");
        
        Assert.Equals(evercraftDbContext.DnDCharacters.First().CharacterName, "edited name");
        return Task.CompletedTask;
    }
}