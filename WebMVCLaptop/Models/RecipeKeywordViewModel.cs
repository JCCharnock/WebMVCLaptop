using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCLaptop.Models
{
    public class RecipeKeywordViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public SelectList Keywords { get; set; }
        public string Keyword { get; set; }
        public string SearchString { get; set; }
        public bool IncludeUntested { get; set; }
    }
}
