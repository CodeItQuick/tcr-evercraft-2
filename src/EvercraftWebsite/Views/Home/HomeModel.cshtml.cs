using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EvercraftWebsite.Views.Home;

public class HomeModel : PageModel
{
    private readonly ILogger<HomeModel> _logger;

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