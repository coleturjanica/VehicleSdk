using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BelronUS.SDK.Base.Helpers
{
    public static class RequiredHelper
    {
        public static bool HasRequired<T>(T request)
        {
            var missingRequiredFields = new List<string>();

            foreach (var item in from p in request.GetType().GetProperties()
                                  where Attribute.IsDefined(p, typeof(RequiredAttribute))
                                  select p)
            {
                var value = item.GetValue(request);
                
                if (value == null || 
                    (value is string str && string.IsNullOrEmpty(str)) ||
                    (value is int intValue && intValue == 0))
                {
                    missingRequiredFields.Add(item.Name);
                }
            }

            if (missingRequiredFields.Any())
            {
                var errorMessage = new StringBuilder("The following required properties are missing or empty: ");
                errorMessage.Append(string.Join(", ", missingRequiredFields));
                throw new ValidationException(errorMessage.ToString());
            }

            return true;
        }
    }
}
