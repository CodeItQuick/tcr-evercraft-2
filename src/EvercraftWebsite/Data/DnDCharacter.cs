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
}