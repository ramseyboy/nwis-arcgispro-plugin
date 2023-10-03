using NwisApiClient.Models;
using NwisApiClient.Serializers;
using Refit;

namespace NwisApiClient
{
    public interface INwisApi
    {
        public Task<IEnumerable<Site>> GetSites(string state);
    }

    public class NwisApi: INwisApi
    {
        private const string ApiUrl = "https://waterservices.usgs.gov/nwis";
        private readonly INwisApiInternal _nwisApiInternal = RestService.For<INwisApiInternal>(ApiUrl);

        public async Task<IEnumerable<Site>> GetSites(string state)
        {
            var res = await _nwisApiInternal.GetSites(state);
            return await RdbReader.ReadAsync<Site>(await res.Content.ReadAsStreamAsync());
        }
    }

    internal interface INwisApiInternal
    {
        [Get("/site/?stateCd={state}&format=rdb")]
        internal Task<HttpResponseMessage> GetSites(string state);
    }
}