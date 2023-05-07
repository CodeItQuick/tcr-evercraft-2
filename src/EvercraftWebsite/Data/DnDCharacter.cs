using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvercraftWebsite.Data;

[Table("DnDCharacter")]
public class DnDCharacter
{
    [Key]
    public int Id { get; set; }
    public string CharacterName { get; set; }
    public int HitPoints { get; set; } = 5;
    public int Armor { get; set; } = 10;
    public CharacterAlignment Alignment { get; set; } = CharacterAlignment.Neutral;
    public CharacterModifier StrengthModifier { get; set; } = CharacterModifier.Ten;
    public CharacterModifier DexterityModifier { get; set; } = CharacterModifier.Ten;
    public CharacterModifier ConstitutionModifier { get; set; } = CharacterModifier.Ten;
    public CharacterModifier WisdomModifier { get; set; } = CharacterModifier.Ten;
    public CharacterModifier IntelligenceModifier { get; set; } = CharacterModifier.Ten;
    public CharacterModifier CharismaModifier { get; set; } = CharacterModifier.Ten;
    public int ExperiencePoints { get; set; } = 0;
}

public enum CharacterModifier
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Eleven = 11,
    Twelve = 12,
    Thirteen = 13,
    Fourteen = 14,
    Fifteen = 15,
    Sixteen = 16,
    Seventeen = 17,
    Eighteen = 18,
    Nineteen = 19,
    Twenty = 20,
}

public enum CharacterAlignment
{
    Evil = -1,
    Neutral = 0,
    Good = 1
}