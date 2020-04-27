using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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
        public string imageURL { get; set; }
        public string originalURL { get; set; }
    }

    public class Ingredient
    {
        public string quantity { get; set; }
        public string name { get; set; }
    }

    public class Step
    {
        public string order { get; set; }
        public string text { get; set; }
    }

}
