using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMVCLaptop.Models;
using WebMVCLaptop.Service;

namespace WebMVCLaptop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class RecipeServiceController : ControllerBase
    {
        public RecipeServiceController(Service.RecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public RecipeService recipeService { get; }

        [HttpGet]
        public IEnumerable<Recipe> Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return recipeService.GetRecipes();
            else return recipeService.GetSingle(id);
        }

        //[HttpGet]
        //public Recipe GetSingle(string id)
        //{
        //    return recipeService.GetSingle(id);
        //}

    }
}