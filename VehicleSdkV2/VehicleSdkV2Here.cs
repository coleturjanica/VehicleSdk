using System.Net.Http.Json;
using System.Text.Json;
using BelronUS.Http.HttpClientHelper;
using BelronUS.Http.HttpClientHelper.Interface;
using BelronUS.ServiceHelpers.BelronUSJsonSerializerOptions;
using Microsoft.Extensions.Logging;
using VehicleSdkV2.Models.Response;
using VehicleSdkV2.Utilities;

namespace VehicleSdkV2;

public class VehicleSdkV2Here : IVehicleSdkV2Here
{
    private readonly ISecretManager _secretManager;
    private readonly IHttpClientHelper _httpClientHelper;
    private readonly JsonSerializerOptions _jsonSerializerOptions = BelronUSJsonSerializerOptions.GetSerializerOptionsWithIgnoreNull();
    private readonly HttpClient _httpClient;
    private readonly ILogger<VehicleSdkV2Here> _logger;


    public VehicleSdkV2Here(ISecretManager secretManager, IHttpClientHelper httpClientHelper, IHttpClientFactory httpClientFactory, ILogger<VehicleSdkV2Here> logger)
    {
        _secretManager = secretManager;
        _httpClientHelper = httpClientHelper;
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
    }

    public void printHi() {
        Console.WriteLine("hi");
    }

    public async Task<VehicleResponseModel> GetVehicleByCarId(string carId)
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
