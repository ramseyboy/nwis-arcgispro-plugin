using System.Globalization;
using System.Reflection;
using NwisApiClient.Parameters;
using NwisApiClient.Parameters.Site;
using NwisApiClient.Serializers;
using Refit;

namespace NwisApiClient.Tests;

public class NwisApiTest
{
    [Fact(DisplayName = "Test get sites api")]
    public async Task TestGetSites()
    {
        var nwisApi = NwisApi.Create();

        var query = new NwisSiteParametersBuilder()
            .CountyCode("48453")
            .BuildQuery();

        var sites = await nwisApi.GetSites(query);
        Assert.NotNull(sites);
    }

    [Fact(DisplayName = "Test get county codes api")]
    public async Task TestGetCountyCodes()
    {
        var nwisApi = NwisApi.Create();
        var codes = await nwisApi.GetCountyCodes();
        Assert.NotNull(codes);
    }

    [Fact(DisplayName = "Test get state codes api")]
    public async Task TestGetStateCodes()
    {
        var nwisApi = NwisApi.Create();
        var codes = await nwisApi.GetStateCodes();
        Assert.NotNull(codes);
    }

    [Fact(DisplayName = "Test get hydrologic unit codes api")]
    public async Task TestGetHydrologicUnitCodes()
    {
        var nwisApi = NwisApi.Create();
        var codes = await nwisApi.GetHydrologicUnitCodes();
        Assert.NotNull(codes);
    }
}