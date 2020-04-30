using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebMVCLaptop.Data;
using WebMVCLaptop.Service;
using System.Linq;

namespace WebMVCLaptop.Models
{

    public class Recipes
    {
        public Recipe[] RecipeList { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize<Recipes>(this);
        }
    }

    public class Recipe
    {
        public string id { get; set; }
        public string name { get; set; }
        public Ingredient[] ingredients { get; set; }
        public Step[] steps { get; set; }
        public string image { get; set; }
        public string originalURL { get; set; }
        public string notes { get; set; }
        public string keywords { get; set; }

        // try to make create and edit the same!
        // may have to pull it all apart, collection must be identical so
        // views must have the same forms
        public static void CreateOrEditNewRecipe(string id,IFormCollection collection, WebMVCLaptopContext context)
        {
            Recipe newrec = new Recipe();

            // if id exists it's an edit
            // if it doesn't, it's new so get next available
            if (string.IsNullOrEmpty(id)) newrec.id = GetNextId(context); else newrec.id = id;

            newrec.name = collection["name"];
            newrec.originalURL = collection["originalURL"];
            newrec.notes = collection["notes"];
            newrec.image= collection["image"];
            newrec.keywords= collection["keywords"];

            List<string> tlist;
            string[] tsarr;
            tsarr = collection["ingredients"].ToString().Split("\r\n");
            tlist = tsarr.ToList<string>();
            tlist = tlist.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            tsarr = tlist.ToArray();
            newrec.ingredients = new Ingredient[tsarr.Length];
            for (int i = 0; i < tsarr.Length; i++)
            {
                Ingredient ni = new Ingredient();
                ni.name = System.Net.WebUtility.HtmlEncode(tsarr[i]);
                newrec.ingredients[i] = ni;
            }

            //seperate out steps
            tsarr = collection["steps"].ToString().Split("\r\n");
            //clean up using list
            tlist = tsarr.ToList<string>();
            tlist = tlist.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            tsarr = tlist.ToArray();

            newrec.steps = new Step[tsarr.Length];
            for (int i = 0; i < tsarr.Length; i++)
            {
                Step ns = new Step();
                ns.order = (i + 1).ToString();
                ns.text = System.Net.WebUtility.HtmlEncode(tsarr[i]);
                newrec.steps[i] = ns;
            }

            List<Recipe> tlist2 = context.Recipes.RecipeList.ToList<Recipe>();
            //remove 'old' record from context (if it exists)
            tlist2 = tlist2.Where(rec => rec.id != newrec.id).ToList<Recipe>();
            tlist2.Add(newrec);
            context.Recipes.RecipeList = tlist2.ToArray();

            // dangerous - updates json file
            RecipeService.WriteToJsonFile(context);
        }

        internal static void EditExistingRecipe(IFormCollection collection, WebMVCLaptopContext context)
        {
            throw new NotImplementedException();
        }

        private static string GetNextId(WebMVCLaptopContext context)
        {
            var rlist = context.Recipes.RecipeList;
            return (rlist.Max(x => int.Parse(x.id)) + 1).ToString();
        }
    }

    public class Ingredient
    {
        public string name { get; set; }
    }

    public class Step
    {
        public string order { get; set; }
        public string text { get; set; }
    }
}
