using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proiect1.Data;
using proiect1.Models;

namespace proiect1.Pages.SavedRecipes
{
    public class CreateModel : PageModel
    {
        private readonly proiect1.Data.proiect1Context _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(proiect1.Data.proiect1Context context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }



        public IActionResult OnGet()
        {
            var recipeList = _context.Recipe
                .Select(x => new
                {
                    x.Id,
                    RecipeFullName = x.Title // Assuming there is a property named Title in Recipe class
                })
                .ToList();

            ViewData["RecipeId"] = new SelectList(recipeList, "Id", "RecipeFullName");

            // Initialize SavedRecipe to a new instance
            SavedRecipe = new SavedRecipe();

            // Set default values
            SavedRecipe.RecipeId = recipeList.FirstOrDefault()?.Id ?? 0;
            SavedRecipe.SavedDate = DateTime.Now;

            return Page();
        }


        [BindProperty]
        public SavedRecipe SavedRecipe { get; set; } = default!;


        [BindProperty]
        public int RecipeId { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || SavedRecipe == null)
            {
                return Page();
            }

            try
            {
                // Asigură-te că RecipeId este setat corect
                SavedRecipe.RecipeId = RecipeId;

                _context.SavedRecipes.Add(SavedRecipe);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "You cannot save the same recipe more than once.");
                return Page();
            }
        }
    }

}

