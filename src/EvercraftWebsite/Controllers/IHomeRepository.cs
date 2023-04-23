using EvercraftWebsite.Data;

namespace EvercraftWebsite.Controllers;

public interface IHomeRepository
{
    public List<DnDCharacter> RetrieveDnDCharacters();
    public int CreateCharacter(string characterName);
    public int RemoveCharacter(int id);
}