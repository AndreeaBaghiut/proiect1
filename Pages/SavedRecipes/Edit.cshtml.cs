using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proiect1.Data;
using proiect1.Models;

namespace proiect1.Pages.SavedRecipes
{
    public class EditModel : PageModel
    {
        private readonly proiect1.Data.proiect1Context _context;

        public EditModel(proiect1.Data.proiect1Context context)
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

            var savedrecipe =  await _context.SavedRecipes.FirstOrDefaultAsync(m => m.Id == id);
            if (savedrecipe == null)
            {
                return NotFound();
            }
            SavedRecipe = savedrecipe;
           ViewData["RecipeId"] = new SelectList(_context.Recipe, "Id", "Id");
           ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SavedRecipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavedRecipeExists(SavedRecipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SavedRecipeExists(int id)
        {
          return (_context.SavedRecipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
