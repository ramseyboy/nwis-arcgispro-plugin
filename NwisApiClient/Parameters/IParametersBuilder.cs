namespace NwisApiClient.Parameters;

public interface IParametersBuilder
{
    protected string ApiUrl { get; }

    public NwisQuery BuildQuery();
}