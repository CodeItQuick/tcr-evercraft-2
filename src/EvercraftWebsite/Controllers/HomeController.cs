using EvercraftWebsite.Views.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvercraftWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        // GET: HomeController
        [HttpGet]
        [HttpPost]
        public ActionResult Home()
        {
            var dnDCharacters = _homeRepository.RetrieveDnDCharacters();
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
                return RedirectToAction("Home", "Home");
            }

            _homeRepository.CreateCharacter(characterName);

            return RedirectToAction("Home", "Home");
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            _homeRepository.RemoveCharacter(id);
            return RedirectToAction("Home");
        }
        
        // GET: HomeController/Edit/5
        public ActionResult Edit(int id, string editedCharacterName)
        {
            _homeRepository.EditCharacter(id, editedCharacterName);
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
        public void CharacterAttacked(int i)
        {
            
        }
    }
}
