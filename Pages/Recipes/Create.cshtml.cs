using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using proiect1.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace proiect1.Pages.Recipes

{
    [Authorize(Roles = "Admin")]

    public class CreateModel : RecipeCategoriesPageModel
    {
        private readonly proiect1.Data.proiect1Context _context;
        private readonly IWebHostEnvironment _environment;
       


        public CreateModel(proiect1.Data.proiect1Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
           
        }


        [BindProperty]
        public Recipe Recipe { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }
        [BindProperty] 
        public List<RecipeIngredientInputModel> RecipeIngredientsInput { get; set; }

        [BindProperty]
        public RecipePhoto RecipePhoto { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }


        public IActionResult OnGet()
        {
            var recipe = new Recipe();
            recipe.RecipeCategories = new List<RecipeCategory>();
            recipe.RecipeIngredients = new List<RecipeIngredient>();
            PopulateAssignedCategoryData(_context, recipe);
           // Recipe.PublishingDate = DateTime.Now;
            PopulateIngredientsDropdown();
            RecipeIngredientsInput = new List<RecipeIngredientInputModel>();

            return Page();
        }

        private void PopulateIngredientsDropdown()
        {
            var ingredients = _context.Ingredient.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name,
                Group = new SelectListGroup { Name = i.Unit } 
            })
            .ToList();

            AllIngredients = ingredients;


        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (selectedCategories != null)
            {
                Recipe.RecipeCategories = selectedCategories
                    .Select(cat => new RecipeCategory { CategoryId = int.Parse(cat) })
                    .ToList();
            }

            if (RecipeIngredientsInput != null)
            {
                Recipe.RecipeIngredients = RecipeIngredientsInput
                    .Select(input => new RecipeIngredient
                    {
                        IngredientID = input.IngredientID,
                        Quantity = input.Quantity
                    })
                    .ToList();
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

           

            Recipe.PublishingDate = DateTime.Now;
           
            _context.Recipe.Add(Recipe);
            await _context.SaveChangesAsync();
            Recipe.Photo = Photo.FileName;
            return RedirectToPage("./Index");

        }
    }
        public class RecipeIngredientInputModel
        {
            public int IngredientID { get; set; }
            public decimal Quantity { get; set; }
        }

    }
