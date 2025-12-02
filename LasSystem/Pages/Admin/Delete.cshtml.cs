using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LasSystem.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly IKundRepository _kundRepository;

        public DeleteModel(IKundRepository kundRepository)
        {
            _kundRepository = kundRepository;
        }

        [BindProperty]
        public Domain.Entities.Kund Kund { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kund = await _kundRepository.GetByIdAsync(id.Value);

            if (kund is not null)
            {
                Kund = kund;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kund = await _kundRepository.GetByIdAsync(id.Value);
            if (kund is not null)
            {
                await _kundRepository.DeleteAsync(kund.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
