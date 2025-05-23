using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base.Helpers;

namespace BelronUS.SDK.Base
{
    /// <summary>
    /// Represents the base response model from any API call.
    /// </summary>
    /// <remarks>
    /// The following properties are required:
    /// <list type="bullet">
    /// <item><description>BaseApplicationName</description></item>
    /// <item><description>BaseCorrelationId</description></item>
    /// <item><description>BaseHeaders</description></item>
    /// <item><description>BaseApiBaseUrl</description></item>
    /// <item><description>BaseClientId</description></item>
    /// <item><description>BaseClientSecret</description></item>
    /// </list>
    /// </remarks>
    public abstract class BaseSdkRequest
    {
        [Required]
        public string BaseApplicationName { get; set; }
        [Required]
        public Guid BaseCorrelationId { get; set; }
        [Required]
        public string BaseApiBaseUrl { get; set; }
        [Required]
        public string BaseClientId { get; set; }
        [Required]
        public string BaseClientSecret { get; set; }

        public virtual void Validate()
        {
            RequiredHelper.HasRequired(this);

            if (BaseCorrelationId == Guid.Empty)
        {
            throw new ValidationException("BaseCorrelationId must not be Empty.");
        }
        }
    }

    /// <summary>
    /// Represents the base request model used in any API call.
    /// </summary>
    /// <remarks>
    /// The following properties are required:
    /// <list type="bullet">
    /// <item><description>BaseStatusCode</description></item>
    /// </list>
    /// </remarks>
    public abstract class BaseSdkResponse
    {
        public int BaseStatusCode { get; set; }
    }
}