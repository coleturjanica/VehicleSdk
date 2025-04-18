using System.Net.Http.Json;
using System.Text.Json;
using BelronUS.Http.HttpClientHelper;
using BelronUS.Http.HttpClientHelper.Interface;
using BelronUS.ServiceHelpers.BelronUSJsonSerializerOptions;
using Microsoft.Extensions.Logging;
using BelronUS.VehicleSDKs.V2.Models.Response;
using BelronUS.VehicleSDKs.V2.Utilities;

namespace BelronUS.VehicleSDKs.V2;

public class VehicleSdk(ISecretManager secretManager, IHttpClientHelper httpClientHelper, IHttpClientFactory httpClientFactory, ILogger<VehicleSdk> logger) : IVehicleSdk
{
    private readonly ISecretManager _secretManager = secretManager;
    private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;
    private readonly JsonSerializerOptions _jsonSerializerOptions = BelronUSJsonSerializerOptions.GetSerializerOptionsWithIgnoreNull();
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly ILogger<VehicleSdk> _logger = logger;

    public void printHi() {
        Console.WriteLine("hi");
    }

    public async Task<VehicleResponseModel> LookupByCarId(string carId)
    {
        var lookupByCarIdVehicleAPiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByCarId}/{carId}";
        var request = new HttpClientRequestObject
        {
            HttpMethod = HttpMethod.Get,
            RequestURI = lookupByCarIdVehicleAPiEndpoint,
            ClientHeaders = new Dictionary<string, string>() { { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() } },
            AutoCheckResponseStatusCode = false
        };

        var httpResponseMessage = await _httpClientHelper.CallClientAndGetHttpResponse(_httpClient, request);

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
