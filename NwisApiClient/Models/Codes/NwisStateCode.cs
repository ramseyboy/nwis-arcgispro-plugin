using CsvHelper.Configuration.Attributes;

namespace NwisApiClient.Models.Codes;

public class NwisStateCode: NwisCode
{
    [Name("id")]
    public string Code { get; set; }
    [Name("code")]
    public string Label { get; set; }
    [Name("name")]
    public string Description { get; set; }
}