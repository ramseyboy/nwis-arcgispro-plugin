using CsvHelper.Configuration.Attributes;

namespace NwisApiClient.Models.Codes;

public class NwisSiteType: NwisCode
{
    [Name("name")]
    public string Code { get; set; }
    [Name("long_name")]
    public string Label { get; set; }
    [Name("description")]
    public string Description { get; set; }
}