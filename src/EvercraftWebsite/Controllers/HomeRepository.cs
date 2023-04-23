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
    public int CreateCharacter(string characterName)
    {
        _applicationDbContext.DnDCharacters.Add(new DnDCharacter() { CharacterName = characterName ?? "Hello World" });
        var saveChanges = _applicationDbContext.SaveChanges();
        return saveChanges;
    }
}

public interface IHomeRepository
{
    public List<DnDCharacter> RetrieveDnDCharacters();
}