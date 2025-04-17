using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using vehicleSdk.v1.Models.Request;

namespace vehicleSdk.v1;

public class VehicleSdkV1()
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new();


    public async Task<IEnumerable<string>> GetMakesV1(GetMakesRequestModel request)
    {

    }
}