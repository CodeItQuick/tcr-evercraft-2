using EvercraftWebsite.Data;
using EvercraftWebsite.Views.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;
        private EvercraftDbContext _applicationDbContext;

        public HomeController(
            EvercraftDbContext? evercraftDbContext, 
            IHomeRepository homeRepository,
            DbContextOptions<EvercraftDbContext>? dbContextOptions = null)
        {
            _homeRepository = homeRepository;
            DbContextOptions<EvercraftDbContext> options = dbContextOptions ?? 
                                                           new DbContextOptionsBuilder<EvercraftDbContext>()
                                                               .UseInMemoryDatabase("TemporaryDatabase").Options;
            _applicationDbContext = evercraftDbContext ?? new EvercraftDbContext(options);
        }

        // GET: HomeController
        [HttpGet]
        [HttpPost]
        public ActionResult Home()
        {
            var dnDCharacters = _applicationDbContext.DnDCharacters.ToList();
            var indexModel = new HomeModel() { DnDCharacters = dnDCharacters };
            return View(indexModel);
        }

        // GET: HomeController/Create
        [HttpPost]
        [HttpGet]
        public ActionResult Create([FromForm] string? characterName)
        {
            if (characterName == null)
            {
                return RedirectToAction("Home", "Home", new { characterName = characterName ?? "defaultCharacterName" });
            }
            
            _applicationDbContext.DnDCharacters.Add(new DnDCharacter() { CharacterName = characterName ?? "Hello World" });
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Home", "Home");
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Home");
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Home");
        }
        //
        // // POST: HomeController/Delete/5
        // TODO: No idea how this works
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Delete(int id, IFormCollection collection)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }
    }
}
