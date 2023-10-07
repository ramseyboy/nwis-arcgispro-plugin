using System.ComponentModel;

namespace NwisApiClient.Parameters.Site;

public enum NwisSiteNameMatch
{
    [Description("any")]
    Any,
    [Description("start")]
    Start,
    [Description("exact")]
    Exact
}