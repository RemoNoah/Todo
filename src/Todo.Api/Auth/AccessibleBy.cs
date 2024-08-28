using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Security.Claims;

namespace Todo.Api.Auth;

/// <summary>
/// Flags to control access to API Controller Methods
/// </summary>
[Flags]
public enum AccessFlags
{
    None = 0,
    Everyone = 1,
    Self = 2, // If self is used a userId or dto with a userId property must be provided as parameter to the controller method
    Admin = 4
}

/// <summary>
/// Attribute to control access to API Controller Methods
/// </summary>
/// <example>
/// to use this attribute place it over a Api Controller Method and enter the desired parameters
/// [AccessibleBy(AccessFlags.Everyone)]
/// public IActionResult Endpoint();
///
/// to allow a Method for multiple Flags use the bitwise or operator
/// [AccessibleBy(AccessFlags.Self | AccessFlags.VocationalTrainer)]
/// public IActionResult Endpoint();
///
/// to require multiple Flags use the attribute multiple times
/// [AccessibleBy(AccessFlags.Self]
/// [AccessibleBy(AccessFlags.VocationalTrainer)]
/// public IActionResult Endpoint();
/// </example>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class AccessibleBy : ActionFilterAttribute
{
    private readonly AccessFlags flags;
    private const string UserIdParameterName = "userId";

    /// <summary>
    /// Constructs a AccessibleBy attribute
    /// </summary>
    /// <param name="flags">Flags to control access to api method</param>
    public AccessibleBy(AccessFlags flags = AccessFlags.Everyone)
    {
        this.flags = flags;
    }

    /// <summary>
    /// Will search the parameters of a api controller method for a userId
    /// </summary>
    /// <param name="context">Context of Api method call</param>
    /// <returns>UserId if found</returns>
    private static Guid? GetUserId(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue(UserIdParameterName, out object? userId))
        {
            return (Guid)userId!;
        }

        // Iterate over all dto parameters and search for a parameter containing a userId
        foreach (ParameterDescriptor parameter in context.ActionDescriptor.Parameters)
        {
            if (parameter.Name.ToLower().Contains("dto"))
            {
                // Use reflection to get value of dto parameter
                object? parameterValue = Convert.ChangeType(context.ActionArguments[parameter.Name], parameter.ParameterType);

                Type type = parameterValue!.GetType();
                PropertyInfo[] parameterProperties = type.GetProperties();
                if (parameterProperties.Any(x => x.Name.ToLower() == UserIdParameterName.ToLower() && x.PropertyType == typeof(Guid)))
                {
                    return (Guid)parameterProperties.FirstOrDefault(x => x.Name.ToLower() == UserIdParameterName.ToLower())!.GetValue(parameterValue)!;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Checks if a user is allowed to access an api endpoint
    /// </summary>
    /// <param name="context">Context of Api method call</param>
    /// <returns>true if user is allowed to access api endpoint</returns>
    /// <exception cref="ArgumentException">If self Flag is used, a parameter named userId or a dto with a userId property with type Guid must be provided</exception>
    private bool IsAllowed(ActionExecutingContext context)
    {
        if ((flags & AccessFlags.Everyone) == AccessFlags.Everyone)
        {
            return true;
        }

        IEnumerable<Claim> claims = context.HttpContext.User.Claims.ToList();
        if (!claims.Any())
        {
            return false;
        }

        if ((flags & AccessFlags.Self) == AccessFlags.Self)
        {
            Guid? authorizedUserId = new(claims.FirstOrDefault(x => x.Type == AppClaimTypes.Id)!.Value);
            Guid? requestedUserId = GetUserId(context);
            if (!requestedUserId.HasValue)
            {
                throw new ArgumentException("User id of user who's data is being requested has not been provided, but is required on data endpoints with 'AccesibleBy.Self' flags");
            }

            if (requestedUserId == authorizedUserId)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Overrides api Call if user is not allowed to access
    /// </summary>
    /// <param name="context">Context of Api method call</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!IsAllowed(context))
        {
            // Return custom 401 result
            context.Result = new JsonResult(new
            {
                Message = "Access to requested data denied."
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        base.OnActionExecuting(context);
    }
}
