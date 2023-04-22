using System.ComponentModel.DataAnnotations.Schema;
using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Controllers
{
    public class HomeController : Controller
    {
        private EvercraftDbContext _applicationDbContext;

        public HomeController()
        {
            DbContextOptions<EvercraftDbContext> options = new DbContextOptionsBuilder<EvercraftDbContext>()
                .UseInMemoryDatabase("TemporaryDatabase").Options;
            _applicationDbContext = new EvercraftDbContext(options);
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

    public class EvercraftDbContext : DbContext
    {
        public EvercraftDbContext(DbContextOptions<EvercraftDbContext> options): base(options)
        {
            
        }
        
        public virtual DbSet<DnDCharacter> DnDCharacters { get; set; }
    }

    [Table("DnDCharacter")]
    public class DnDCharacter
    {
    }
}
