// EditModel.cshtml.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using proiect1.Data;
using proiect1.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace proiect1.Pages.Recipes
{
    [Authorize(Roles = "Admin")]
    public class EditModel : RecipeCategoriesPageModel
    {
        private readonly proiect1Context _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(proiect1Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipe
    .Include(b => b.RecipeIngredients).ThenInclude(b => b.Ingredient)
    .Include(b => b.RecipeCategories).ThenInclude(b => b.Category)
    .FirstOrDefaultAsync(m => m.Id == id);


            if (Recipe == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Recipe);
            PopulateIngredientsDropdown();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (Photo != null && Photo.Length > 0)
            {
                var directoryPath = Path.Combine(_environment.WebRootPath, "Recipe Photos");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var photoPath = Path.Combine(directoryPath, Photo.FileName);
                using (var stream = new FileStream(photoPath, FileMode.Create))
                {
                    await Photo.CopyToAsync(stream);
                }

                RecipePhoto recipePhoto = new RecipePhoto
                {
                    Image = Photo,
                    ImagePath = Photo.FileName
                };

                _context.RecipePhotos.Add(recipePhoto);
                Recipe.Photo = Photo.FileName;
            }

            var recipeToUpdate = await _context.Recipe
                .Include(i => i.RecipeIngredients)
                .ThenInclude(i => i.Ingredient)
                .Include(i => i.RecipeCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (recipeToUpdate == null)
            {
                return NotFound();
            }

            // Update the properties of existing ingredients
            if (await TryUpdateModelAsync<Recipe>(
                 recipeToUpdate,
                 "Recipe",
                 i => i.Title, i => i.PublishingDate,
                 i => i.Description, i => i.Instructions,
                 i => i.RecipeIngredients))

            {
                // Update the RecipeIngredients separately
                UpdateRecipeIngredients(recipeToUpdate);
                // Update categories
                UpdateRecipeCategories(_context, selectedCategories, recipeToUpdate);
                await _context.SaveChangesAsync();

                Recipe.Photo = Photo.FileName;
                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, recipeToUpdate);
            PopulateIngredientsDropdown();

            return Page();
        }

        private void UpdateRecipeIngredients(Recipe recipeToUpdate)
        {
            var existingIngredients = recipeToUpdate.RecipeIngredients.ToDictionary(ri => ri.Id);
            var updatedIngredients = new List<RecipeIngredient>();

            foreach (var ingredient in recipeToUpdate.RecipeIngredients)
            {
                var ingredientId = ingredient.Id;

                if (existingIngredients.ContainsKey(ingredientId))
                {
                    // Update the properties of the existing ingredient
                    var existingIngredient = existingIngredients[ingredientId];
                    _context.Entry(existingIngredient).CurrentValues.SetValues(ingredient);
                    updatedIngredients.Add(existingIngredient);
                }
                else
                {
                    // This is a new ingredient, add it to the context
                    _context.RecipeIngredient.Add(ingredient);
                    updatedIngredients.Add(ingredient);
                }
            }



            // Remove deleted ingredients
            var deletedIngredients = existingIngredients.Keys.Except(updatedIngredients.Select(ri => ri.Id)).ToList();
            foreach (var ingredientId in deletedIngredients)
            {
                var ingredientToRemove = existingIngredients[ingredientId];
                _context.RecipeIngredient.Remove(ingredientToRemove);
            }

            // Update the recipeToUpdate with the modified list of RecipeIngredients
            recipeToUpdate.RecipeIngredients = updatedIngredients;
        }


        private void PopulateIngredientsDropdown()
        {
            AllIngredients = _context.Ingredient
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                    Group = new SelectListGroup { Name = i.Unit }
                })
                .ToList();
        }
    }
}

