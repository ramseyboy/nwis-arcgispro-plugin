using System.Reflection;
using System.Text;
using NetTopologySuite.Geometries;
using NwisApiClient.Exceptions;
using NwisApiClient.Extensions;
using NwisApiClient.Models.Codes;

namespace NwisApiClient.Parameters.Site;

public class NwisSiteParametersBuilder: NwisCommonParametersBuilder<NwisSiteParametersBuilder>
{
    [NwisQueryParameter("stateCd", NwisParameterType.Major)]
    private NwisStateCode? _stateCode;

    [NwisQueryParameter("countyCd", NwisParameterType.Major)]
    private ICollection<string>? _countyCodes;

    [NwisQueryParameter("huc", NwisParameterType.Major)]
    private ICollection<string>? _hydrologicUnitCodes;

    [NwisQueryParameter("sites", NwisParameterType.Major)]
    private ICollection<string>? _siteNumbers;

    [NwisQueryParameter("bbox", NwisParameterType.Major)]
    private Envelope? _boundingBox;

    [NwisQueryParameter("siteOutput", NwisParameterType.Output)]
    private NwisSiteOutput _siteOutput = NwisSiteOutput.Basic;

    [NwisQueryParameter("startDt", NwisParameterType.Minor)]
    private DateTime? _startDate;

    [NwisQueryParameter("endDt", NwisParameterType.Minor)]
    private DateTime? _endDate;

    [NwisQueryParameter("period", NwisParameterType.Minor)]
    private TimeSpan? _period;

    [NwisQueryParameter("modifiedSince", NwisParameterType.Minor)]
    private TimeSpan? _modifiedSince;

    [NwisQueryParameter("siteName", NwisParameterType.Minor)]
    private string? _siteName;

    [NwisQueryParameter("siteNameMatchOperator", NwisParameterType.Minor)]
    private NwisSiteNameMatch? _siteNameMatchOperator = NwisSiteNameMatch.Start;

    [NwisQueryParameter("siteStatus", NwisParameterType.Minor)]
    private NwisSiteStatus _siteStatus = NwisSiteStatus.All;

    [NwisQueryParameter("siteType", NwisParameterType.Minor)]
    private List<NwisSiteType>? _siteTypes;

    [NwisQueryParameter("hasDataTypeCd", NwisParameterType.Minor)]
    private NwisDataCollectionTypeCode? _hasDataTypeCode;

    [NwisQueryParameter("parameterCd", NwisParameterType.Minor)]
    private NwisParameterCode? _parameterCode;

    [NwisQueryParameter("agencyCd", NwisParameterType.Minor)]
    private NwisAgencyCode? _agencyCode;

    [NwisQueryParameter("altMin", NwisParameterType.Minor)]
    private double? _altMin;

    [NwisQueryParameter("altMax", NwisParameterType.Minor)]
    private double? _altMax;

    [NwisQueryParameter("drainAreaMin", NwisParameterType.Minor)]
    private double? _drainAreaMin;

    [NwisQueryParameter("drainAreaMax", NwisParameterType.Minor)]
    private double? _drainAreaMax;

    [NwisQueryParameter("aquiferCd", NwisParameterType.Minor)]
    private NwisAquiferCode? _aquiferCode;

    [NwisQueryParameter("localAquiferCd", NwisParameterType.Minor)]
    private NwisLocalAquiferCode? _localAquiferCode;

    [NwisQueryParameter("wellDepthMin", NwisParameterType.Minor)]
    private double? _wellDepthMin;

    [NwisQueryParameter("wellDepthMax", NwisParameterType.Minor)]
    private double? _wellDepthMax;

    [NwisQueryParameter("holeDepthMin", NwisParameterType.Minor)]
    private double? _holeDepthMin;

    [NwisQueryParameter("holeDepthMax", NwisParameterType.Minor)]
    private double? _holeDepthMax;

    public NwisSiteParametersBuilder()
    {
    }

    public NwisSiteParametersBuilder StateCode(NwisStateCode stateCode)
    {
        _stateCode = stateCode;
        return this;
    }

    public NwisSiteParametersBuilder CountyCode(params string[] countyCodes)
    {
        if (countyCodes is null || countyCodes.Length == 0 || countyCodes.Any(string.IsNullOrEmpty))
        {
            throw new NwisParameterException("countyCodes parameter cannot be empty", nameof(countyCodes));
        }

        if (countyCodes.Length > 20)
        {
            throw new NwisParameterException($"Cannot pass more than 20 countyCodes, found {countyCodes.Length}", nameof(countyCodes));
        }

        _countyCodes = countyCodes;
        return this;
    }

