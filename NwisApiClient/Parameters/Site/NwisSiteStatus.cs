using System.ComponentModel;

namespace NwisApiClient.Parameters.Site;

public enum NwisSiteStatus
{
    [Description("all")]
    All,
    [Description("active")]
    Active,
    [Description("inactive")]
    Inactive
}