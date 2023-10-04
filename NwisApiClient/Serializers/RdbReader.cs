using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using NwisApiClient.Models;

namespace NwisApiClient.Serializers;

public static class RdbReader
{
    public static List<T> Read<T>(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = "\t",
            ShouldSkipRecord = row => row.Row[0].StartsWith("#") || row.Row[0].StartsWith("5s")
        };
        using var csv = new CsvReader(reader, configuration);
        return csv.GetRecords<T>().ToList();
    }
}