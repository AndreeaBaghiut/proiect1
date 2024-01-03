using System.ComponentModel.DataAnnotations;

namespace proiect1.Models
{
    public class User
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Username-ul trebuie sa inceapa cu majuscula")]

        [StringLength(30, MinimumLength = 5)]
        public int? UserName { get; set; }
        public string Email { get; set; }
        public ICollection<SavedRecipe>? SavedRecipes { get; set; }
    }
}
