using System.Net.Http.Json;
using System.Text.Json;
using BelronUS.Http.HttpClientHelper;
using BelronUS.Http.HttpClientHelper.Interface;
using BelronUS.ServiceHelpers.BelronUSJsonSerializerOptions;
using Microsoft.Extensions.Logging;
using BelronUS.VehicleSDKs.V2.Models.Response;
using BelronUS.VehicleSDKs.V2.Utilities;
using BelronUS.VehicleSDKs.V2.Models.Request;
using BelronUS.SDK.Base.Helpers;

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

        // Append Query
        var uri = QueryStringHelper.BuildQueryString(lookupByCarIdVehicleAPiEndpoint, request);

        var clientHeaders = request.Headers ?? [];
        clientHeaders.Add(_secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret());

        // Construct Request, Send and Receive
        var httpRequest = new HttpClientRequestObject
        {
            HttpMethod = HttpMethod.Get,
            RequestURI = uri,
            ClientHeaders = clientHeaders,
            AutoCheckResponseStatusCode = false
        };

        var httpResponseMessage = await _httpClientHelper.CallClientAndGetHttpResponse(_httpClient, httpRequest);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            _logger.LogError("Error calling LookupByAddress. Response: {Response}", response);

            return null;
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<VehicleResponseModel>>(_jsonSerializerOptions);
    }

    public async Task<VehicleResponseModel> LookupByCarId(LookupByCarIdRequestModel request)
    {
        // Get the base URL from the secret manager
        var lookupByCarIdVehicleAPiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByCarId}/{request.CarId}";
        
        // Construct Request, Send and Receive
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
            _logger.LogError("Error calling LookupByCarId. Response: {Response}", response);

            return null;
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<VehicleResponseModel>(_jsonSerializerOptions);
    }

    public async Task<VehicleResponseModel> LookupByVin(LookupByVinRequestModel request)
    {
        // Get the base URL from the secret manager
        var lookupByVINVehicleAPiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByVin}/{request.Vin}";
        
        // Construct Request, Send and Receive
        var httpRequest = new HttpClientRequestObject
        {
            HttpMethod = HttpMethod.Get,
            RequestURI = lookupByVINVehicleAPiEndpoint,
            ClientHeaders = new Dictionary<string, string>() { { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() } },
            AutoCheckResponseStatusCode = false
        };

        var httpResponseMessage = await _httpClientHelper.CallClientAndGetHttpResponse(_httpClient, httpRequest);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            _logger.LogError("Error calling GetVehicleByVIN. Response: {Response}", response);

            return null;
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<VehicleResponseModel>(_jsonSerializerOptions);
    }
}