    public NwisSiteParametersBuilder HydrologicUnitCode(params string[] hydrologicUnitCodes)
    {
        if (hydrologicUnitCodes is null || hydrologicUnitCodes.Length == 0 || hydrologicUnitCodes.Any(string.IsNullOrEmpty))
        {
            throw new NwisParameterException("hydrologicUnitCodes parameter cannot be empty or contain empty strings", nameof(hydrologicUnitCodes));
        }

        var majorCodeCount = hydrologicUnitCodes.Count(c => c.Length == 2);
        var minorCodeCount = hydrologicUnitCodes.Count(c => c.Length == 8);

        if (majorCodeCount == 0 && minorCodeCount == 0)
        {
            throw new NwisParameterException(
                $@"hydrologicUnitCodes must be 2 chars for major code 
                        and 8 chars for minor code, found no valid codes 
                        in {string.Join(',', hydrologicUnitCodes)}", nameof(hydrologicUnitCodes));
        }

        if (majorCodeCount > 1)
        {
            throw new NwisParameterException(
                $@"Only allowed 1 major 2 digit hydrologicUnitCode, found {majorCodeCount}", nameof(hydrologicUnitCodes));
        }

        if (minorCodeCount > 10)
        {
            throw new NwisParameterException(
                $@"Only allowed 10 minor 8 digit hydrologicUnitCode, found {minorCodeCount}", nameof(hydrologicUnitCodes));
        }

        _hydrologicUnitCodes = hydrologicUnitCodes;
        return this;
    }

    public NwisSiteParametersBuilder SiteNumbers(params string[] siteNumbers)
    {
        if (siteNumbers is null || siteNumbers.Length == 0 || siteNumbers.Any(string.IsNullOrEmpty))
        {
            throw new NwisParameterException("siteNumbers parameter cannot be empty", nameof(siteNumbers));
        }
        _siteNumbers = siteNumbers;
        return this;
    }

    public NwisSiteParametersBuilder BoundingBox(Envelope boundingBox)
    {
        _boundingBox = boundingBox;
        return this;
    }

    public NwisSiteParametersBuilder SiteOutput(NwisSiteOutput siteOutput)
    {
        _siteOutput = siteOutput;
        return this;
    }

    public override NwisSiteParametersBuilder DataCollectionTypeCode(NwisDataCollectionTypeCode dataCollectionTypeCode)
    {
        _dataCollectionTypeCode = dataCollectionTypeCode;
        return this;
    }

    public override NwisSiteParametersBuilder SeriesCatalogOutput(bool seriesCatalogOutput)
    {
        _seriesCatalogOutput = seriesCatalogOutput;
        return this;
    }

    public override NwisQuery BuildQuery()
    {
        var fieldDict = GetType().GetFields()
            .Where(f => f.GetValue(this) is not null)
            .ToDictionary(
                fi => fi.Name,
                fi => fi.GetCustomAttribute(typeof(NwisQueryParameterAttribute)) as NwisQueryParameterAttribute ?? throw new NwisParameterException("Nwis Parameters must be annotation with the 'NwisQueryParameter' attribute"));

        var majorParamsCount = fieldDict.Values.Count(f => f.ParameterType == NwisParameterType.Major);
        if (majorParamsCount > 1)
        {
            var agg = fieldDict.Values
                .Where(f => f.ParameterType == NwisParameterType.Major)
                .Select(f => f.Name)
                .ToList();
            throw new NwisParameterException(
            $@"Only 1 major parameter allowed in NWIS requests,
                    found {majorParamsCount}.Choose 1 of {string.Join(',', agg)}");
        }

        var sb = new StringBuilder();
        if (_countyCodes is not null && _countyCodes.Count > 0)
        {
            sb.Append($"{fieldDict[nameof(_countyCodes)].Name}={string.Join(',', _countyCodes.Select(c => !string.IsNullOrEmpty(c)))}");
            sb.Append('&');
        }

        if (_stateCode is not null && !string.IsNullOrEmpty(_stateCode.Code))
        {
            sb.Append($"{fieldDict[nameof(_stateCode)]}={string.Join(',', _stateCode.Code)}");
            sb.Append('&');
        }

        if (_hydrologicUnitCodes is not null && _hydrologicUnitCodes.Count > 0)
        {
            sb.Append($"{fieldDict[nameof(_hydrologicUnitCodes)].Name}={string.Join(',', _hydrologicUnitCodes.Select(c => !string.IsNullOrEmpty(c)))}");
            sb.Append('&');
        }

        if (_siteNumbers is not null && _siteNumbers.Count > 0)
        {
            sb.Append($"{fieldDict[nameof(_siteNumbers)].Name}={string.Join(',', _siteNumbers.Select(c => !string.IsNullOrEmpty(c)))}");
            sb.Append('&');
        }

        if (_boundingBox is not null && _boundingBox.Area != 0.0)
        {
            sb.Append($"{fieldDict[nameof(_stateCode)]}={_boundingBox.MinX},{_boundingBox.MinY},{_boundingBox.MaxX},{_boundingBox.MaxY}");
            sb.Append('&');
        }

        sb.Append($"{fieldDict[nameof(_siteOutput)]}={_siteOutput.GetDescription()}");

        sb.Append(BuildCommonParameters());

        var builder = new UriBuilder(ApiUrl + "/site");
        builder.Query = sb.ToString();

        return new NwisQuery(builder.Uri);
    }
}