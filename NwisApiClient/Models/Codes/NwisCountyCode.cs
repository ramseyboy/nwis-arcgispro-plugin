using CsvHelper.Configuration.Attributes;

namespace NwisApiClient.Models.Codes;

public class NwisCountyCode: NwisCode
{
    [Name("county_cd")]
    public string Code { get; set; }
    [Name("county_nm")]
    public string Label { get; set; }
}