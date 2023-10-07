namespace NwisApiClient.Parameters;

public class NwisQuery
{
    internal NwisQuery(Uri uri)
    {
        Uri = uri;
    }

    public Uri Uri { get; set; }
}