using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using proiect1.Data;
using proiect1.Models;

namespace proiect1.Pages.SavedRecipes
{
    public class DeleteModel : PageModel
    {
        private readonly proiect1.Data.proiect1Context _context;

        public DeleteModel(proiect1.Data.proiect1Context context)
        {
            _context = context;
        }

        [BindProperty]
      public SavedRecipe SavedRecipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SavedRecipes == null)
            {
                return NotFound();
            }

            var savedrecipe = await _context.SavedRecipes.FirstOrDefaultAsync(m => m.Id == id);

            if (savedrecipe == null)
            {
                return NotFound();
            }
            else 
            {
                SavedRecipe = savedrecipe;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SavedRecipes == null)
            {
                return NotFound();
            }
            var savedrecipe = await _context.SavedRecipes.FindAsync(id);

            if (savedrecipe != null)
            {
                SavedRecipe = savedrecipe;
                _context.SavedRecipes.Remove(SavedRecipe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
