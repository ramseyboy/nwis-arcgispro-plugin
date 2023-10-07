using System.Text;
using NwisApiClient.Models.Codes;

namespace NwisApiClient.Parameters;

public abstract class NwisCommonParametersBuilder<T>: IParametersBuilder where T: IParametersBuilder
{
    public string ApiUrl => "https://waterservices.usgs.gov/nwis";

    [NwisQueryParameter("outputDataTypeCd", NwisParameterType.Output)]
    protected NwisDataCollectionTypeCode? _dataCollectionTypeCode;

    [NwisQueryParameter("format", NwisParameterType.Output)]
    protected string? _format = "rdb";

    [NwisQueryParameter("seriesCatalogOutput", NwisParameterType.Output)]
    protected bool _seriesCatalogOutput = false;

    public abstract T DataCollectionTypeCode(NwisDataCollectionTypeCode dataCollectionTypeCode);

    public abstract T SeriesCatalogOutput(bool seriesCatalogOutput);

    public abstract NwisQuery BuildQuery();

    protected string BuildCommonParameters()
    {
        var sb = new StringBuilder();
        return sb.ToString();
    }
}