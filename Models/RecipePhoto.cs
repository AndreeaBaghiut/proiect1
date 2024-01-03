using System.ComponentModel.DataAnnotations.Schema;

namespace proiect1.Models
{
    public class RecipePhoto
    {
        public int Id { get; set; }

        [NotMapped] // Aceasta indica EF să ignore proprietatea în procesul de mapare a bazei de date
        public IFormFile Image { get; set; }

        // Proprietatea pentru numele fișierului
        public string ImagePath { get; set; }
    }
}
