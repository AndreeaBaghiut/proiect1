namespace proiect1.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Unit { get; set; } //grame, lingurita, ml,...

        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }


    }
}
