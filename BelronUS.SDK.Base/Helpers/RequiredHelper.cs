using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BelronUS.SDK.Base.Helpers;

public static class RequiredHelper
{
    public static bool HasRequired<T>(T request)
    {
        var missingRequiredFields = new List<string>();

        // Loop through all properties of the request object
        foreach (var property in typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))))
        {
            // Get the value of the property
            var value = property.GetValue(request);
            if (value == null || (value is string str && string.IsNullOrEmpty(str)))
            {
                missingRequiredFields.Add(property.Name);
            }
        }

        // If there are any missing required fields, throw a ValidationException
        if (missingRequiredFields.Any())
        {
            var errorMessage = new StringBuilder("The following required properties are missing or empty: ");
            errorMessage.Append(string.Join(", ", missingRequiredFields));
            throw new ValidationException(errorMessage.ToString());
        }

        return true;
    }
}
