using proiect1.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace proiect1.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }
        public Recipe? Recipe { get; set; }

        public int IngredientID { get; set; }
        public Ingredient? Ingredient { get; set; }
        [Column(TypeName = "decimal(6, 0)")]
        public decimal Quantity { get; set; }
    }
}

