using System.ComponentModel;

namespace NwisApiClient.Parameters.Site;

public enum NwisSiteOutput
{
    [Description("basic")]
    Basic,
    [Description("expanded")]
    Expanded
}