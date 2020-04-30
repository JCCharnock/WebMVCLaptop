using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMVCLaptop.Service;
using WebMVCLaptop.Models;
using WebMVCLaptop.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public ActionResult Index(string keyword, string searchString)
        {
            // need to break down space delimited keyword lists
            var klists = from r in _context.Recipes.RecipeList where (!string.IsNullOrWhiteSpace(r.keywords)) select r.keywords;
            List<string> klist = new List<string>();
            foreach (string k in klists)
            {
                string[] ta = k.Split(' ');
                for (int i = 0; i < ta.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(ta[i]))
                    {
                        if (!klist.Contains(ta[i])) klist.Add(ta[i]);
                    }
                }
            }

            var recs = from r in _context.Recipes.RecipeList select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                recs = recs.Where(s => s.name.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                // replace this with some fancy LINQ query when I know how
                recs = FilterRecipesByKeyword(recs, keyword);
            }

            var recipeKeywordVM = new RecipeKeywordViewModel()
            {
                Keywords = new SelectList(klist),
                Recipes = recs.ToList()
            };

            return View(recipeKeywordVM);
        }

        private IEnumerable<Recipe> FilterRecipesByKeyword(IEnumerable<Recipe> recs, string keyword)
        {
            List<Recipe> ret = new List<Recipe>();
            foreach(Recipe rec in recs)
            {
                if (rec.keywords != null)
                {
                    if (rec.keywords.Split(' ').Contains<string>(keyword)) ret.Add(rec);
                }
            }
            return ret;
        }

        // GET: Recipe/Details/2
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
                // does context exist at this point? YES
                Recipe.CreateOrEditNewRecipe("", collection, _context);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _context.Recipes.RecipeList.FirstOrDefault(m => m.id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                Recipe.CreateOrEditNewRecipe(id, collection, _context);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _context.Recipes.RecipeList.FirstOrDefault(m => m.id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _context.Recipes.RecipeList.FirstOrDefault(m => m.id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            WebMVCLaptop.Service.RecipeService.DeleteRecipe(id, _context);
            return RedirectToAction(nameof(Index));
        }
    }
}