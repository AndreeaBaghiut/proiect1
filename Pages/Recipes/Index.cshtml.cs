using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using proiect1.Data;
using proiect1.Models;

namespace proiect1.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly proiect1.Data.proiect1Context _context;

        public IndexModel(proiect1.Data.proiect1Context context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get; set; }// = default!;

        public RecipeData RecipeD { get; set; }

        public int RecipeId { get; set; }

        public int CategoryId { get; set; }

        public string TitleSort { get; set; }

        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID, int? ingredientId, string sortOrder, string searchString)
        {
            //Recipe = await _context.Recipe.ToListAsync();

            RecipeD = new RecipeData(); 

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

            CurrentFilter = searchString;

            RecipeD.Recipes = await _context.Recipe
             .Include(r => r.RecipeIngredients)
                 .ThenInclude(ri => ri.Ingredient)
             .Include(r => r.RecipeCategories)
                 .ThenInclude(rc => rc.Category)
             .AsNoTracking()
             .OrderBy(r => r.Title)
             .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                RecipeD.Recipes = RecipeD.Recipes.Where(s =>
                    s.RecipeCategories.Any(rc => rc.Category.CategoryName.Contains(searchString))
                    || s.Title.Contains(searchString)
                );
            }

            if (id != null)
            {
                RecipeId = id.Value;
                Recipe recipe = RecipeD.Recipes
                    .Where(i => i.Id == id.Value).Single();
                RecipeD.Categories = recipe.RecipeCategories.Select(rc => rc.Category);
            }


            switch (sortOrder)
                {
                    case "title_desc":
                        RecipeD.Recipes = RecipeD.Recipes.OrderByDescending(s => s.Title);
                        break;
                    default:
                        RecipeD.Recipes = RecipeD.Recipes.OrderBy(s => s.Title);
                        break;

                }
            }
        }
    }

