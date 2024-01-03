using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using proiect1.Data;
using proiect1.Models;
using proiect1.Models.ViewModels;

namespace proiect1.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly proiect1.Data.proiect1Context _context;

        public IndexModel(proiect1.Data.proiect1Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        public CategoriesIndexData CategoryData { get; set; }

        public int CategoryId { get; set; }

        public int RecipeId { get; set; }


        public async Task OnGetAsync(int? id, int? recipeId)
        {
            CategoryData = new CategoriesIndexData();
            CategoryData.Categories = await _context.Category
     .Include(c => c.RecipeCategories)
         .ThenInclude(rc => rc.Recipe)
     .OrderBy(c => c.CategoryName)
     .ToListAsync();

            if (id != null)
            {
                CategoryId = id.Value;
                Category category = CategoryData.Categories
                    .FirstOrDefault(c => c.Id == id.Value);

                if (category != null)
                {
                    CategoryData.Recipes = category.RecipeCategories.Select(rc => rc.Recipe).ToList();
                }
            }

        }
    }
}
