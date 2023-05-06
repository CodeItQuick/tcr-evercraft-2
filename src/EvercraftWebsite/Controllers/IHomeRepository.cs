using EvercraftWebsite.Data;

namespace EvercraftWebsite.Controllers;

public interface IHomeRepository
{
    public List<DnDCharacter> RetrieveDnDCharacters();
    public int CreateCharacter(string characterName);
    public int RemoveCharacter(int id);
    public void EditCharacterName(int id, string editedName);
    public void AttackCharacter(int attackedCharacterId, int randomDieRoll);
    public void EditCharacterAlignment(int id, CharacterAlignment alignment);
    public void SetModifier(int id, int modifierIdx, string modifierType);
}