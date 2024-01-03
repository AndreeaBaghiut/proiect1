using Microsoft.AspNetCore.Mvc.RazorPages;
using proiect1.Data;

namespace proiect1.Models
{
    public class RecipeCategoriesPageModel:PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(proiect1Context context,
        Recipe recipe)
        {
            var allCategories = context.Category;
            var recipeCategories = new HashSet<int>(
            recipe.RecipeCategories.Select(c => c.CategoryId)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.Id,
                    Name = cat.CategoryName,
                    Assigned = recipeCategories.Contains(cat.Id)
                });
            }
        }
        public void UpdateRecipeCategories(proiect1Context context,
 string[] selectedCategories, Recipe recipeToUpdate)
        {
            if (selectedCategories == null)
            {
                recipeToUpdate.RecipeCategories = new List<RecipeCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var recipeCategories = new HashSet<int>
            (recipeToUpdate.RecipeCategories.Select(c => c.Category.Id));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.Id.ToString()))
                {
                    if (!recipeCategories.Contains(cat.Id))
                    {
                        recipeToUpdate.RecipeCategories.Add(
                        new RecipeCategory
                        {
                            RecipeId = recipeToUpdate.Id,
                            CategoryId = cat.Id
                        });
                    }
                }
                else
                {
                    if (recipeCategories.Contains(cat.Id))
                    {
                        RecipeCategory courseToRemove
                        = recipeToUpdate
                        .RecipeCategories
                      .SingleOrDefault(i => i.CategoryId == cat.Id);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}


