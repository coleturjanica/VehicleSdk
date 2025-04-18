namespace BelronUS.SDK.Base;

internal interface IBaseSdkModel
{
    string ApplicationName { get; set; }
    string CorrelationId { get; set; }
}

interface IBaseSdkRequest : IBaseSdkModel
{
    Dictionary<string, string> Headers { get; set; }
    string ApiBaseUrl { get; set; }
}

interface IBaseSdkResponse : IBaseSdkModel
{
    Dictionary<string, string> Headers { get; set; }
    string ApiBaseUrl { get; set; }
}