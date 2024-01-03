namespace proiect1.Models
{
    public class RecipeData
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<RecipeCategory> RecipeCategories { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
