using CsvHelper.Configuration.Attributes;

namespace NwisApiClient.Models.Codes;

public class NwisAgencyCode: NwisCode
{
    [Name("agency_cd")]
    public string Code { get; set; }
    [Name("party_nm")]
    public string Label { get; set; }
}