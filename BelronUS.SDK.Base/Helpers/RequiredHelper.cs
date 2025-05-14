using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BelronUS.SDK.Base.Helpers;

public static class RequiredHelper
{
    public static bool HasRequired<T>(T request)
    {
        var missingRequiredFields = new List<string>();

         foreach (var property in typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))))
        {
            var value = property.GetValue(request);
            if (value == null || (value is string str && string.IsNullOrEmpty(str)))
            {
                missingRequiredFields.Add(property.Name);
            }
        }

        // If there are any missing required fields, create the error message
        if (missingRequiredFields.Any())
        {
            var errorMessage = $"The following required properties are missing or empty: {string.Join(", ", missingRequiredFields)}";
            throw new ValidationException(errorMessage);
        }
        
        return true;
    }
}
