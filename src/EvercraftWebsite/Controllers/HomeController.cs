﻿using EvercraftWebsite.Data;
using EvercraftWebsite.Views.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvercraftWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;
        private readonly Random _random;

        public HomeController(IHomeRepository homeRepository, DieRandomPicker? dieRandomPicker = null)
        {
            _homeRepository = homeRepository;
            _random = dieRandomPicker ?? new();
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
            _homeRepository.EditCharacterName(id, editedCharacterName);
            return RedirectToAction("Home");
        }
        // GET: HomeController/EditAlignment/5
        public ActionResult EditAlignment(int id, CharacterAlignment alignment)
        {
            _homeRepository.EditCharacterAlignment(id, alignment);
            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult CharacterAttacked(int attackedCharacterId)
        {
            var randomDieRoll = _random.Next(1, 20);
            _homeRepository.AttackCharacter(attackedCharacterId, randomDieRoll);
            return RedirectToAction("Home");
        }

        
        [HttpPost]
        public ActionResult EditModifiers(int id, int modifierNumber, string modifierType)
        {
            
            _homeRepository.SetModifier(id, modifierNumber, modifierType);
            return RedirectToAction("Home");
        }
    }
}
