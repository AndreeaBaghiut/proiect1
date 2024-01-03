using System.ComponentModel.DataAnnotations;

namespace proiect1.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Display(Name = "Recipe Title")]
        public string? Title { get; set; }

        [Display(Name = "Recipe Description")]
        public string? Description { get; set; }

        [Display(Name = "Recipe Instructions")]
        public string? Instructions { get; set; }

       // [Display(Name = "Recipe Photo")]
        public string? Photo { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }

        public int? SavedRecipeId { get; set; }
        public SavedRecipe? SavedRecipe { get; set; }
        public ICollection<RecipeCategory>? RecipeCategories { get; set; }

        public int? UserId { get; set; }

    }
}
