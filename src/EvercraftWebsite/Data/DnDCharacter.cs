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
    public CharacterModifier StrengthModifier { get; set; }
}

public enum CharacterModifier
{
    One = -5,
    Two = -4,
    Three = -4,
    Four = -3,
    Five = -3,
    Six = -2,
    Seven = -2,
    Eight = -1,
    Nine = -1,
    Ten = 0,
    Eleven = 0,
    Twelve = 1,
    Thirteen = 1,
    Fourteen = 2,
    Fifteen = 2,
    Sixteen = 3,
    Seventeen = 3,
    Eighteen = 4,
    Nineteen = 4,
    Twenty = 5,
        
}

public enum CharacterAlignment
{
    Evil = -1,
    Neutral = 0,
    Good = 1
}