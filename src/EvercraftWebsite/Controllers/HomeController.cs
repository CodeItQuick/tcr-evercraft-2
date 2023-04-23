﻿using EvercraftWebsite.Data;
using EvercraftWebsite.Views.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Controllers
{
    public class HomeController : Controller
    {
        private EvercraftDbContext _applicationDbContext;

        public HomeController(DbContextOptions<EvercraftDbContext>? dbContextOptions = null)
        {
            DbContextOptions<EvercraftDbContext> options = dbContextOptions ?? 
                new DbContextOptionsBuilder<EvercraftDbContext>()
                .UseInMemoryDatabase("TemporaryDatabase").Options;
            _applicationDbContext = new EvercraftDbContext(options);
        }

        // GET: HomeController
        public ActionResult Index()
        {
            var dnDCharacters = _applicationDbContext.DnDCharacters.ToList();

            HomeModel indexModel = new HomeModel() { DnDCharacters = dnDCharacters };
            return View(indexModel);
        }

        // GET: HomeController/Create
        public ActionResult Create(string? characterName)
        {
            _applicationDbContext.DnDCharacters.Add(new DnDCharacter() { CharacterName = characterName ?? "Hello World" });
            // _applicationDbContext.SaveChanges();
            
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
