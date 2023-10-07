using CsvHelper.Configuration.Attributes;

namespace NwisApiClient.Models.Codes;

public class NwisHydrologicUnitCodes: NwisCode
{
    [Name("huc")]
    public string Code { get; set; }
    [Name("basin")]
    public string Label { get; set; }
}