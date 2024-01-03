namespace proiect1.Models
{
    public class Category //desert, cina, mic dejun,...
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }

        public ICollection<RecipeCategory>? RecipeCategories { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
