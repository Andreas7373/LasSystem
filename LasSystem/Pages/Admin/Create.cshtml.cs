using Application.Interfaces;
using Domain.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LasSystem.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly IKundRepository _kundRepository;

        public CreateModel(IKundRepository kundRepository)
        {
            _kundRepository = kundRepository;
        }
        public IEnumerable<SelectListItem> ImportSystemTypList { get; set; }
        public IActionResult OnGet()
        {
            ImportSystemTypList = Enum.GetValues(typeof(ImportSystemTyp))
           .Cast<ImportSystemTyp>()
           .Select(e => new SelectListItem
           {
               Value = e.ToString(),
               Text = e.ToString()
           });

            Kund = new Domain.Entities.Kund
            {
                Personer = [],
                Namn = "Test",
                ImportSystemTyp = ImportSystemTyp.Winlas,
                BrytDatum = DateOnly.MaxValue,
                OmraknadWinLas = DateOnly.FromDateTime(DateTime.Now),
                ConnectionStringWinLas = "Server=.\\SQLEXPRESS;Database=XXXXXXXX;Trusted_Connection=True;TrustServerCertificate=True;"
            };
            return Page();
        }

        [BindProperty]
        public Domain.Entities.Kund Kund { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _kundRepository.CreateAsync(Kund);

            return RedirectToPage("./Index");
        }
    }
}
