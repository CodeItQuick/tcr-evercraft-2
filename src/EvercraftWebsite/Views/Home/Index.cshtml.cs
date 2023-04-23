using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EvercraftWebsite.Views.Home;

public class HomeModel
{
    private readonly ILogger<HomeModel> _logger;
    public List<DnDCharacter>? DnDCharacters { get; set; } = new();


    public HomeModel()
    {
    }

    public HomeModel(ILogger<HomeModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}