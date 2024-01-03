using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using proiect1.Data;
using proiect1.Models;

namespace proiect1.Pages.Recipes
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly proiect1.Data.proiect1Context _context;

        public DeleteModel(proiect1.Data.proiect1Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Recipe Recipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }
            else 
            {
                Recipe = recipe;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.SavedRecipe)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Remove the associated SavedRecipe if it exists
            if (recipe.SavedRecipe != null)
            {
                _context.SavedRecipes.Remove(recipe.SavedRecipe);
            }

            // Remove the recipe
            _context.Recipe.Remove(recipe);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
