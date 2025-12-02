namespace Domain.Entities
{
    public class WinLasData
    {
        public Guid Id { get; set; }
        
        public Guid PersonId { get; set; }
        public Person Person {get; set; }

        public int WinLasPersonid { get; set; }
        public DateOnly Senaste { get; set; }
        public int TotalAnstallningsTid { get; set; }
        public int KonverteringVikIdagTid { get; set; }
        public int KonverteringSavIdagTid { get; set; }
        public int KonverteringVikSenasteTid { get; set; }
        public int KonverteringSavSenasteTid { get; set; }
        public int ForetradeAllmanIdagTid { get; set; }
        public int ForetradeAllmanSenasteTid { get; set; }
        public int ForetradeSavSenasteTid { get; set; }
        public int ForetradeSavIdagTid { get; set; }

    }
}
