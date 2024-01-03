using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using proiect1.Data;
using proiect1.Models;

namespace proiect1.Pages.Ingredients
{
    public class IndexModel : PageModel
    {
        private readonly proiect1.Data.proiect1Context _context;

        public IndexModel(proiect1.Data.proiect1Context context)
        {
            _context = context;
        }

        public IList<Ingredient> Ingredient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Ingredient != null)
            {
                Ingredient = await _context.Ingredient.ToListAsync();
            }
        }
    }
}
