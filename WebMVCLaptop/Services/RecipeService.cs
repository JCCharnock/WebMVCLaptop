using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WebMVCLaptop.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using WebMVCLaptop.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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


        private static string JsonFileNameNonWeb()
        {
            //string strtemp = AppDomain.CurrentDomain.BaseDirectory;
            string strtemp = Directory.GetCurrentDirectory();
            return Path.Combine(strtemp, "wwwroot", "data", "recipe.json");
        }

        internal IEnumerable<Recipe> GetSingle(string id)
        {
            Recipe[] list;
            List<Recipe> ret = new List<Recipe>();
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                list = JsonSerializer.Deserialize<Recipe[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            for (int i = 0; i<list.Length; i++)
            {
                if (list[i].id == id) ret.Add(list[i]);
            }
            return ret;
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

        // write back to json file
        public static void WriteToJsonFile(WebMVCLaptopContext context)
        {
            //shouldn't need to to this
            //but if you don't bits of the old context are left in the file
            File.Delete(JsonFileNameNonWeb());

            using (var outputStream = File.OpenWrite(JsonFileNameNonWeb()))
            {
                // first delete 


                JsonSerializer.Serialize<IEnumerable<Recipe>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    context.Recipes.RecipeList
                );
            }
        }

        public static void DeleteRecipe(string id, WebMVCLaptopContext context)
        {
            List<Recipe> tlist = context.Recipes.RecipeList.ToList<Recipe>();
            tlist = tlist.Where(p => p.id != id).ToList();
            context.Recipes.RecipeList = tlist.ToArray();
            //DANGER!!
            WriteToJsonFile(context);
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

                foreach (Recipe rec in temp)
                {
                    if (rec.ingredients != null)
                    {
                        foreach (Ingredient ing in rec.ingredients)
                        {
                            //ing.name = ing.quantity + " " + ing.name;
                            //ing.quantity = null;
                        }
                    }
                }
                jsonFileReader.Close();  //needed?
            }

            // DELETING FILE - ARE YOU SURE
            File.Delete(JsonFileNameNonWeb());

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