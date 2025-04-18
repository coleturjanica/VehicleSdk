using System.Net.Http.Json;
using System.Text.Json;
using BelronUS.Http.HttpClientHelper;
using BelronUS.Http.HttpClientHelper.Interface;
using BelronUS.ServiceHelpers.BelronUSJsonSerializerOptions;
using Microsoft.Extensions.Logging;
using BelronUS.VehicleSDKs.V2.Models.Response;
using BelronUS.VehicleSDKs.V2.Utilities;
using BelronUS.VehicleSDKs.V2.Models.Request;
using Microsoft.AspNetCore.WebUtilities;

namespace BelronUS.VehicleSDKs.V2;

public class VehicleSdk(ISecretManager secretManager, IHttpClientHelper httpClientHelper, IHttpClientFactory httpClientFactory, ILogger<VehicleSdk> logger) : IVehicleSdk
{
    private readonly ISecretManager _secretManager = secretManager;
    private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;
    private readonly JsonSerializerOptions _jsonSerializerOptions = BelronUSJsonSerializerOptions.GetSerializerOptionsWithIgnoreNull();
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly ILogger<VehicleSdk> _logger = logger;

    public async Task<IEnumerable<VehicleResponseModel>> LookupByAddress(LookupByAddressRequestModel request)
    {
        // Get the base URL from the secret manager
        var lookupByCarIdVehicleAPiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByAddress}";

        // Construct Query, adding non-required fields if they are not null or empty
        var queryStringDictionary = new Dictionary<string, string>
        {
            { nameof(request.LastName), request.LastName },
            { nameof(request.RegisteredStreetAddress), request.RegisteredStreetAddress },
            { nameof(request.State), request.State },
            { nameof(request.Zip), request.Zip }
        };
        if (!string.IsNullOrEmpty(request.FirstName))
        {
            queryStringDictionary.Add(nameof(request.FirstName), request.FirstName);
        }
        if (!string.IsNullOrEmpty(request.City))
        {
            queryStringDictionary.Add(nameof(request.City), request.City);
        }

        // Append Query
        var uri = QueryHelpers.AddQueryString(lookupByCarIdVehicleAPiEndpoint, queryStringDictionary);

        request.Headers.Add(_secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret());

        // Construct Request, Send and Receive
        var httpRequest = new HttpClientRequestObject
        {
            HttpMethod = HttpMethod.Get,
            RequestURI = uri,
            ClientHeaders = request.Headers,
            AutoCheckResponseStatusCode = false
        };

        var httpResponseMessage = await _httpClientHelper.CallClientAndGetHttpResponse(_httpClient, httpRequest);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            _logger.LogError("Error calling GetVehicleByCarId. Response: {Response}", response);

            return null;
        }

        var objectFromResponseContent = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<VehicleResponseModel>>(_jsonSerializerOptions);

        return objectFromResponseContent;
    }

    public async Task<VehicleResponseModel> LookupByCarId(LookupByCarIdRequestModel request)
    {
        var lookupByCarIdVehicleAPiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByCarId}/{request.CarId}";
        var httpRequest = new HttpClientRequestObject
        {
            HttpMethod = HttpMethod.Get,
            RequestURI = lookupByCarIdVehicleAPiEndpoint,
            ClientHeaders = new Dictionary<string, string>() { { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() } },
            AutoCheckResponseStatusCode = false
        };

        var httpResponseMessage = await _httpClientHelper.CallClientAndGetHttpResponse(_httpClient, httpRequest);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            _logger.LogError("Error calling GetVehicleByCarId. Response: {Response}", response);

            return null;
        }

        var objectFromResponseContent = await httpResponseMessage.Content.ReadFromJsonAsync<VehicleResponseModel>(_jsonSerializerOptions);

        return objectFromResponseContent;
    }
}
