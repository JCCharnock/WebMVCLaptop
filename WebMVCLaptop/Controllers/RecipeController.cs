using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMVCLaptop.Service;
using WebMVCLaptop.Models;
using WebMVCLaptop.Data;
using System.Linq;

namespace WebMVCLaptop.Controllers
{
    public class RecipeController : Controller
    {
        private readonly WebMVCLaptopContext _context;

        public RecipeController(WebMVCLaptopContext context)
        {
            _context = context;
        }


        // GET: Recipe
        public ActionResult Index()
        {
            
            
            return View(_context.Recipes);
        }

        // GET: Recipe/Details/2
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var recipe = new Recipe();

            var recipe = _context.Recipes.RecipeList.FirstOrDefault(m => m.id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recipe/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}