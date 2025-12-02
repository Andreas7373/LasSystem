using Domain.Enums;

namespace Domain.Entities
{
    public class Anstallning
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public DateOnly From { get; set; }
        public DateOnly? Tom { get; set; } 
        public string Anstallningsnummer { get; set; }
        public string AvtalKod { get; set; }
        public AnstallningsKlassificeringTyp AnstallningsKlassificeringTyp { get; set; }
        public string AnstallningsKlassificeringKod { get; set; }
        public AvgangsorsaksTyp AvgangsorsaksTyp { get; set; }
        public string? AvgangsorsakKod { get; set; }
        public string? OrganisationsKod { get; set; }
        public string? OrganisationsText { get; set; }

        public string? BefattningsKod { get; set; }
        public string? BefattninsText { get; set; }
        public string? BefattningsGruppering { get; set; }
        public float Sysselsattningsgrad { get; set; }
        public LoneformTyp LoneformTyp { get; set; }
        public bool Last {  get; set; }
        public bool Vilande { get; set; }

        public DateTime Regtid { get; set; }
    }
}
