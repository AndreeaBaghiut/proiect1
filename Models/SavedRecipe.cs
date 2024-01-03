using System.ComponentModel.DataAnnotations;

namespace proiect1.Models
{
    public class SavedRecipe
    {
        public int Id { get; set; }

        // Legătură către utilizatorul care a salvat rețeta
        public int? UserId { get; set; }
        public User? User { get; set; }

        // Legătură către rețeta salvată
        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        [Display(Name = "Saved Date")]
        [DataType(DataType.Date)]
        public DateTime SavedDate { get; set; }


    }
}
