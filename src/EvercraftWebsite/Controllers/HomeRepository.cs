using EvercraftWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Controllers;

public class HomeRepository : IHomeRepository
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
    public bool CreateCharacter(string characterName)
    {
        _applicationDbContext.DnDCharacters.Add(new DnDCharacter() { CharacterName = characterName ?? "Hello World" });
        _applicationDbContext.SaveChanges();
        return true;
    }
}

public interface IHomeRepository
{
    public List<DnDCharacter> RetrieveDnDCharacters();
}