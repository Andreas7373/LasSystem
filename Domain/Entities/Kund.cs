using Domain.Enums;

namespace Domain.Entities
{
    public class Kund
    {
        public Guid Id { get; set; }
        public string Namn {  get; set; }

        public List<Person> Personer { get; set; } = new List<Person>();
        public ImportSystemTyp ImportSystemTyp { get; set; }
        public DateOnly BrytDatum { get; set; } = DateOnly.MinValue;
        public string ConnectionStringWinLas { get; set; }
        public DateOnly OmraknadWinLas { get; set; }

        //Inställningar
        public bool AnvandVarjeArbetsDagSomEnPerionVidSava { get; set; }
        public int Pensionsalder { get; set; } = 68;
        public int ForetradeAllmanDagar { get; set; } = 360;
        public int ForetradeSavDagar { get; set; } = 270;
        public int KonverteringLASDagar { get; set; } = 730;
        public int KonverteringSavDagar { get; set; } = 360;
        public int KonverteringVikDagar { get; set; } = 548;

    }
}
