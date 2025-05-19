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
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2;

public class VehicleSdk(ISecretManager secretManager, IHttpClientHelper httpClientHelper, IHttpClientFactory httpClientFactory, ILogger<VehicleSdk> logger) : IVehicleSdk
{
    private readonly ISecretManager _secretManager = secretManager;
    private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;
    private readonly JsonSerializerOptions _jsonSerializerOptions = BelronUSJsonSerializerOptions.GetSerializerOptionsWithIgnoreNull();
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly ILogger<VehicleSdk> _logger = logger;

    public T CreateRequest<T>(Action<T> initializer) where T : BaseSdkRequest, new()
    {
        var instance = new T();
        initializer(instance);
        instance.Validate();
        return instance;
    }

    public async Task<IEnumerable<VehicleResponseModel>> LookupByAddress(LookupByAddressRequestModel request)
    {
        // Get the base URL from the secret manager
        var lookupByCarIdVehicleApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByAddress}";

        // Append Query
        var uri = QueryStringHelper.BuildQueryString(lookupByCarIdVehicleApiEndpoint, request);

        // BaseHeaders is confusing because correlationId is a header and might need to be added like sercretmanager is from the request
        var clientHeaders = request.BaseHeaders ?? [];
        clientHeaders.Add(_secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret());
        clientHeaders.Add("X-Application-Name", request.BaseApplicationName);
        clientHeaders.Add("X-Correlation-ID", request.BaseCorrelationId.ToString());

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

        var vehicleResponses = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<VehicleResponseModel>>(_jsonSerializerOptions);

        // Map BaseSdkResponse fields to each item in the list
        if (vehicleResponses != null)
        {
            var appName = httpResponseMessage.Headers.GetValues("X-Application-Name").FirstOrDefault();
            var correlationIdHeader = httpResponseMessage.Headers.GetValues("X-Correlation-Id").FirstOrDefault();
            var correlationId = Guid.Parse(correlationIdHeader);

            var baseHeaders = httpResponseMessage.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value));
            baseHeaders.Remove("X-Application-Name");
            baseHeaders.Remove("X-Correlation-Id");
            var statusCode = (int)httpResponseMessage.StatusCode;

            foreach (var item in vehicleResponses)
            {
                item.BaseApplicationName = appName;
                item.BaseCorrelationId = correlationId;
                item.BaseHeaders = baseHeaders;
                item.BaseStatusCode = statusCode;
            }
        }

        return vehicleResponses;
    }

    public async Task<VehicleResponseModel> LookupByCarId(LookupByCarIdRequestModel request)
    {
        // Get the base URL from the secret manager
        var lookupByCarIdVehicleApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByCarId}/{request.CarId}";

        var clientHeaders = request.BaseHeaders ?? [];
        clientHeaders.Add(_secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret());
        clientHeaders.Add("X-Application-Name", request.BaseApplicationName);
        clientHeaders.Add("X-Correlation-ID", request.BaseCorrelationId.ToString());
        
        // Construct Request, Send and Receive
        var httpRequest = new HttpClientRequestObject
        {
            HttpMethod = HttpMethod.Get,
            RequestURI = lookupByCarIdVehicleApiEndpoint,
            ClientHeaders = clientHeaders,
            AutoCheckResponseStatusCode = false
        };

        var httpResponseMessage = await _httpClientHelper.CallClientAndGetHttpResponse(_httpClient, httpRequest);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            _logger.LogError("Error calling LookupByCarId. Response: {Response}", response);

            return null;
        }

        var vehicleResponse = await httpResponseMessage.Content.ReadFromJsonAsync<VehicleResponseModel>(_jsonSerializerOptions);

        // Map BaseSdkResponse fields
        if (vehicleResponse != null)
        {
            vehicleResponse.BaseApplicationName = httpResponseMessage.Headers.GetValues("X-Application-Name").FirstOrDefault();
            var correlationIdHeader = httpResponseMessage.Headers.GetValues("X-Correlation-Id").FirstOrDefault();
            vehicleResponse.BaseCorrelationId = Guid.Parse(correlationIdHeader);
            var baseHeaders = httpResponseMessage.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value));
            baseHeaders.Remove("X-Application-Name");
            baseHeaders.Remove("X-Correlation-Id");
            vehicleResponse.BaseHeaders = baseHeaders;
            vehicleResponse.BaseStatusCode = (int)httpResponseMessage.StatusCode;
        }

        return vehicleResponse;
    }

    public async Task<VehicleResponseModel> LookupByVin(LookupByVinRequestModel request)
    {
        // Get the base URL from the secret manager
        var lookupByVinVehicleApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByVin}/{request.Vin}";

        var clientHeaders = request.BaseHeaders ?? [];
        clientHeaders.Add(_secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret());
        clientHeaders.Add("X-Application-Name", request.BaseApplicationName);
        clientHeaders.Add("X-Correlation-ID", request.BaseCorrelationId.ToString());

        // Construct Request, Send and Receive
        var httpRequest = new HttpClientRequestObject
        {
            HttpMethod = HttpMethod.Get,
            RequestURI = lookupByVinVehicleApiEndpoint,
            ClientHeaders = clientHeaders,
            AutoCheckResponseStatusCode = false
        };

        var httpResponseMessage = await _httpClientHelper.CallClientAndGetHttpResponse(_httpClient, httpRequest);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            _logger.LogError("Error calling GetVehicleByVIN. Response: {Response}", response);

            return null;
        }

        var vehicleResponse = await httpResponseMessage.Content.ReadFromJsonAsync<VehicleResponseModel>(_jsonSerializerOptions);

        // Map BaseSdkResponse fields
        if (vehicleResponse != null)
        {
            vehicleResponse.BaseApplicationName = httpResponseMessage.Headers.GetValues("X-Application-Name").FirstOrDefault();
            var correlationIdHeader = httpResponseMessage.Headers.GetValues("X-Correlation-Id").FirstOrDefault();
            vehicleResponse.BaseCorrelationId = Guid.Parse(correlationIdHeader);
            var baseHeaders = httpResponseMessage.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value));
            baseHeaders.Remove("X-Application-Name");
            baseHeaders.Remove("X-Correlation-Id");
            vehicleResponse.BaseHeaders = baseHeaders;
            vehicleResponse.BaseStatusCode = (int)httpResponseMessage.StatusCode;
        }
        
        return vehicleResponse;
    }    
}
