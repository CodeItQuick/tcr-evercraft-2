using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Controllers;

[Authorize]
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
        _applicationDbContext.DnDCharacters.Add(new DnDCharacter()
        {
            CharacterName = characterName ?? "Hello World"
        });
        var saveChanges = _applicationDbContext.SaveChanges();
        return saveChanges;
    }
    public int RemoveCharacter(int id)
    {
        var dnDCharacter = _applicationDbContext.DnDCharacters.Find(id);
        if (dnDCharacter != null)
        {
            _applicationDbContext.DnDCharacters.Remove(dnDCharacter);
        }
        var saveChanges = _applicationDbContext.SaveChanges();
        return saveChanges;
    }

    public void EditCharacter(int id, string editedName)
    {
        var dnDCharacter = _applicationDbContext.DnDCharacters.Find(id);
        if (dnDCharacter != null)
        {
            dnDCharacter.CharacterName = editedName;
            _applicationDbContext.DnDCharacters.Update(dnDCharacter);
        }
        _applicationDbContext.SaveChanges();
    }

    public void AttackCharacter(int attackedCharacterId, int randomDieRoll)
    {
        var dnDCharacter = _applicationDbContext.DnDCharacters.Find(attackedCharacterId);
        
        if (dnDCharacter is not { } character || character.Armor >= randomDieRoll) return;

        if (character.HitPoints <= 1)
        {
            _applicationDbContext.DnDCharacters.Remove(dnDCharacter);
            _applicationDbContext.SaveChanges();
            return;
        }
        
        dnDCharacter.HitPoints -= 1;
        _applicationDbContext.DnDCharacters.Update(dnDCharacter);
        _applicationDbContext.SaveChanges();
    }
}