using System.Text.Json;
using BelronUS.HttpLegacy.HttpClientHelper;
using BelronUS.HttpLegacy.HttpClientHelper.Interface;
using Microsoft.Extensions.Logging;
using BelronUS.VehicleSDKs.V2.Models.Response;
using BelronUS.VehicleSDKs.V2.Utilities;
using BelronUS.VehicleSDKs.V2.Models.Request;
using BelronUS.SDK.Base.Helpers;
using BelronUS.SDK.Base;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BelronUS.VehicleSDKs.V2
{
    public class VehicleSdk : IVehicleSdk
    {
        private readonly ISecretManager _secretManager;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger<VehicleSdk> _logger;

        public VehicleSdk(ISecretManager secretManager, IHttpClientHelper httpClientHelper,
                        IHttpClientFactory httpClientFactory, ILogger<VehicleSdk> logger)
        {
            _secretManager = secretManager;
            _httpClientHelper = httpClientHelper;
            _jsonSerializerOptions = JsonSerializerOptionsHelper.GetSerializerOptionsWithIgnoreNull();
            _httpClient = httpClientFactory.CreateClient();
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
            // Get the base URL from the secret manager
            var lookupByCarIdVehicleApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByAddress}";

            // Append Query
            var uri = QueryStringHelper.BuildQueryString(lookupByCarIdVehicleApiEndpoint, request);

            var clientHeaders = new Dictionary<string, string>
            {
                { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() },
                { "X-Application-Name", request.BaseApplicationName },
                { "X-Correlation-ID", request.BaseCorrelationId.ToString() }
            };

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
                var statusCode = (int)httpResponseMessage.StatusCode;

                foreach (var item in vehicleResponses)
                {
                    item.BaseStatusCode = statusCode;
                }
            }

            return vehicleResponses;
        }

        public async Task<VehicleResponseModel> LookupByCarId(LookupByCarIdRequestModel request)
        {
            // Get the base URL from the secret manager
            var lookupByCarIdVehicleApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByCarId}/{request.CarId}";

            var clientHeaders = new Dictionary<string, string>
            {
                { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() },
                { "X-Application-Name", request.BaseApplicationName },
                { "X-Correlation-ID", request.BaseCorrelationId.ToString() }
            };

            // Construct Request, Send and Receive
            var httpRequest = new HttpClientRequestObject
            {
                HttpMethod = HttpMethod.Get,
                RequestURI = lookupByCarIdVehicleApiEndpoint,
                ClientHeaders = clientHeaders,
                AutoCheckResponseStatusCode = false
            };

            var httpResponseMessage = await _httpClientHelper.CallClientAndGetHttpResponse(_httpClient, httpRequest);

            foreach (var header in httpResponseMessage.Headers)
            {
                Console.WriteLine($"Header: {header.Key} = {string.Join(",", header.Value)}");
            }

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
                vehicleResponse.BaseStatusCode = (int)httpResponseMessage.StatusCode;
            }

            return vehicleResponse;
        }

        public async Task<VehicleResponseModel> LookupByVin(LookupByVinRequestModel request)
        {
            // Get the base URL from the secret manager
            var lookupByVinVehicleApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByVin}/{request.Vin}";

            var clientHeaders = new Dictionary<string, string>
            {
                { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() },
                { "X-Application-Name", request.BaseApplicationName },
                { "X-Correlation-ID", request.BaseCorrelationId.ToString() }
            };

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
                _logger.LogError("Error calling LookupByVin. Response: {Response}", response);

                return null;
            }

            var vehicleResponse = await httpResponseMessage.Content.ReadFromJsonAsync<VehicleResponseModel>(_jsonSerializerOptions);

            // Map BaseSdkResponse fields
            if (vehicleResponse != null)
            {
                vehicleResponse.BaseStatusCode = (int)httpResponseMessage.StatusCode;
            }

            return vehicleResponse;
        }

        public async Task<IEnumerable<VehicleResponseModel>> LookupByCarProperties(LookupByCarPropertiesRequestModel request)
        {
            // Get the base URL from the secret manager
            var lookupByCarPropsApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByCarProperties}";

            // Append Query
            var uri = QueryStringHelper.BuildQueryString(lookupByCarPropsApiEndpoint, request);

            var clientHeaders = new Dictionary<string, string>
            {
                { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() },
                { "X-Application-Name", request.BaseApplicationName },
                { "X-Correlation-ID", request.BaseCorrelationId.ToString() }
            };

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
                _logger.LogError("Error calling LookupByCarProperties. Response: {Response}", response);

                return null;
            }

            var vehicleResponses = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<VehicleResponseModel>>(_jsonSerializerOptions);

            // Map BaseSdkResponse fields to each item in the list
            if (vehicleResponses != null)
            {
                var statusCode = (int)httpResponseMessage.StatusCode;

                foreach (var item in vehicleResponses)
                {
                    item.BaseStatusCode = statusCode;
                }
            }
            return vehicleResponses;
        }

        public async Task<VehicleResponseModel> LookupByLicensePlate(LookupByLicensePlateRequestModel request)
        {
            // Get the base URL from the secret manager
            var lookupByLicensePlateApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetLookupByLicensePlate}";

            // Append Query
            var uri = QueryStringHelper.BuildQueryString(lookupByLicensePlateApiEndpoint, request);

            var clientHeaders = new Dictionary<string, string>
            {
                { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() },
                { "X-Application-Name", request.BaseApplicationName },
                { "X-Correlation-ID", request.BaseCorrelationId.ToString() }
            };

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
                _logger.LogError("Error calling LookupByLicensePlate. Response: {Response}", response);

                return null;
            }

            var vehicleResponse = await httpResponseMessage.Content.ReadFromJsonAsync<VehicleResponseModel>(_jsonSerializerOptions);

            // Map BaseSdkResponse fields
            if (vehicleResponse != null)
            {
                vehicleResponse.BaseStatusCode = (int)httpResponseMessage.StatusCode;
            }

            return vehicleResponse;
        }

        public async Task<RegistrationLookupPermissableResponseModel> GetRegistrationLookupPermissable(RegistrationLookupPermissableRequestModel request)
        {
            // Get the base URL from the secret manager
            var getRegistrationLookupPermissableApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetRegistrationLookupPermissable}";

            // Append Query
            var uri = QueryStringHelper.BuildQueryString(getRegistrationLookupPermissableApiEndpoint, request);

            var clientHeaders = new Dictionary<string, string>
            {
                { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() },
                { "X-Application-Name", request.BaseApplicationName },
                { "X-Correlation-ID", request.BaseCorrelationId.ToString() }
            };

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
                _logger.LogError("Error calling GetRegistrationLookupPermissable. Response: {Response}", response);

                return null;
            }

            var registrationLookupPermissableResponse = await httpResponseMessage.Content.ReadFromJsonAsync<RegistrationLookupPermissableResponseModel>(_jsonSerializerOptions);

            // Map BaseSdkResponse fields
            if (registrationLookupPermissableResponse != null)
            {
                registrationLookupPermissableResponse.BaseStatusCode = (int)httpResponseMessage.StatusCode;
            }

            return registrationLookupPermissableResponse;
        }
        
        public async Task<IEnumerable<VehicleResponseModel>> SearchVehicle(SearchVehicleRequestModel request)
        {
            // Get the base URL from the secret manager
            var searchVehicleApiEndpoint = $"{_secretManager.GetBelronApiBaseURL()}{ExternalEndpoints.VehicleApi.GetSearchVehicle}";

            // Append Query
            var uri = QueryStringHelper.BuildQueryString(searchVehicleApiEndpoint, request);

            var clientHeaders = new Dictionary<string, string>
            {
                { _secretManager.GetOriginVerifyKey(), _secretManager.GetOriginVerifySecret() },
                { "X-Application-Name", request.BaseApplicationName },
                { "X-Correlation-ID", request.BaseCorrelationId.ToString() }
            };

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
                _logger.LogError("Error calling SearchVehicle. Response: {Response}", response);

                return null;
            }

            var vehicleResponses = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<VehicleResponseModel>>(_jsonSerializerOptions);

            // Map BaseSdkResponse fields to each item in the list
            if (vehicleResponses != null)
            {
                var statusCode = (int)httpResponseMessage.StatusCode;

                foreach (var item in vehicleResponses)
                {
                    item.BaseStatusCode = statusCode;
                }
            }
            return vehicleResponses;
        }
    }
}