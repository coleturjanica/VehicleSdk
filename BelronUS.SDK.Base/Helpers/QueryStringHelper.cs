using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;

namespace BelronUS.SDK.Base.Helpers
{
    public static class QueryStringHelper
    {
        public static string BuildQueryString<T>(string baseUrl, T request)
        {
            // Use reflection to get all properties of the request object
            var queryStringDictionary = new Dictionary<string, string>();

            foreach (var property in typeof(T).GetProperties())
            {
                var value = property.GetValue(request)?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    queryStringDictionary.Add(property.Name, value);
                }
            }

            // Append the query string to the base URL
            return QueryHelpers.AddQueryString(baseUrl, queryStringDictionary);
        }
    }
}
