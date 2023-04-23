using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EvercraftWebsite.Views.Home;

public class HomeModel
{
    public List<DnDCharacter>? DnDCharacters { get; set; } = new();
    
}