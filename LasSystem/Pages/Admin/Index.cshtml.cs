using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Migration;
namespace LasSystem.Pages.Admin
{
    [IgnoreAntiforgeryToken]
    public class IndexModel(IKundRepository kundRepository, IBerakningService berakningService, MigrationWinLasDatabasService migrationService) : PageModel
    {


        public List<Domain.Entities.Kund> Kunder { get; set; }

        public async Task OnGetAsync()
        {
            Kunder = await kundRepository.GetAllAsync();
        }

        public async Task<JsonResult> OnPostMigrateAsync(Guid id)
        {
            var kund = await kundRepository.GetByIdAsync(id);
            if (kund == null) return new JsonResult(new { message = "Kund ej hittad" });

            await migrationService.Execute(kund.Id, kund.ConnectionStringWinLas);
            return new JsonResult(new { message = "Migrering klar!" });
        }

        public async Task<JsonResult> OnPostCalcAsync(Guid id)
        {
            var kund = await kundRepository.GetByIdAsync(id);
            if (kund == null) return new JsonResult(new { message = "Kund ej hittad" });

            await berakningService.BeraknaAlla(kund);
            return new JsonResult(new { message = "Beräkning klar!" });
        }

        public class IdRequest { public Guid Id { get; set; } }
    }
}
