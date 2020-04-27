using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Text.Json;
using WebMVCLaptop.Models;


namespace WebMVCLaptop.Data
{
    public class WebMVCLaptopContext
    {
        public Recipes Recipes { get; set; }

        public WebMVCLaptopContext()
        {
            // OK try getting data direct from json 
            string dir = Directory.GetCurrentDirectory();
            string strTemp;
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                strTemp = jsonFileReader.ReadToEnd();
            }

            Recipe[] rarr= JsonSerializer.Deserialize<Recipe[]>(strTemp);
            Recipes = new Recipes();
            Recipes.RecipeList = rarr;
        }

        private string JsonFileName
        {
            get { return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","data", "recipe.json"); }
        }

    }
}