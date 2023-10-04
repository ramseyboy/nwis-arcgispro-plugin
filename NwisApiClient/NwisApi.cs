using NwisApiClient.Models;
using NwisApiClient.Serializers;
using Refit;

namespace NwisApiClient
{
    public interface INwisApi
    {
        public Task<List<Site>> GetSites(string state);
    }

    public class NwisApi: INwisApi
    {
        private const string ApiUrl = "https://waterservices.usgs.gov/nwis";
        //private readonly INwisApiInternal _nwisApiInternal = RestService.For<INwisApiInternal>(ApiUrl);

        public static INwisApi Create()
        {
            return new NwisApi();
        }

        private NwisApi()
        {

        }

        public async Task<List<Site>> GetSites(string state)
        {
            UriBuilder builder = new UriBuilder(ApiUrl + "/site");
            builder.Query = $"stateCd={state}&format=rdb";
            var uri = builder.Uri;

            var res = await new HttpClient().GetAsync(uri).ConfigureAwait(false);
            return RdbReader.Read<Site>(await res.Content.ReadAsStreamAsync().ConfigureAwait(false));
        }
    }

    internal interface INwisApiInternal
    {
        [Get("/site/?stateCd={state}&format=rdb")]
        internal Task<HttpResponseMessage> GetSites(string state);
    }
}