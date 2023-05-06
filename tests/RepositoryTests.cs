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

        homeRepository.EditCharacterName(1, "edited name");
        
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
            .UseInMemoryDatabase("CanCriticalHit").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("can attack character");

        homeRepository.AttackCharacter(1, 20);

        Assert.That(evercraftDbContext.DnDCharacters.First().HitPoints, Is.EqualTo(3));
    }
    [Test]
    public void RepositoryCanAttackNewCharactersAndOnCriticalHitCharacterDies()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanCriticalHitCharacterDies").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("can attack character");
        homeRepository.AttackCharacter(1, 19);
        homeRepository.AttackCharacter(1, 19);
        homeRepository.AttackCharacter(1, 19);

        homeRepository.AttackCharacter(1, 20);

        Assert.That(evercraftDbContext.DnDCharacters.Count(), Is.EqualTo(0));
    }
    [Test]
    public void RepositoryNewCharacterHasNeutralAlignment()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasAlignment").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.CreateCharacter("aligned character");

        Assert.That(evercraftDbContext.DnDCharacters.First().Alignment, 
            Is.EqualTo(CharacterAlignment.Neutral));
    }
    [Test]
    public void RepositoryCanAttackNewCharacterCanSetToEvilAlignment()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CanCriticalHitCharacterHasEvilAlignment").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("aligned character");
        
        homeRepository.EditCharacterAlignment(1, CharacterAlignment.Evil);

        Assert.That(evercraftDbContext.DnDCharacters.First().Alignment, 
            Is.EqualTo(CharacterAlignment.Evil));
    }
    [Test]
    public void RepositoryNewCharacterHasStrengthModifier()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasStrengthModifier").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.CreateCharacter("strength character");

        Assert.That(evercraftDbContext.DnDCharacters.First().StrengthModifier, 
            Is.EqualTo(CharacterModifier.Ten));
        Assert.That((int) evercraftDbContext.DnDCharacters.First().StrengthModifier, 
            Is.EqualTo(10));
    }
    [Test]
    public void RepositoryNewCharacterHasDexterityModifier()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasDexterityModifier").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.CreateCharacter("Dexterity character");

        Assert.That(evercraftDbContext.DnDCharacters.First().DexterityModifier, 
            Is.EqualTo(CharacterModifier.Ten));
    }
    [Test]
    public void RepositoryNewCharacterHasConstitutionModifier()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasConstitutionModifier").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.CreateCharacter("Constitution character");

        Assert.That(evercraftDbContext.DnDCharacters.First().ConstitutionModifier, 
            Is.EqualTo(CharacterModifier.Ten));
    }
    [Test]
    public void RepositoryNewCharacterHasWisdomModifier()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasWisdomModifier").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.CreateCharacter("Wisdom character");

        Assert.That(evercraftDbContext.DnDCharacters.First().WisdomModifier, 
            Is.EqualTo(CharacterModifier.Ten));
    }
    [Test]
    public void RepositoryNewCharacterHasIntelligenceModifier()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasIntelligenceModifier").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.CreateCharacter("Intelligence character");

        Assert.That(evercraftDbContext.DnDCharacters.First().IntelligenceModifier, 
            Is.EqualTo(CharacterModifier.Ten));
    }
    [Test]
    public void RepositoryNewCharacterHasCharismaModifier()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasCharismaModifier").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        
        homeRepository.CreateCharacter("Charisma character");

        Assert.That(evercraftDbContext.DnDCharacters.First().CharismaModifier, 
            Is.EqualTo(CharacterModifier.Ten));
    }
    [Test]
    public void RepositoryNewCharacterCanSetModifier()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasCharismaModifiers").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("Charisma character");

        homeRepository.SetModifier(1, 10, "Charisma"); 
        
        Assert.That(evercraftDbContext.DnDCharacters.First().CharismaModifier, 
            Is.EqualTo(CharacterModifier.Ten));
    }
    [Test]
    public void RepositoryNewCharacterCanSetModifierToThirteen()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasCharismaModifiers").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("Charisma character");

        homeRepository.SetModifier(1, 13, "Charisma"); 
        
        Assert.That(evercraftDbContext.DnDCharacters.First().CharismaModifier, 
            Is.EqualTo(CharacterModifier.Thirteen));
    }
    [Test]
    public void RepositoryNewCharacterCanSetModifierStrengthToThirteen()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasCharismaModifierStrength").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("Strength character");

        homeRepository.SetModifier(1, 13, "Strength"); 
        
        Assert.That(evercraftDbContext.DnDCharacters.First().StrengthModifier, 
            Is.EqualTo(CharacterModifier.Thirteen));
    }
    [Test]
    public void RepositoryNewCharacterCanSetModifierWisdomToThirteen()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasCharismaModifierWisdom").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("Wisdom character");

        homeRepository.SetModifier(1, 13, "Wisdom"); 
        
        Assert.That(evercraftDbContext.DnDCharacters.First().WisdomModifier, 
            Is.EqualTo(CharacterModifier.Thirteen));
    }
    [Test]
    public void RepositoryNewCharacterCanSetModifierIntelligenceToThirteen()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasCharismaModifierIntelligence").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("Intelligence character");

        homeRepository.SetModifier(1, 13, "Intelligence"); 
        
        Assert.That(evercraftDbContext.DnDCharacters.First().IntelligenceModifier, 
            Is.EqualTo(CharacterModifier.Thirteen));
    }
    [Test]
    public void RepositoryNewCharacterCanSetModifierConstitutionToThirteen()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasCharismaModifierConstitution").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("Constitution character");

        homeRepository.SetModifier(1, 13, "Constitution"); 
        
        Assert.That(evercraftDbContext.DnDCharacters.First().ConstitutionModifier, 
            Is.EqualTo(CharacterModifier.Thirteen));
    }
    [Test]
    public void RepositoryNewCharacterCanSetModifierDexterityToThirteen()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("CharacterHasCharismaModifierDexterity").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("Dexterity character");

        homeRepository.SetModifier(1, 13, "Dexterity"); 
        
        Assert.That(evercraftDbContext.DnDCharacters.First().DexterityModifier, 
            Is.EqualTo(CharacterModifier.Thirteen));
    }
    [Test]
    public void RepositoryCanAttackNewCharactersAndOnModifierChangesHitStrength()
    { 
        var dbContextOptions = new DbContextOptionsBuilder<EvercraftDbContext>()
            .UseInMemoryDatabase("ModifierChangesHitStrength").Options;
        var evercraftDbContext = new EvercraftDbContext(dbContextOptions);
        var homeRepository = new HomeRepository(
            evercraftDbContext);
        homeRepository.CreateCharacter("can attack character");
        homeRepository.SetModifier(1, 10, "Strength");

        homeRepository.AttackCharacter(1, 20);

        Assert.That(evercraftDbContext.DnDCharacters.First().HitPoints, Is.EqualTo(3));
    }
}