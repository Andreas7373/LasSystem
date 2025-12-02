using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Migration.Models;

namespace Migration;

public class MigrationWinLasDatabasService(IPersonRepository personRepository)
{

    public async Task Execute(Guid kundId, string connectionStringWinLas)
    {
        using var contextWinLas = new WinLasDbContext(connectionStringWinLas); //WinLas databasen

        var L11ALe = contextWinLas.WLBLANDATs.Where(b => b.KOLUMN_A == "L11ALe").Select(b => b.KOLUMN_B).FirstOrDefault(); //Koordinater från WLBLANDAT
        var L11BLe = contextWinLas.WLBLANDATs.Where(b => b.KOLUMN_A == "L11BLe").Select(b => b.KOLUMN_B).FirstOrDefault(); //Koordinater från WLBLANDAT

        var index = new IndexAnstallningar(L11ALe, L11BLe); //Beräknar alla koordinater frö anställningen utifrån koordinater i WLBLANDAT

        var personIds = await personRepository.GetAllPersonsId(kundId);
        await personRepository.BulkDeletePersonerAsync(kundId, personIds); //Raderar alla personer och anställningar
       

        //Läser personer och anställningar från databasen, packar upp anställningarna med koordinaterna
        int pageSize = 100;
        int page = 0;
        var count = contextWinLas.WLPERSDATAs.Count();
        do
        {
            var rawBatchPersoner = contextWinLas.WLPERSDATAs
                .OrderBy(x => x.PERSONNR)
                .Select(x => new RawPerson
                {
                    PERSONNR = x.PERSONNR,
                    PERSONID = x.PERSONID,
                    T1 = x.T1,
                    T2 = x.T2,
                    T3 = x.T3,
                    T6 = x.T6,
                    T7 = x.T7,
                    T8 = x.T8,
                    T9 = x.T9,
                    T11 = x.T11,
                    T10 = x.T10,
                    T12 = x.T12,
                    T13 = x.T13,
                    SISTDATE = x.SISTDATE,
                    ARKDATE = x.ARKDATE
                })
                .Where(p => p.PERSONNR != "000000000000")
                .Skip(page * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();

            var rawPersonIds = rawBatchPersoner.Select(p => p.PERSONID).ToList();

            var rawBatchAnstallningar = contextWinLas.WLANSTs
                .Where(a => rawPersonIds.Contains(a.PERSONID)).
                Select(a => new RawAnstallning { PERSONID = a.PERSONID, ANST1 = a.ANST1, ANST2 = a.ANST2, ANST3 = a.ANST3, ANST4 = a.ANST4, ANST5 = a.ANST5, ANST6 = a.ANST6, ANST7 = a.ANST7, ANST8 = a.ANST8 })
                .AsNoTracking()
                .ToList();

            var listPersoner = new List<Person>();

            foreach (var personId in rawPersonIds)
            {
                var rawPerson = rawBatchPersoner.FirstOrDefault(r => r.PERSONID == personId);
                var rawAnstallningar = rawBatchAnstallningar.Where(a => a.PERSONID == personId);

                if (rawPerson != null && rawAnstallningar != null)
                {
                    var Person = CreatePersonAnstallningar(rawPerson, rawAnstallningar, index, kundId);
                    listPersoner.Add(Person);
                }
            }
            bool result = await personRepository.BulkInsertPersonerAsync(listPersoner, true);
            page++;

        } while (page * pageSize < count);
    }

    private Person CreatePersonAnstallningar(RawPerson rawPerson, IEnumerable<RawAnstallning> rawAnstallningar, IndexAnstallningar index, Guid kundId)
    {
        var person = CreatePerson(rawPerson, kundId);

        person.Anstallningar = [];

        foreach (var rawAnstallning in rawAnstallningar)
        {
            var listAnst = new[]
            {
                rawAnstallning.ANST1, rawAnstallning.ANST2, rawAnstallning.ANST3, rawAnstallning.ANST4,
                rawAnstallning.ANST5, rawAnstallning.ANST6, rawAnstallning.ANST7, rawAnstallning.ANST8
            };

            foreach (var anst in listAnst)
            {
                if (!string.IsNullOrWhiteSpace(anst))
                {
                    int notTid = 0;
                    var anstallning = CreateAnstallning(person.Id, anst, index, out notTid);
                    person.Anstallningar.Add(anstallning);
                    person.TillagsTid += notTid;
                }
            }
        }

        return person;
    }

    private Anstallning CreateAnstallning(Guid personId, string anst, IndexAnstallningar index, out int notTid)
    {
        var rawAnst = CreateRawAnst(anst, index);
        _ = int.TryParse(rawAnst.Not, out notTid);

        var newAnst = new Anstallning
        {   Id = Guid.NewGuid(),
            PersonId = personId,
            Anstallningsnummer = ParseAnstallningsnummer(rawAnst.Anstallningsnummer),
            AvgangsorsaksTyp = ParseAvgangsorsakTyp(rawAnst.Not[3]),
            AvgangsorsakKod = ParseAvgangsorsksKod(rawAnst.Not[3]),
            AnstallningsKlassificeringKod = ParseAnstallningsKlassificeringsKod(rawAnst.Typ, rawAnst.Form),
            AnstallningsKlassificeringTyp = ParseAnstallningsKlassificeringTyp(rawAnst.WL_TYP),
            AvtalKod = rawAnst.Avtal,
            From = DateOnly.ParseExact(rawAnst.From, "yyMMdd"),
            Tom = ParseTom(rawAnst),
            OrganisationsKod = rawAnst.OrganisationsKod,
            Sysselsattningsgrad = Common.ParseSysselsattningsgrad(rawAnst.Sysselsattningsgrad),
            LoneformTyp = ParseLoneform(rawAnst),
            Vilande = ParseVilande(rawAnst.Not[3]),
            Last = ParseLast(rawAnst.Not[2])
        };

    
        return newAnst;
    }

    private static DateOnly? ParseTom(RawAnst rawAnst)
    {
        if (rawAnst.WL_TYP == "TVH" || rawAnst.WL_TYP == "TVD" || rawAnst.Not[2] == 'Ö')
        {
            return  null;
        }

        return DateOnly.ParseExact(rawAnst.Tom, "yyMMdd");
    }

    private static LoneformTyp ParseLoneform(RawAnst rawAnst)
    {
        if (rawAnst.WL_TYP == "TIM")
            return LoneformTyp.Tim;
        
        else if (rawAnst.Not[0] == 'D' || rawAnst.Not[0] == 'S')
           return LoneformTyp.Dagar;
        
        return LoneformTyp.Manand;
    }

    private bool ParseVilande(char v)
    {
        return v == 'V';
    }

    private static bool ParseLast(char v)
    {
        return v == 'R';
    }

    private RawAnst CreateRawAnst(string anst, IndexAnstallningar index)
    {
        return new RawAnst
        {
            From = SafeSubstring(anst, index.FromIndex, index.FromLength),
            Tom = SafeSubstring(anst, index.TomIndex, index.TomLength),
            BefattningsGrupp = SafeSubstring(anst, index.BefattningsgruppIndex, index.BefattningsgruppLength),
            Forvaltning = SafeSubstring(anst, index.ForvaltningIndex, index.ForvaltningLength),
            WL_TYP = SafeSubstring(anst, index.WL_TYPIndex, index.WL_TYPLength),
            Not = SafeSubstring(anst, index.NotIndex, index.NotLength),
            Aid = SafeSubstring(anst, index.AidIndex, index.AidLength),
            Sysselsattningsgrad = SafeSubstring(anst, index.SysselsattningsgradIndex, index.SysselsattningsgradLength),
            Disp2 = SafeSubstring(anst, index.Disp2Index, index.Disp2Length),
            BefattningsText = SafeSubstring(anst, index.BefattningstextIndex, index.BefattningstextLength),
            Anstallningsnummer = SafeSubstring(anst, index.AnstallningsnummerIndex, index.AnstallningsnummerLength),
            Avtal = SafeSubstring(anst, index.AvtalIndex, index.AvtalLength),
            Typ = SafeSubstring(anst, index.TypIndex, index.TypLength),
            Form = SafeSubstring(anst, index.FormIndex, index.FormLength),
            OrganisationsKod = SafeSubstring(anst, index.OrganisationsIdIndex, index.OrganisationsIdLength),
            Tankad = SafeSubstring(anst, index.TankadIndex, index.TankadLength)
        };
    }

    private static string ParseAnstallningsnummer(string nummer)
    {
        if (nummer.Contains('_'))
            return nummer.Split('_')[0];
        return nummer.Trim();
    }
    private static string ParseAnstallningsKlassificeringsKod(string typ, string form)
    {
        if (string.IsNullOrWhiteSpace(typ) && string.IsNullOrWhiteSpace(form))
            return string.Empty;
        if (string.IsNullOrEmpty(form) && !string.IsNullOrEmpty(typ))
            return typ.Trim();
        if (!string.IsNullOrWhiteSpace(form) && string.IsNullOrWhiteSpace(typ))
            return form.Trim();
        return typ.Trim() + '-' + form.Trim();
    }

    private static string ParseAvgangsorsksKod(char c)
    {
        if (c == 'E')
            return "E";
        if (c == 'P')
            return "P";
        return string.Empty;
    }
    public static AvgangsorsaksTyp ParseAvgangsorsakTyp(char c)
    {
        if (c == 'P')
            return AvgangsorsaksTyp.PEN;
        if (c == 'E')
            return AvgangsorsaksTyp.EFT;
        return AvgangsorsaksTyp.NaN;
    }
    private Person CreatePerson(RawPerson rawPerson, Guid kundId)
    {
        var personId = Guid.NewGuid();
        var historisktBrytDatum = rawPerson.ARKDATE.HasValue
                ? DateOnly.ParseExact(rawPerson.ARKDATE.Value.ToString("D8"), "yyyyMMdd")
                : DateOnly.MinValue;
        if (historisktBrytDatum == new DateOnly(1949, 12, 31))
            historisktBrytDatum = DateOnly.MinValue;

        return new Person
        {
            Id = personId,
            Personnummer = rawPerson.PERSONNR,
            KundId = kundId,
            HistoriskTid = rawPerson.T1 ?? 0,
            TillagsTid = rawPerson.T2 ?? 0,
            HistorisktBrytDatum = historisktBrytDatum,
            WinLasData = new WinLasData
            {
                Id = Guid.NewGuid(),
                PersonId = personId,
                WinLasPersonid = rawPerson.PERSONID,
                TotalAnstallningsTid = rawPerson.T3 ?? 0,
                KonverteringVikIdagTid = rawPerson.T10 ?? 0,
                KonverteringSavIdagTid = rawPerson.T9 ?? 0,
                KonverteringVikSenasteTid = rawPerson.T7 ?? 0,
                KonverteringSavSenasteTid = rawPerson.T8 ?? 0,
                ForetradeAllmanSenasteTid = rawPerson.T6 ?? 0,
                ForetradeAllmanIdagTid = rawPerson.T11 ?? 0,
                ForetradeSavSenasteTid = rawPerson.T12 ?? 0,
                ForetradeSavIdagTid = rawPerson.T13 ?? 0,
                Senaste = Common.ParseIntToDate(rawPerson.SISTDATE)
            }

        };
    }

    private static string SafeSubstring(string input, int startIndex, int length)
    {
        int realIndex = Math.Max(startIndex - 1, 0);
        if (realIndex >= input.Length)
            return string.Empty;

        if (realIndex + length > input.Length)
            length = input.Length - realIndex;

        return input.Substring(realIndex, length);
    }

    public static AnstallningsKlassificeringTyp ParseAnstallningsKlassificeringTyp(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return AnstallningsKlassificeringTyp.NaN;

        // Försök matcha exakt enum-namn (inkl. svenska bokstäver)
        if (Enum.TryParse<AnstallningsKlassificeringTyp>(value, ignoreCase: false, out var result))
            return result;

        return AnstallningsKlassificeringTyp.NaN;
    }
}
