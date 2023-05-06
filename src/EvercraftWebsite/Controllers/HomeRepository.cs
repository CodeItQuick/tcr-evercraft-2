using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Controllers;

[Authorize]
public class HomeRepository : IHomeRepository
{
    private readonly EvercraftDbContext _applicationDbContext;

    private static readonly Dictionary<int, CharacterModifier> Modifiers = new Dictionary<int, CharacterModifier>()
    {
        [1] = CharacterModifier.One,
        [2] = CharacterModifier.Two,
        [3] = CharacterModifier.Three,
        [4] = CharacterModifier.Four,
        [5] = CharacterModifier.Five,
        [6] = CharacterModifier.Six,
        [7] = CharacterModifier.Seven,
        [8] = CharacterModifier.Eight,
        [9] = CharacterModifier.Nine,
        [10] = CharacterModifier.Ten,
        [11] = CharacterModifier.Eleven,
        [12] = CharacterModifier.Twelve,
        [13] = CharacterModifier.Thirteen,
        [14] = CharacterModifier.Fourteen,
        [15] = CharacterModifier.Fifteen,
        [16] = CharacterModifier.Sixteen,
        [17] = CharacterModifier.Seventeen,
        [18] = CharacterModifier.Eighteen,
        [19] = CharacterModifier.Nineteen,
        [20] = CharacterModifier.Twenty,
    };

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

    public void EditCharacterName(int id, string editedName)
    {
        var dnDCharacter = _applicationDbContext.DnDCharacters.Find(id);
        if (dnDCharacter != null)
        {
            dnDCharacter.CharacterName = editedName;
            _applicationDbContext.DnDCharacters.Update(dnDCharacter);
        }
        _applicationDbContext.SaveChanges();
    }
    public void EditCharacterAlignment(int id, CharacterAlignment alignment)
    {
        var dnDCharacter = _applicationDbContext.DnDCharacters.Find(id);
        if (dnDCharacter != null)
        {
            dnDCharacter.Alignment = alignment;
            _applicationDbContext.DnDCharacters.Update(dnDCharacter);
        }
        _applicationDbContext.SaveChanges();
    }

    public void AttackCharacter(int attackedCharacterId, int randomDieRoll)
    {
        var dnDCharacter = _applicationDbContext.DnDCharacters.Find(attackedCharacterId);
        
        if (dnDCharacter is not { } character || character.Armor >= randomDieRoll) return;

        var damageAmt = randomDieRoll == 20 ? 2 : 1;

        if (character.HitPoints <= damageAmt)
        {
            _applicationDbContext.DnDCharacters.Remove(dnDCharacter);
            _applicationDbContext.SaveChanges();
            return;
        }
        
        dnDCharacter.HitPoints -= damageAmt;
        _applicationDbContext.DnDCharacters.Update(dnDCharacter);
        _applicationDbContext.SaveChanges();
    }

    public void SetModifier(int id, int modifierIdx, string modifierType)
    {
        var dnDCharacter = _applicationDbContext.DnDCharacters.Find(id);
        if (dnDCharacter != null)
        {
            var modifierHandler = new Dictionary<string, Func<int, DnDCharacter, DnDCharacter>>()
            {
                ["Charisma"] = (value, characterValue) => { 
                    characterValue.CharismaModifier = Modifiers[value];
                    return characterValue;
                }
            };
            dnDCharacter = modifierHandler["Charisma"](modifierIdx, dnDCharacter);

            _applicationDbContext.DnDCharacters.Update(dnDCharacter);
        }
        _applicationDbContext.SaveChanges();
    }
}