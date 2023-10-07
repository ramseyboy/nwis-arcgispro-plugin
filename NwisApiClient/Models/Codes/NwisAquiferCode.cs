using CsvHelper.Configuration.Attributes;

namespace NwisApiClient.Models.Codes;

/// <summary>
/// N or S, where N = aquifer, S = aquifer system
///
/// 3 digit number = rock type (4 digits used for "Other" to fill out the code)
///
/// 100-- Sand and gravel aquifers
/// 200--Semiconsolidated sand aquifers (not used)
/// 300--Sandstone aquifers
/// 400--Carbonate-rock aquifers
/// 500--Sandstone and carbonate-rock aquifers
/// 600--Igneous and metamorphic-rock aquifers
/// 9999--Areas that are not a national aquifer
///
/// 6 character identifier derived from the aquifer name using the code derivation procedure also used for GWSI geohydrologic unit names. (Note that the National Aquifer Code uses a different numeric component than the geohydrologic unit names.)
/// </summary>
public class NwisAquiferCode: NwisCode
{
    [Name("code")]
    public string Code { get; set; }
    [Name("aquifer_name")]
    public string Label { get; set; }
    [Name("valid_states")]
    public string ValidStates { get; set; }
}