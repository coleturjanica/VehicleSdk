using System.ComponentModel.DataAnnotations;

namespace BelronUS.SDK.Base.Helpers;


// TODO Make this throw an array of all unmet required fields, not just the first one.
public static class RequiredHelper
{
    public static bool HasRequired<T>(T request)
    {
        // Loop through all properties of the request object
        foreach (var property in typeof(T).GetProperties())
        {
            // Check if the property has the [Required] attribute
            if (Attribute.IsDefined(property, typeof(RequiredAttribute)))
            {
                // Get the value of the property
                var value = property.GetValue(request);
                if (value == null || (value is string str && string.IsNullOrEmpty(str)))
                {
                    throw new ValidationException($"The property '{property.Name}' is required and cannot be null or empty.");
                }
            }
        }

        return true;
    }
}
