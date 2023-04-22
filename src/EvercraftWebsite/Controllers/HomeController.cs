using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvercraftWebsite.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create(string? characterName)
        {
            return RedirectToAction("Index", new  { characterName = characterName ?? "defaultCharacterName" });
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Index");
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
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
