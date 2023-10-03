using System.Globalization;
using NwisApiClient.Serializers;
using Refit;

namespace NwisApiClient.Tests;

public class NwisApiTest
{
    [Fact(DisplayName = "Test get sites api")]
    public async Task TestGetSites()
    {
        var nwisApi = new NwisApi();
        var sites = await nwisApi.GetSites("tx");
        Assert.NotNull(sites);
    }
}