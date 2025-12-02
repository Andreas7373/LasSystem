using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LasSystem.Pages.Admin
{
    public class ResultRow
    {
        public string Personnummer { get; set; } = string.Empty;
        public string LasSystem { get; set; } = string.Empty;
        public string WinLas { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
    }
    public class GranskaModel(IKundRepository kundRepository, IPersonRepository personRepository) : PageModel
    {
        public List<ResultRow> Resultat { get; set; } = new();

        public string Info { get; set; }
        public Domain.Entities.Kund? Kund { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public async Task OnGetAsync(Guid id)
        {
            Id = id;
            var kund = await kundRepository.GetByIdAsync(id);
            if (kund != null)
            {
                Kund = kund;
            }
        }


        public async Task<IActionResult> OnPostTidAsync()
        {

            Kund = await kundRepository.GetByIdAsync(Id);
            var personIds = await personRepository.GetAllPersonsId(Id);
            var personer = await personRepository.SelectPersonerWithWinLasDataAsync(Id, personIds);

            Resultat = new List<ResultRow>();

            var diff1 = 0;
            foreach (var person in personer.OrderBy(p => p.Personnummer))
            {
                var tid = person.AnstallningsTid + person.TillagsTid + person.HistoriskTid;

                if (tid != person.WinLasData.TotalAnstallningsTid)
                {
                    if (Math.Abs(tid - person.WinLasData.TotalAnstallningsTid) > 1)
                    {
                        var row = new ResultRow
                        {
                            Personnummer = person.Personnummer,
                            LasSystem = tid.ToString(),
                            WinLas = person.WinLasData.TotalAnstallningsTid.ToString(),
                            Info = person.Info ?? ""
                        };

                        Resultat.Add(row);
                    }
                    else
                    {
                        diff1++;
                    }
                }

            }
            Info = "Antal personer som skiljer med en dag: " + diff1;
            return Page();
        }

        public async Task<IActionResult> OnPostKonvVikIdagAsync()
        {

            Kund = await kundRepository.GetByIdAsync(Id);
            var personIds = await personRepository.GetAllPersonsId(Id);
            var personer = await personRepository.SelectPersonerWithWinLasDataAsync(Id, personIds);

            Resultat = new List<ResultRow>();

            foreach (var person in personer.OrderBy(p => p.Personnummer))
            {

                if (person.KonverteringVikIdagTid != person.WinLasData.KonverteringVikIdagTid)
                {
                    var row = new ResultRow
                    {
                        Personnummer = person.Personnummer,
                        LasSystem = person.KonverteringVikIdagTid.ToString(),
                        WinLas = person.WinLasData.KonverteringVikIdagTid.ToString(),
                        Info = person.Info ?? ""
                    };

                    Resultat.Add(row);
                }

            }
            return Page();
        }

        public async Task<IActionResult> OnPostKonvSavIdagAsync()
        {

            Kund = await kundRepository.GetByIdAsync(Id);
            var personIds = await personRepository.GetAllPersonsId(Id);
            var personer = await personRepository.SelectPersonerWithWinLasDataAsync(Id, personIds);

            Resultat = new List<ResultRow>();

            foreach (var person in personer.OrderBy(p => p.Personnummer))
            {

                if (person.KonverteringSavIdagTid != person.WinLasData.KonverteringSavIdagTid)
                {
                    var row = new ResultRow
                    {
                        Personnummer = person.Personnummer,
                        LasSystem = person.KonverteringSavIdagTid.ToString(),
                        WinLas = person.WinLasData.KonverteringSavIdagTid.ToString(),
                        Info = person.Info ?? ""
                    };

                    Resultat.Add(row);
                }

            }
            return Page();
        }

        public async Task<IActionResult> OnPostForetradeAllmanIdagAsync()
        {

            Kund = await kundRepository.GetByIdAsync(Id);
            var personIds = await personRepository.GetAllPersonsId(Id);
            var personer = await personRepository.SelectPersonerWithWinLasDataAsync(Id, personIds);

            Resultat = new List<ResultRow>();

            foreach (var person in personer.OrderBy(p => p.Personnummer))
            {

                if (person.ForetradeAllmanIdagTid != person.WinLasData.ForetradeAllmanIdagTid)
                {
                    var row = new ResultRow
                    {
                        Personnummer = person.Personnummer,
                        LasSystem = person.ForetradeAllmanIdagTid.ToString(),
                        WinLas = person.WinLasData.ForetradeAllmanIdagTid.ToString(),
                        Info = person.Info ?? ""
                    };

                    Resultat.Add(row);
                }

            }
            return Page();
        }
    }
}
