using System.Net;
using System.Reflection;
using NetTopologySuite.Geometries;
using NwisApiClient.Extensions;
using NwisApiClient.Models;
using NwisApiClient.Models.Codes;
using NwisApiClient.Parameters;
using NwisApiClient.Serializers;

namespace NwisApiClient
{
    public interface INwisApi
    {
        public Task<List<NwisSite>> GetSites(NwisQuery query, CancellationToken cancellationToken = new());

        public Task<List<NwisCode>> GetStateCodes(CancellationToken cancellationToken = new());

        public Task<List<NwisCode>> GetCountyCodes(CancellationToken cancellationToken = new());

        public Task<List<NwisCode>> GetHydrologicUnitCodes(CancellationToken cancellationToken = new());
    }

    public class NwisApi: INwisApi
    {
        private readonly HttpClient _httpClient;

        public static INwisApi Create()
        {
            return new NwisApi();
        }

        private NwisApi()
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler)
            {
                DefaultRequestHeaders =
                {
                    {"Accept-Encoding", "gzip, compress"}
                }
            };
        }

        public async Task<List<NwisSite>> GetSites(NwisQuery query, CancellationToken cancellationToken = new())
        {
            var msg = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = query.Uri
            };

            var res = await _httpClient.SendAsync(msg, cancellationToken);
            return await RdbReader.ReadAsync<NwisSite>(await res.Content.ReadAsStreamAsync(cancellationToken), cancellationToken);
        }

        public async Task<List<NwisCode>> GetStateCodes(CancellationToken cancellationToken = new())
        {
            var stream = await Assembly.GetExecutingAssembly().GetResourceStream("state_codes.tsv");
            var codes = await RdbReader.ReadAsync<NwisStateCode>(stream, cancellationToken);
            return codes.Cast<NwisCode>().ToList();
        }

        public async Task<List<NwisCode>> GetCountyCodes(CancellationToken cancellationToken = new())
        {
            var stream = await Assembly.GetExecutingAssembly().GetResourceStream("county_codes.tsv");
            var codes = await RdbReader.ReadAsync<NwisCountyCode>(stream, cancellationToken);
            return codes.Cast<NwisCode>().ToList();
        }

        public async Task<List<NwisCode>> GetHydrologicUnitCodes(CancellationToken cancellationToken = new())
        {
            var stream = await Assembly.GetExecutingAssembly().GetResourceStream("hydrologic_unit_codes.tsv");
            var codes = await RdbReader.ReadAsync<NwisHydrologicUnitCodes>(stream, cancellationToken);
            return codes.Cast<NwisCode>().ToList();
        }
    }
}