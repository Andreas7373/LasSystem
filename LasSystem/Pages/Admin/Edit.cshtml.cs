using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LasSystem.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IKundRepository _kundRepository;

        public EditModel(IKundRepository kundRepository)
        {
            _kundRepository = kundRepository;
        }
        public IEnumerable<SelectListItem> ImportSystemTypList { get; set; }

        [BindProperty]
        public Domain.Entities.Kund Kund { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            ImportSystemTypList = Enum.GetValues(typeof(ImportSystemTyp))
            .Cast<ImportSystemTyp>()
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.ToString()
            });

            if (id == null)
            {
                return NotFound();
            }

            var kund =  await _kundRepository.GetByIdAsync(id.Value);
            if (kund == null)
            {
                return NotFound();
            }
            Kund = kund;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _kundRepository.UpdateAsync(Kund);

            return RedirectToPage("./Index");
        }

    }
}
