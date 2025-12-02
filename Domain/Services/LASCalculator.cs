using Domain.Entities;
using Domain.Enums;
using System.Globalization;

namespace Domain.Services
{
    public record CalcObj(DateOnly Datum, string Anstallningsnummer);
    public static class LASCalculator
    {
        private static DateOnly _omraknatDatum;
        private static AnstallningsKlassificeringTyp[] ExcludedAnstallningsTid = 
        {
            AnstallningsKlassificeringTyp.XLT,
            AnstallningsKlassificeringTyp.PAN,
            AnstallningsKlassificeringTyp.ELB,
            AnstallningsKlassificeringTyp.NaN
        };

        private static AnstallningsKlassificeringTyp[] IncludedForetrade =
        {
            AnstallningsKlassificeringTyp.VIK,
            AnstallningsKlassificeringTyp.AVA,
            AnstallningsKlassificeringTyp.SÄV,
            AnstallningsKlassificeringTyp.ÖMF,
            AnstallningsKlassificeringTyp.TVA,
            AnstallningsKlassificeringTyp.XLT,
            AnstallningsKlassificeringTyp.SÄS,
        };

        public static void BeraknaLAS(Kund kund, Person person, ManadsAckar? manadsAckar = null)
        {
            if(person.Personnummer == "197809286268")
            {

            }
            _omraknatDatum = kund.OmraknadWinLas == DateOnly.MinValue ?  DateOnly.FromDateTime(DateTime.Now) : kund.OmraknadWinLas;
            person.Reset();

            var isPensionar = person.Anstallningar.Any(a => a.AvgangsorsaksTyp == AvgangsorsaksTyp.PEN) || HarPasseratPensionsAldern(kund.Pensionsalder, person.Personnummer);
            var isTv = person.Anstallningar.Any(a => a.AnstallningsKlassificeringTyp == AnstallningsKlassificeringTyp.TVH || a.AnstallningsKlassificeringTyp == AnstallningsKlassificeringTyp.TVD);
            var egenBegäran = person.Anstallningar.Any() && person.Anstallningar.OrderBy(a => a.Tom).Last().AvgangsorsaksTyp == AvgangsorsaksTyp.EFT;

            if (isPensionar)
                person.Info = "Pensionär";
            if (isTv)
                person.Info = "Tillsvidare";
            if (egenBegäran)
                person.Info = "Slutat på egen begäran";

            //Inga anställningar
            if (person.Anstallningar.Count == 0)
            {
                person.Info += ", saknar anställningar";
                return;
            }
            var tid = new List<DateOnly>();
            var foretradeAllman = new List<CalcObj>();
            var sav = new List<CalcObj>();
            var vik = new List<CalcObj>();
            var ava = new List<CalcObj>();
            var tva = new List<CalcObj>();
            var sas = new List<CalcObj>();
            var now = _omraknatDatum;
            var sav20221001 = new DateOnly(2022, 10, 1);
            var ava20220301 = new DateOnly(2022, 3, 1);
            var femArTillbaka = now.AddYears(-5).AddDays(1);
            var treArTillbaka = now.AddYears(-3).AddDays(1);
     

            foreach (var anst in person.Anstallningar.OrderBy(a => a.From))
            {
                var klass = anst.AnstallningsKlassificeringTyp;

                if (klass == AnstallningsKlassificeringTyp.TIM)
                    continue;

                var from = anst.From;
                var tom = anst.Tom == null ? now : anst.Tom;

                for (var i = from; i <= tom; i = i.AddDays(1))
                {
                    
                    if (i <= person.HistorisktBrytDatum)
                        continue;

                    if (!ExcludedAnstallningsTid.Contains(anst.AnstallningsKlassificeringTyp))
                        tid.Add(i);

                    if (isPensionar || isTv || egenBegäran)
                        continue;

                    //Företräde allmän 
                    if (IncludedForetrade.Contains(klass))
                    {
                        if (klass == AnstallningsKlassificeringTyp.SÄV)
                        {
                            if(i >= sav20221001)
                                foretradeAllman.Add(new CalcObj(i, anst.Anstallningsnummer));
                        }
                        else
                        {
                            foretradeAllman.Add(new CalcObj(i, anst.Anstallningsnummer));
                        }
                    }

                    //Tillsvidare avslutad
                    if (klass is AnstallningsKlassificeringTyp.TVA)
                    {
                        tva.Add(new CalcObj(i, anst.Anstallningsnummer)); ;
                    }
                    //Vik 
                    else if (klass == AnstallningsKlassificeringTyp.VIK)
                    {
                        vik.Add(new CalcObj(i, anst.Anstallningsnummer));
                    }
                    //AVA
                    else if(klass == AnstallningsKlassificeringTyp.AVA)
                    {
                        ava.Add(new CalcObj(i, anst.Anstallningsnummer));
                    }
                    //Sav 
                    else if(klass == AnstallningsKlassificeringTyp.SÄV && i >= sav20221001)
                    {
                        if(!sav.Any(s => s.Datum == i))
                            sav.Add(new CalcObj(i, anst.Anstallningsnummer));
                    }
                    //Sas
                    else if (klass == AnstallningsKlassificeringTyp.SÄS)
                    {
                        sas.Add(new CalcObj(i, anst.Anstallningsnummer));
                    }
                }
            }
       
            if (sav.Count != 0)
            {
                sav = SammanhangandeDagar(sav, kund);
                foretradeAllman.AddRange(sav);

                sav.AddRange(ava.Where(a => a.Datum >= sav20221001 && a.Datum <= now));
                sav = SammanhangandeDagar(sav, kund);
                
                sav.AddRange(ava.Where(a => a.Datum >= ava20220301 && a.Datum < sav20221001));
                //var isAva2022_03_01 = ava.FirstOrDefault(a => a.Datum == ava20220301);
                //if (isAva2022_03_01 != null)
                //{
                //    var tempAva = new CalcObj(isAva2022_03_01.Datum.AddDays(-1), isAva2022_03_01.Anstallningsnummer);
                //    while(ava.FirstOrDefault(a => a.Datum == tempAva.Datum && a.Anstallningsnummer == tempAva.Anstallningsnummer) != null)
                //    {
                //        sav.Add(tempAva);
                //        tempAva  = new CalcObj(tempAva.Datum.AddDays(-1), tempAva.Anstallningsnummer);
                //    }
                //}
            }

            //foretradeAllman.AddRange(sav);

            //Behöver justera för tillsvidare som överlappar
            foreach (var calcObj in tva)
            {
                vik.RemoveAll(p => p.Datum == calcObj.Datum);
                sav.RemoveAll(p => p.Datum == calcObj.Datum);
                ava.RemoveAll(p => p.Datum == calcObj.Datum);
            }
            
            if(manadsAckar != null)
            {
                manadsAckar = new ManadsAckar
                {
                    ForetradeAllman = foretradeAllman.Select(f => f.Datum).ToList(),
                    ForetradeSava = sav.Select(s => s.Datum).ToList(),
                    KonvSav = sav.Select(s => s.Datum).ToList(),
                    KonvVik = vik.Select(v => v.Datum).ToList()
                };
            }
            var foretradeAllmanSenast = foretradeAllman.Select(f => f.Datum).DefaultIfEmpty(DateOnly.MinValue).Max();
            var foretradeSavSenaste = sav.Select(s => s.Datum).DefaultIfEmpty(DateOnly.MinValue).Max();
            var foretradeSasSenaste = sas.Select(s => s.Datum).DefaultIfEmpty(DateOnly.MinValue).Max();
            var konverteringVikSenaste = vik.Select(s => s.Datum).DefaultIfEmpty(DateOnly.MinValue).Max();
            var konverteringSavSenaste = sav.Select(s => s.Datum).DefaultIfEmpty(DateOnly.MinValue).Max();
            var konverteringAvaSenaste = ava.Select(s => s.Datum).DefaultIfEmpty(DateOnly.MinValue).Max();

            person.ForetradeAllmanSenasteAnstallningDatum = foretradeAllmanSenast;
            person.ForetradeSavSenasteAnstallningDatum = foretradeSavSenaste;
            person.ForetradeSasongSenasteAnstallningDatum = foretradeSasSenaste;
            person.KonverteringVikSenasteAnstallningDatum = konverteringVikSenaste;
            person.KonverteringSavSenasteAnstallningDatum = konverteringSavSenaste;
            person.KonverteringAvaSenasteAnstallningDatum = konverteringAvaSenaste;

            person.ForetradeAllmanIdagTid = foretradeAllman.Where(f => f.Datum >= treArTillbaka && f.Datum <= now).Select(f => f.Datum).Distinct().Count();
            person.ForetradeAllmanSenasteTid = foretradeAllman.Where(f => f.Datum >= foretradeAllmanSenast.AddYears(-3).AddDays(1) && f.Datum <= foretradeAllmanSenast).Select(f => f.Datum).Distinct().Count();   
            person.ForetradeSavIdagTid = sav.Where(f => f.Datum >= treArTillbaka && f.Datum <= now).Select(f => f.Datum).Distinct().Count();
            person.ForetradeSavSenasteTid = sav.Where(f => f.Datum >= foretradeSavSenaste.AddYears(-3).AddDays(1) && f.Datum <= foretradeSavSenaste).Select(f => f.Datum).Distinct().Count();
            x//Förertäde säsong idag tid
            //Förertäde säsong senaste tid
            person.KonverteringSavIdagTid = sav.Where(s => s.Datum >= femArTillbaka && s.Datum <= now).Select(s => s.Datum).Distinct().Count();
            person.KonverteringSavSenasteTid = sav.Where(s => s.Datum >= konverteringSavSenaste.AddYears(-5).AddDays(1) && s.Datum <= konverteringSavSenaste).Select(s => s.Datum).Distinct().Count();
            person.KonverteringVikIdagTid = vik.Where(v => v.Datum >= femArTillbaka && v.Datum <= now).Select(v => v.Datum).Distinct().Count();
            person.KonverteringVikSenasteTid = vik.Where(s => s.Datum >= konverteringVikSenaste.AddYears(-5).AddDays(1) && s.Datum <= konverteringVikSenaste).Select(s => s.Datum).Distinct().Count();
            person.KonverteringAvaIdagTid = ava.Where(v => v.Datum >= femArTillbaka && v.Datum <= now).Select(v => v.Datum).Distinct().Count();
            person.KonverteringAvaSenasteTid = ava.Where(s => s.Datum >= konverteringAvaSenaste.AddYears(-5).AddDays(1) && s.Datum <= konverteringAvaSenaste).Select(s => s.Datum).Distinct().Count();
            person.AnstallningsTid = tid.Distinct().Count();

            //Företrädesdatum säv idag
            if (person.ForetradeSavIdagTid > kund.ForetradeSavDagar)
            {
                person.ForetradeSavDatum = sav.OrderBy(s => s.Datum).Last().Datum.AddMonths(9);
            }

            //Företrädesdatum allmän idag
            if (person.ForetradeAllmanIdagTid > kund.ForetradeAllmanDagar)
            {
               person.ForetradeAllmanDatum = foretradeAllman.OrderBy(f => f.Datum).Last().Datum.AddMonths(9);
            }

            //Konvertering VIK

            //Konvertering SÄV

        }

