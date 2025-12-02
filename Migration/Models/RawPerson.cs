namespace Migration.Models
{
    public class RawPerson
    {
        public string PERSONNR { get; set; }
        public int PERSONID { get; set; }
        public short? T1 { get; set; }
        public short? T2 { get; set; }
        public short? T3 { get; set; } //Anställningstid
        public short? T10 { get; set; } //KonverteringVikIdagTid
        public short? T9 { get; set; } //KonverteringSavIdagTid
        public short? T7 { get; set; } //KonverteringVikSenasteTid
        public short? T8 { get; set; } //KonverteringSavSenasteTid
        public short? T6 { get; set; } //ForetradeAllmanSenasteTid
        public short? T11 { get; set; } //ForetradeAllmanIdagTid
        public short? T12 { get; set; } //ForetradeSavSenasteTid
        public short? T13 { get; set; } //ForetradeSavIdagTid
        public int? SISTDATE { get; set; } //SenatsteAnstallning
        public int? ARKDATE { get; set; } //Historiskt bryt
    }
}
