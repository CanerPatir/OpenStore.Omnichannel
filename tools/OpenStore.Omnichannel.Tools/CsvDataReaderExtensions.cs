using Sylvan.Data.Csv;

namespace OpenStore.Omnichannel.Tools;

static class CsvDataReaderExtensions
{
    public static string GetStringOrDefault(this CsvDataReader csv, int ordinal)
    {
        var val = csv.GetString(ordinal);
        return val == string.Empty ? default : val;
    }

    public static decimal? GetDecimalOrDefault(this CsvDataReader csv, int ordinal)
    {
        var str = csv.GetStringOrDefault(ordinal);

        if (string.IsNullOrWhiteSpace(str))
            return default;

        var val = csv.GetDecimal(ordinal);
        return val == default ? default : val;
    }
    
    public static TEnum GetEnum<TEnum>(this CsvDataReader csv, int ordinal) where TEnum : struct, Enum
    {
        var val = csv.GetString(ordinal);
        var values = Enum.GetValues<TEnum>();
        return values.First(v => Enum.GetName(v) == val);
    }
}