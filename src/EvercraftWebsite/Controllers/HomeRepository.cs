using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Controllers;

[Authorize]
public class HomeRepository : IHomeRepository
{
    private readonly EvercraftDbContext _applicationDbContext;

    private static readonly Dictionary<int, CharacterModifier> Modifiers = new()
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

    private static readonly Dictionary<string, Func<int, DnDCharacter, DnDCharacter>> ModifierHandler = new()
    {
        ["charisma"] = (modifierIdx, characterValue) => { 
            characterValue.CharismaModifier = Modifiers[modifierIdx];
            return characterValue;
        },
        ["strength"] = (modifierIdx, characterValue) => { 
            characterValue.StrengthModifier = Modifiers[modifierIdx];
            return characterValue;
        },
        ["dexterity"] = (modifierIdx, characterValue) => { 
            characterValue.DexterityModifier = Modifiers[modifierIdx];
            return characterValue;
        },
        ["constitution"] = (modifierIdx, characterValue) => { 
            characterValue.ConstitutionModifier = Modifiers[modifierIdx];
            return characterValue;
        },
        ["wisdom"] = (modifierIdx, characterValue) => { 
            characterValue.WisdomModifier = Modifiers[modifierIdx];
            return characterValue;
        },
        ["intelligence"] = (modifierIdx, characterValue) => { 
            characterValue.IntelligenceModifier = Modifiers[modifierIdx];
            return characterValue;
        },
    };

    private static readonly Dictionary<int, int> ModifierTable = new Dictionary<int, int>
    {
        [1] = -5,
        [2] = -4,
        [3] = -4,
        [4] = -3,
        [5] = -3,
        [6] = -2,
        [7] = -2,
        [8] = -1,
        [9] = -1,
        [10] = 0,
        [11] = 0,
        [12] = 1,
        [13] = 1,
        [14] = 2,
        [15] = 2,
        [16] = 3,
        [17] = 3,
        [18] = 4,
        [19] = 4,
        [20] = 5
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
        var attackedCharacter = _applicationDbContext.DnDCharacters.Find(attackedCharacterId);
        
        if (attackedCharacter is not { } character ) return;
        var effectiveArmor = randomDieRoll - ModifierTable[(int) character.DexterityModifier];
        if (character.Armor >= effectiveArmor) return;

        // weaker enemies get hit harder, stronger enemies only get hit for 1 damage
        var coreDamage = (int) character.StrengthModifier > 10 ? 1: 1 - ModifierTable[(int) character.StrengthModifier];
        // highest level characters experience level gets added to the damage
        var characterExperiencePoints = _applicationDbContext.DnDCharacters.OrderBy(x => x.ExperiencePoints).Last().ExperiencePoints;
        var damageAmt = randomDieRoll == 20 ? 2 * coreDamage + characterExperiencePoints / 1000 :
            coreDamage + characterExperiencePoints / 1000;

        var characterDied = character.HitPoints <= damageAmt;
        if (characterDied)
        {
            _applicationDbContext.DnDCharacters.Remove(attackedCharacter);
            _applicationDbContext.SaveChanges();
            var dnDCharacters = _applicationDbContext.DnDCharacters.ToList();
            dnDCharacters.ForEach(x => { 
                x.ExperiencePoints += 1000;
                var constitutionModifier = (int) x.ConstitutionModifier < 12 ? 0 : ModifierTable[(int) x.ConstitutionModifier];
                x.HitPoints += 5 + constitutionModifier;
            });
            _applicationDbContext.SaveChanges();
            return;
        }
        
        attackedCharacter.HitPoints -= damageAmt;
        _applicationDbContext.DnDCharacters.Update(attackedCharacter);
        _applicationDbContext.SaveChanges();
    }

    public void SetModifier(int id, int modifierIdx, string modifierType)
    {
        var dnDCharacter = _applicationDbContext.DnDCharacters.Find(id);
        if (dnDCharacter != null)
        {
            var constitutionBoostsHp = 
                modifierType.ToLower().Equals("constitution") && 
                ModifierTable[(int)dnDCharacter.ConstitutionModifier] < ModifierTable[modifierIdx];
            if (constitutionBoostsHp)
            {
                dnDCharacter.HitPoints += ModifierTable[modifierIdx];
            }
            dnDCharacter = ModifierHandler[modifierType.ToLower()](modifierIdx, dnDCharacter);

            _applicationDbContext.DnDCharacters.Update(dnDCharacter);
        }
        _applicationDbContext.SaveChanges();
    }
}