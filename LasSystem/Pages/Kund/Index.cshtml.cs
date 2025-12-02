using Application.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LasSystem.Pages.Kund
{
   


    public class IndexModel(IKundRepository kundRepository, IPersonRepository personRepository) : PageModel
    {
        public Domain.Entities.Kund? Kund { get; set; }

        public Domain.Entities.Person? Person { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task OnGetAsync(Guid id, string? personnummer)
        {
            Id = id;
            var kund = await kundRepository.GetByIdAsync(id);
            if (kund != null)
            {
                Kund = kund;

                if(personnummer != null)
                {
                    personnummer = Common.LäggTillSekelsiffra(personnummer);
                    Person = await personRepository.GetByPersonnummerAsync(Kund.Id, personnummer);
                }
            }
        }
    }
}
