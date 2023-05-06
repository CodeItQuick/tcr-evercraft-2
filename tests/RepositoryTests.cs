using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace tcr_evercraft_2_tests;

public class RepositoryTests
{

    [Test]
    public void RepositoryCanRetrieveListOfCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanRetrieveCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);

        var retrieveDnDCharacters = homeRepository.RetrieveDnDCharacters();
        
        Assert.GreaterOrEqual(retrieveDnDCharacters.Count, 0);
    }
    [Test]
    public void RepositoryCanCreateNewCharacters()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanAddCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);

        var createCharacter = homeRepository.CreateCharacter("can create character with name");
        
        Assert.That(createCharacter, Is.EqualTo(1));
    }
    [Test]
    public void RepositoryCanRemoveNewCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanRemoveCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        var createCharacter = homeRepository.CreateCharacter("can remove character with name");
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(1));

        homeRepository.RemoveCharacter(1);
        
        Assert.That(createCharacter, Is.EqualTo(1));
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(0));
    }
    [Test]
    public void RepositoryCannotRemoveNonPresentCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanRemoveCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.RemoveCharacter(1);
        
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(0));
    }

    [Test]
    public void RepositoryCanRenameCurrentCharacter()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("Can Edit Character").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        var createCharacter = homeRepository.CreateCharacter("can create character with name");
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(1));

        homeRepository.EditCharacter(1, "edited name");
        
        Assert.That(evercraftDbContext.DnDCharacters.First().CharacterName, Is.EqualTo("edited name"));
    }
    [Test]
    public void RepositoryCanCreateNewCharacterWithHitPoints()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanAddCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);

        homeRepository.CreateCharacter("can create character with 10 hitpoints");
        
        Assert.That(evercraftDbContext.DnDCharacters.First().HitPoints, Is.EqualTo(5));
    }
    [Test]
    public void RepositoryCanCreateNewCharacterWithArmor()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanAddCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);

        homeRepository.CreateCharacter("can create character with 10 hitpoints");
        
        Assert.That(evercraftDbContext.DnDCharacters.First().Armor, Is.EqualTo(10));
    }
    [Test]
    public void RepositoryCanAttackNewCharactersAndHitThemWithElevenRoll()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanAttackCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("can attack character");

        homeRepository.AttackCharacter(1, 11);
        
        Assert.That(evercraftDbContext.DnDCharacters.First().HitPoints, Is.EqualTo(4));
    }
    [Test]
    public void RepositoryCanAttackNewCharactersAndMissThemWithNineRolls()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanAttackAnotherCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("can attack character");

        homeRepository.AttackCharacter(1, 9);
        
        Assert.That(evercraftDbContext.DnDCharacters.First().HitPoints, Is.EqualTo(5));
    }
    [Test]
    public void RepositoryCanAttackNewCharactersAndMissThemWithTenRoll()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanAttackAnotherCharacterAtZero").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("can attack character");

        homeRepository.AttackCharacter(1, 10);
        
        Assert.That(evercraftDbContext.DnDCharacters.First().HitPoints, Is.EqualTo(5));
    }
    [Test]
    public void RepositoryCanAttackNewCharactersAndAtOneHitpointDies()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanKillCharacter").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("can attack character");
        homeRepository.AttackCharacter(1, 11);
        homeRepository.AttackCharacter(1, 11);
        homeRepository.AttackCharacter(1, 11);
        homeRepository.AttackCharacter(1, 11);
        
        homeRepository.AttackCharacter(1, 11);
        
        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(0));
    }
    [Test]
    public void RepositoryCanAttackNewCharactersAndOnCriticalHit()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanAttackAnotherCharacterAtZero").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("can attack character");

        homeRepository.AttackCharacter(1, 20);
        
        Assert.That(evercraftDbContext.DnDCharacters.First().HitPoints, Is.EqualTo(4));
    }
}