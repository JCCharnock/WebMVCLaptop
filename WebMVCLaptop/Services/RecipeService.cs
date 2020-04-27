using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WebMVCLaptop.Models;
using Microsoft.AspNetCore.Hosting;
using System;

namespace WebMVCLaptop.Service
{
    public class RecipeService
    {
        public RecipeService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "recipe.json"); }
        }

        public IEnumerable<Recipe> GetRecipes()
        {

            //ManupulateRecipeJson();

            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Recipe[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        private void ManupulateRecipeJson()
        {
            Recipe[] temp = new Recipe[] { };

            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                //get recipe list object
                temp = JsonSerializer.Deserialize<Recipe[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                //foreach (Recipe rec in temp)
                //{
                //    foreach (Ingredient ing in rec.ingredients)
                //    {
                //        //ing.type = null;
                //    }
                //}
                //jsonFileReader.Close();  //needed?
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Recipe>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    temp
                );
            }



        }
    }

}