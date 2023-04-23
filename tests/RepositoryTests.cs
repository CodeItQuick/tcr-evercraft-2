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
}