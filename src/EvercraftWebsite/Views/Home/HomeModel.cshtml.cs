using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EvercraftWebsite.Views.Home;

public class HomeModel : PageModel
{
    private readonly ILogger<HomeModel>? _logger;
    public List<DnDCharacter>? DnDCharacters { get; set; }


    public HomeModel()
    {
    }

    public HomeModel(ILogger<HomeModel>? logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}