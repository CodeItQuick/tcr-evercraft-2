using EvercraftWebsite.Controllers;
using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EvercraftWebsite.Views.Home;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class HomeModel : PageModel
{
    private readonly ILogger<HomeModel> _logger;

    [JsonProperty(PropertyName = "DnDCharacters")]
    public List<DnDCharacter>? DnDCharacters { get; set; } = new List<DnDCharacter>();


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