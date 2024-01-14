using System.ComponentModel.DataAnnotations.Schema;

namespace proiect1.Models
{
    public class RecipePhoto
    {
        public int Id { get; set; }

        [NotMapped] // indica ef sa ignore proprietatea in procesul de mapare a bazei de date
        public IFormFile Image { get; set; }

        // numele fisierului
        public string ImagePath { get; set; }
    }
}
