namespace Migration;

public class IndexAnstallningar
{
    // Index
    public int FromIndex { get; set; }
    public int TomIndex { get; set; }
    public int BefattningsgruppIndex { get; set; }
    public int ForvaltningIndex { get; set; }
    public int WL_TYPIndex { get; set; }
    public int NotIndex { get; set; }
    public int AidIndex { get; set; }
    public int SysselsattningsgradIndex { get; set; }
    public int Disp2Index { get; set; }
    public int BefattningstextIndex { get; set; }
    public int AnstallningsnummerIndex { get; set; }
    public int AvtalIndex { get; set; }
    public int TypIndex { get; set; }
    public int FormIndex { get; set; }
    public int OrganisationsIdIndex { get; set; }
    public int TankadIndex { get; set; }

    // Längder
    public int FromLength { get; set; }
    public int TomLength { get; set; }
    public int BefattningsgruppLength { get; set; }
    public int ForvaltningLength { get; set; }
    public int WL_TYPLength { get; set; }
    public int NotLength { get; set; }
    public int AidLength { get; set; }
    public int SysselsattningsgradLength { get; set; }
    public int Disp2Length { get; set; }
    public int BefattningstextLength { get; set; }
    public int AnstallningsnummerLength { get; set; }
    public int AvtalLength { get; set; }
    public int TypLength { get; set; }
    public int FormLength { get; set; }
    public int OrganisationsIdLength { get; set; }
    public int TankadLength { get; set; }

    public IndexAnstallningar(string? L11ALe, string? L11BLe)
    {
         
        var ab1 = 6;
        var ab2 = 6;
        var ab3 = L11ALe == null ? 8 : int.Parse(L11ALe);
        var ap3 = 22;
        var qAb4 = L11BLe == null ? 15 : int.Parse(L11BLe);
        var qAp4 = ap3 + ab3 + 2;
        var ab5 = 3;
        var ap5 = qAp4 + qAb4 + 2;
        var qAb6 = 4;
        var qAp6 = ap5 + ab5 + 2;
        var qAb7 = 13;
        var qAp7 = qAp6 + qAb6 + 1;
        var qAb8 = 5;
        var qAp8 = qAp7 + qAb7 + 1;
        var qAb9 = 8;
        var qAp9 = qAp8 + qAb8 + 1 + 3 + 1;
        var qAb10 = 25;
        var qAp10 = qAp9 + qAb9 + 1;
        var ab11 = 15;
        var ap11 = qAp10 + qAb10 + 2;
        var qAb14 = 18;
        var ap12 = ap11 + ab11;
        var ab12 = 1;
        var qAp13 = ap12 + ab12;
        var qAb13 = 20;
        var qAp14 = qAp13 + qAb13;
        var qAb15 = 16;
        var qAp15 = qAp14 + qAb14;

        // Index
        FromIndex = 1;
        TomIndex = 9;
        BefattningsgruppIndex = ap3 - 5;
        ForvaltningIndex = qAp4 - 5;
        WL_TYPIndex = ap5 - 5;
        NotIndex = qAp6 - 5;
        AidIndex = qAp7 - 5;
        SysselsattningsgradIndex = qAp8 - 5;
        Disp2Index = qAp9 - 5;
        BefattningstextIndex = qAp10 - 5;
        AnstallningsnummerIndex = ap11 - 5;
        AvtalIndex = ap11 - 5 + ab11 + 1;
        TypIndex = ap11 - 5 + ab11 + 3;
        FormIndex = ap11 - 5 + ab11 + 6;
        OrganisationsIdIndex = qAp14 - 5;
        TankadIndex = qAp15 - 5;

        // Längder
        FromLength = ab1;
        TomLength = ab2;
        BefattningsgruppLength = ab3;
        ForvaltningLength = qAb4;
        WL_TYPLength = ab5;
        NotLength = qAb6;
        AidLength = qAb7;
        SysselsattningsgradLength = qAb8;
        Disp2Length = qAb9;
        BefattningstextLength = qAb10;
        AnstallningsnummerLength = ab11;
        AvtalLength = 2;
        TypLength = 2;
        FormLength = 2;
        OrganisationsIdLength = qAb14;
        TankadLength = qAb15;
    }
}

