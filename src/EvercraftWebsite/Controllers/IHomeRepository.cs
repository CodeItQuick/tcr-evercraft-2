using EvercraftWebsite.Data;

namespace EvercraftWebsite.Controllers;

public interface IHomeRepository
{
    public List<DnDCharacter> RetrieveDnDCharacters();
    public int CreateCharacter(string characterName);
    public int RemoveCharacter(int id);
    public void EditCharacter(int id, string editedName);
    public void AttackCharacter(int attackedCharacterId, int randomDieRoll);
}