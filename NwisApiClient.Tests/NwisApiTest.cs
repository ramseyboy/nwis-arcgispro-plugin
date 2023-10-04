using System.Globalization;
using System.Reflection;
using NwisApiClient.Serializers;
using Refit;

namespace NwisApiClient.Tests;

public class NwisApiTest
{
    [Fact(DisplayName = "Test get sites api")]
    public async Task TestGetSites()
    {
        var nwisApi = NwisApi.Create();
        var sites = await nwisApi.GetSites("tx");
        Assert.NotNull(sites);
    }

    [Fact(DisplayName = "Test get sites api sync")]
    public void TestGetSitesSync()
    {
        var nwisApi = NwisApi.Create();
        var sites = Task.Run(() => nwisApi.GetSites("tx")).Result;
        Assert.NotNull(sites);
    }
}