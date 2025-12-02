namespace Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public Guid KundId { get; set; }
        public Kund Kund { get; set; }

        public List<Anstallning> Anstallningar { get; set; }
        public string Personnummer { get; set; }
        public string? Info { get; set; }

        public DateOnly HistorisktBrytDatum { get; set; } = DateOnly.MinValue;
        public int HistoriskTid { get; set; }
        public int TillagsTid { get; set; }
        public int AnstallningsTid { get; set; }

        //Företräde tid
        public int ForetradeAllmanIdagTid { get; set; }
        public int ForetradeSavIdagTid { get; set; }
        public int ForetradeSasongIdagTid { get; set; }
        public int ForetradeAllmanSenasteTid { get; set; }
        public int ForetradeSavSenasteTid { get; set; }     
        public int ForetradeSasongSenasteTid { get; set; }


        //Företräde datum
        public DateOnly ForetradeAllmanDatum { get; set; } = DateOnly.MinValue;
        public DateOnly ForetradeSavDatum { get; set; } = DateOnly.MinValue;
        public DateOnly ForetradeSasongDatum { get; set; } = DateOnly.MinValue;

        //Företräde senaste anställning datum
        public DateOnly ForetradeAllmanSenasteAnstallningDatum { get; set; } = DateOnly.MinValue;
        public DateOnly ForetradeSavSenasteAnstallningDatum { get; set; } = DateOnly.MinValue;
        public DateOnly ForetradeSasongSenasteAnstallningDatum { get; set; } = DateOnly.MinValue;

        //Konvertering tid

        public int KonverteringSavIdagTid { get; set; }
        public int KonverteringVikIdagTid { get; set; }
        public int KonverteringAvaIdagTid { get; set; }

        public int KonverteringSavSenasteTid { get; set; }
        public int KonverteringVikSenasteTid { get; set; }
        public int KonverteringAvaSenasteTid { get; set; }

        //Konvertering Datum
        public DateOnly KonverteringSavDatum { get; set; } = DateOnly.MinValue;
        public DateOnly KonverteringVikDatum { get; set; } = DateOnly.MinValue;
        public DateOnly KonverteringAvaDatum { get; set; } = DateOnly.MinValue;

        //Konvertering senaste anställning datum
        public DateOnly KonverteringSavSenasteAnstallningDatum { get; set; } = DateOnly.MinValue;
        public DateOnly KonverteringVikSenasteAnstallningDatum { get; set; } = DateOnly.MinValue;
        public DateOnly KonverteringAvaSenasteAnstallningDatum { get; set; } = DateOnly.MinValue;

        public WinLasData WinLasData { get; set; }
        public void Reset()
        {
            AnstallningsTid = 0;

            ForetradeAllmanIdagTid = 0;
            ForetradeSavIdagTid = 0;
            ForetradeSasongIdagTid = 0;
            ForetradeAllmanSenasteTid = 0;
            ForetradeSavSenasteTid = 0;
            ForetradeSasongSenasteTid = 0;

            ForetradeAllmanDatum = DateOnly.MinValue;
            ForetradeSavDatum = DateOnly.MinValue;
            ForetradeSasongDatum = DateOnly.MinValue;

            ForetradeAllmanSenasteAnstallningDatum = DateOnly.MinValue;
            ForetradeSavSenasteAnstallningDatum = DateOnly.MinValue;
            ForetradeSasongSenasteAnstallningDatum = DateOnly.MinValue;

            KonverteringSavIdagTid = 0;
            KonverteringVikIdagTid = 0;
            KonverteringAvaIdagTid = 0;
            KonverteringSavSenasteTid = 0;
            KonverteringVikSenasteTid = 0;
            KonverteringAvaSenasteTid = 0;

            KonverteringSavDatum = DateOnly.MinValue;
            KonverteringVikDatum = DateOnly.MinValue;
            KonverteringAvaDatum = DateOnly.MinValue;

            KonverteringSavSenasteAnstallningDatum = DateOnly.MinValue;
            KonverteringVikSenasteAnstallningDatum = DateOnly.MinValue;
            KonverteringAvaSenasteAnstallningDatum = DateOnly.MinValue;

            Info = "";
        }
    }
}
