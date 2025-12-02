using System.Globalization;

namespace Migration;

public class Common
{
    public static bool ÄrSkottårsdag(DateTime datum)
    {
        return datum.Month == 2 && datum.Day == 29 && DateTime.IsLeapYear(datum.Year);
    }
    public static DateOnly ParseDate(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length != 6)
            throw new ArgumentException("Felaktigt datumformat, förväntar mig 'yymmdd'.");

        int year = int.Parse(input.Substring(0, 2));
        int month = int.Parse(input.Substring(2, 2));
        int day = int.Parse(input.Substring(4, 2));

        // Antag att årtal < 50 hör till 2000-talet, annars 1900-talet
        year += year < 50 ? 2000 : 1900;

        return new DateOnly(year, month, day);
    }
    public static float ParseSysselsattningsgrad(string sysselsattningsgrad)
    {
        if (float.TryParse(sysselsattningsgrad, out float value))
            return value;
        return 0;
    }
    public static string SafeSubstring(string input, int startIndex, int length)
    {
        // Delphi-index startar på 1 → C# startar på 0
        int realIndex = Math.Max(startIndex - 1, 0);
        if (realIndex >= input.Length)
            return string.Empty;

        if (realIndex + length > input.Length)
            length = input.Length - realIndex;

        return input.Substring(realIndex, length);
    }

    public static DateOnly ParseIntToDate(int? dateInt)
    {
        if (dateInt == null) 
            return DateOnly.MinValue;
        string dateString = dateInt.Value.ToString();
        if (DateOnly.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date))
        {
            return date;
        }
        else
        {
            return DateOnly.MinValue;
        }
    }

    public static bool HarPasserat68(string personnummer)
    {
        // Ta ut de första 8 tecknen som födelsedatum (yyyyMMdd)
        string födelsedatumStr = personnummer.Substring(0, 8);

        // Försök tolka datumet
        if (DateTime.TryParseExact(födelsedatumStr, "yyyyMMdd", CultureInfo.InvariantCulture,
                                   DateTimeStyles.None, out DateTime födelsedatum))
        {
            return DateTime.Now.AddYears(-68).Date >= födelsedatum.Date;
        }

        // Om personnumret inte går att tolka — returnera false eller hantera särskilt
        return false;
    }
}
