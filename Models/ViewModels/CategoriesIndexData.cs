using System.Security.Policy;

namespace proiect1.Models.ViewModels
{
    public class CategoriesIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
    }
}
