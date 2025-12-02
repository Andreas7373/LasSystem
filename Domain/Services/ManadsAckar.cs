using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ManadsAckar
    {
        public List<DateOnly> ForetradeAllman { get; set; }
        public List<DateOnly> ForetradeSava { get; set; }
        public List<DateOnly> KonvSav { get; set; }
        public List<DateOnly> KonvVik { get; set; }




        public List<(int year, int month)> Get()
        {
            var r1 = MandadsAckar(ForetradeAllman);
            var r2 = MandadsAckar(ForetradeSava);
            var r3 = MandadsAckar(KonvSav);
            var r4 = MandadsAckar(KonvVik);

            return  r1.Keys
            .Union(r2.Keys)
            .Union(r3.Keys)
            .Union(r4.Keys)
            .OrderBy(k => k.Year).ThenBy(k => k.Month)
            .ToList();

            //Console.WriteLine("{0,-10} {1,5} {2,5} {3,5} {4,5}",
            //    "Månad", "Las", "Säv", "Vik", "Ava");

            //foreach (var key in allKeys)
            //{
            //    r1.TryGetValue(key, out int v1);
            //    r2.TryGetValue(key, out int v2);
            //    r3.TryGetValue(key, out int v3);
            //    r4.TryGetValue(key, out int v4);

            //    Console.WriteLine(
            //        "{0,-10} {1,5} {2,5} {3,5} {4,5}",
            //        $"{key.Year}-{key.Month:00}",
            //        v1, v2, v3, v4
            //    );
            //}
        }

       
        private Dictionary<(int Year, int Month), int> MandadsAckar(List<DateOnly> list)
        {

            return list
                .GroupBy(d => (d.Year, d.Month))
                .ToDictionary(
                    g => g.Key,
                    g => g.Distinct().Count()
                );
        }
    }
}
