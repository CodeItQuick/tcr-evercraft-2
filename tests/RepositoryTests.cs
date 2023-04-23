using EvercraftWebsite.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace tcr_evercraft_2_tests;

public class RepositoryTests
{
    
    [Test]
    public Task RepositoryCanRetrieveListOfCharacters()
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

public class HomeRepository
{
    private readonly EvercraftDbContext _applicationDbContext;

    public HomeRepository(EvercraftDbContext? evercraftDbContext, DbContextOptions<EvercraftDbContext>? dbContextOptions = null)
    {
        DbContextOptions<EvercraftDbContext> options = dbContextOptions ?? 
                                                       new DbContextOptionsBuilder<EvercraftDbContext>()
                                                           .UseInMemoryDatabase("TemporaryDatabase").Options;
        _applicationDbContext = evercraftDbContext ?? new EvercraftDbContext(options);
    }

    public List<DnDCharacter> RetrieveDnDCharacters()
    {
        return _applicationDbContext.DnDCharacters.ToList();
    }
}