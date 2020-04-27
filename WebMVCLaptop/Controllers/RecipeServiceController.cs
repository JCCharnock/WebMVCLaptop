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
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/xml")]
    public class RecipeServiceController : ControllerBase
    {
        public RecipeServiceController(Service.RecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public RecipeService recipeService { get; }

        [HttpGet]
        public IEnumerable<Recipe> GetAll()
        {
            return recipeService.GetRecipes();
        }
    }
}