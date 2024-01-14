using System.ComponentModel.DataAnnotations;

namespace proiect1.Models
{
    public class SavedRecipe
    {
        public int Id { get; set; }

        // legatura cu utilizatorul care a salvat reteta
        public int? UserId { get; set; }
        public User? User { get; set; }

        // Legatura cu reteta salvata
        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        [Display(Name = "Saved Date")]
        [DataType(DataType.Date)]
        public DateTime SavedDate { get; set; }


    }
}
