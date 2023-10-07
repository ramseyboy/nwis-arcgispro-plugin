using CsvHelper.Configuration.Attributes;

namespace NwisApiClient.Models.Codes;

public class NwisDataCollectionTypeCode: NwisCode
{
    [Name("code")]
    public string Code { get; set; }
    [Name("label")]
    public string Label { get; set; }
}