using System.Text;
using NwisApiClient.Models;
using NwisApiClient.Serializers;
using Refit;

namespace NwisApiClient.Tests;

public class RdpReaderTest
{
    [Theory(DisplayName = "Test get sites rdb")]
    [EmbeddedResourceData("NwisApiClient.Tests/Resources/sites.rdb")]
    public void TestReadRdb(string content)
    {
        var stream = new MemoryStream(Encoding.ASCII.GetBytes(content));
        var sites = RdbReader.Read<Site>(stream);
        Assert.NotNull(sites);
    }
}