using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BelronUS.VehicleSDKs.V2.Models.Response;
using BelronUS.VehicleSDKs.V2.Utilities;
using BelronUS.VehicleSDKs.V2.Models.Request;
using BelronUS.SDK.Base.Helpers;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2
{
    public class VehicleSdk : IVehicleSdk
    {
        private readonly ISecretManager _secretManager;
        private readonly HttpClient _httpClient;
        private readonly ILogger<VehicleSdk> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public VehicleSdk(ISecretManager secretManager, HttpClient httpClient, ILogger<VehicleSdk> logger)
        {
            _secretManager = secretManager;
            _httpClient = httpClient;
            _logger = logger;
        }

        public T CreateRequest<T>(Action<T> initializer) where T : BaseSdkRequest, new()
        {
            var instance = new T();
            initializer(instance);
            instance.Validate();
            return instance;
        }

        public async Task<IEnumerable<VehicleResponseModel>> LookupByAddress(LookupByAddressRequestModel request)
        {
            var lookupByAddressEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByAddress}";
            var uri = QueryStringHelper.BuildQueryString(lookupByAddressEndpoint, request);

            var clientHeaders = request.BaseHeaders ?? new Dictionary<string, string>();
            clientHeaders[_secretManager.GetOriginVerifyKey()] = _secretManager.GetOriginVerifySecret();
            clientHeaders["X-Application-Name"] = request.BaseApplicationName;
            clientHeaders["X-Correlation-ID"] = request.BaseCorrelationId.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            foreach (var header in clientHeaders)
                httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);

            var httpResponseMessage = await _httpClient.SendAsync(httpRequest);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogError("Error calling LookupByAddress. Response: {Response}", response);
                return null;
            }

            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            var vehicleResponses = JsonSerializer.Deserialize<IEnumerable<VehicleResponseModel>>(json, _jsonSerializerOptions);

            if (vehicleResponses != null)
            {
                var baseHeaders = new Dictionary<string, string>();
                foreach (var header in httpResponseMessage.Headers)
                    baseHeaders[header.Key] = string.Join(",", header.Value);
                var statusCode = (int)httpResponseMessage.StatusCode;

                foreach (var item in vehicleResponses)
                {
                    item.BaseHeaders = baseHeaders;
                    item.BaseStatusCode = statusCode;
                }
            }

            return vehicleResponses;
        }

        public async Task<VehicleResponseModel> LookupByCarId(LookupByCarIdRequestModel request)
        {
            var lookupByCarIdEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByCarId}/{request.CarId}";

            var clientHeaders = request.BaseHeaders ?? new Dictionary<string, string>();
            clientHeaders[_secretManager.GetOriginVerifyKey()] = _secretManager.GetOriginVerifySecret();
            clientHeaders["X-Application-Name"] = request.BaseApplicationName;
            clientHeaders["X-Correlation-ID"] = request.BaseCorrelationId.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, lookupByCarIdEndpoint);
            foreach (var header in clientHeaders)
                httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);

            var httpResponseMessage = await _httpClient.SendAsync(httpRequest);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogError("Error calling LookupByCarId. Response: {Response}", response);
                return null;
            }

            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            var vehicleResponse = JsonSerializer.Deserialize<VehicleResponseModel>(json, _jsonSerializerOptions);

            if (vehicleResponse != null)
            {
                var baseHeaders = new Dictionary<string, string>();
                foreach (var header in httpResponseMessage.Headers)
                    baseHeaders[header.Key] = string.Join(",", header.Value);
                vehicleResponse.BaseHeaders = baseHeaders;
                vehicleResponse.BaseStatusCode = (int)httpResponseMessage.StatusCode;
            }

            return vehicleResponse;
        }

        public async Task<VehicleResponseModel> LookupByVin(LookupByVinRequestModel request)
        {
            var lookupByVinEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByVin}/{request.Vin}";

            var clientHeaders = request.BaseHeaders ?? new Dictionary<string, string>();
            clientHeaders[_secretManager.GetOriginVerifyKey()] = _secretManager.GetOriginVerifySecret();
            clientHeaders["X-Application-Name"] = request.BaseApplicationName;
            clientHeaders["X-Correlation-ID"] = request.BaseCorrelationId.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, lookupByVinEndpoint);
            foreach (var header in clientHeaders)
                httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);

            var httpResponseMessage = await _httpClient.SendAsync(httpRequest);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogError("Error calling GetVehicleByVIN. Response: {Response}", response);
                return null;
            }

            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            var vehicleResponse = JsonSerializer.Deserialize<VehicleResponseModel>(json, _jsonSerializerOptions);

            if (vehicleResponse != null)
            {
                var baseHeaders = new Dictionary<string, string>();
                foreach (var header in httpResponseMessage.Headers)
                    baseHeaders[header.Key] = string.Join(",", header.Value);
                vehicleResponse.BaseHeaders = baseHeaders;
                vehicleResponse.BaseStatusCode = (int)httpResponseMessage.StatusCode;
            }

            return vehicleResponse;
        }
    }
}