        private static List<CalcObj> SammanhangandeDagar(List<CalcObj> list, Kund kund)
        {
           
            // Sortera listan stabilt
            list = list
                .OrderBy(x => x.Datum)
                .ThenBy(x => x.Anstallningsnummer)
                .GroupBy(x => x.Datum)                 
                .Select(g => g.First())                 
                .ToList();

            // Gruppera per år/månad
            var grupper = list
                .GroupBy(x => new { x.Datum.Year, x.Datum.Month })
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(x => x.Datum).ToList()
                );

            var resultat = new List<CalcObj>(list);

            // Funktion som räknar perioder
            int CountPeriods(List<CalcObj> items)
            {
                if (items.Count == 0) return 0;

                int periods = 1;

                for (int i = 1; i < items.Count; i++)
                {
                    bool hasDateGap = items[i].Datum.DayNumber - items[i - 1].Datum.DayNumber > 1;
                    bool anstChanged = items[i].Anstallningsnummer != items[i - 1].Anstallningsnummer;

                    if (hasDateGap || anstChanged)
                        periods++;
                }

                return periods;
            }

            foreach (var kv in grupper)
            {
                var monthItems = kv.Value;
                int periods = 0;
                if (kund.AnvandVarjeArbetsDagSomEnPerionVidSava)
                {
                    periods = monthItems.Select(m => m.Datum).Distinct().Count();
                }
                else 
                { 
                    periods = CountPeriods(monthItems); 
                }
                    

                if (periods < 3)
                    continue;

                var start = monthItems.First().Datum;
                var end = monthItems.Last().Datum;

                var expanded = new List<CalcObj>();

                int index = 0;

                for (var d = start; d <= end; d = d.AddDays(1))
                {
                    // Om datum finns redan → använd befintlig
                    if (index < monthItems.Count && monthItems[index].Datum == d)
                    {
                        expanded.Add(monthItems[index]);
                        index++;
                    }
                    else
                    {
                        // Datum saknas → fyll med närmast föregående anställningsnummer
                        string anstNr = index > 0
                            ? monthItems[index - 1].Anstallningsnummer
                            : monthItems.First().Anstallningsnummer;

                        expanded.Add(new CalcObj(d, anstNr));
                    }
                }

                resultat.AddRange(expanded.Where(e => !resultat.Any(r => r.Datum == e.Datum)));
            }

            return resultat.OrderBy(x => x.Datum).ToList();
        }

        private static bool HarPasseratPensionsAldern(int pensionsAlder, string personnummer)
        {
            string födelsedatumStr = personnummer.Substring(0, 8);

            if (DateTime.TryParseExact(födelsedatumStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime födelsedatum))
            {
                return DateTime.Now.AddYears(-pensionsAlder).Date >= födelsedatum.Date;
            }

            return false;
        }
    }
}
