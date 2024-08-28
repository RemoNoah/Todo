using Todo.Api.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Todo.Tests.Auth;

[TestClass]
public class AccessibleByTest
{
    /// <summary>
    /// Tests if method available to everyone can be accessed by everyone.
    /// </summary>
    [TestMethod]
    public void TestAccessibleByEveryone()
    {
        // Arrane
        ActionExecutingContext context = CreateActionExecutingContext();

        // Act
        AccessibleBy AccessibleByAttr = new(AccessFlags.Everyone);
        AccessibleByAttr.OnActionExecuting(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(OkResult));
    }

    /// <summary>
    /// Tests if method can be accessed using valid id.
    /// </summary>
    [TestMethod]
    public void TestAccessibleBySelfSuccess()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        ActionExecutingContext context = CreateActionExecutingContext(
            userId,
            [],
            new Dictionary<string, object>()
            {
                {"userId", userId}
            }
        );

        // Act
        AccessibleBy AccessibleByAttr = new(AccessFlags.Self);
        AccessibleByAttr.OnActionExecuting(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(OkResult));
    }

    /// <summary>
    /// Tests if method can be accessed by self with invalid userId.
    /// </summary>
    [TestMethod]
    public void TestAccessibleBySelfUnauthorized()
    {
        // Arrange
        ActionExecutingContext context = CreateActionExecutingContext(
            Guid.NewGuid(),
            [],
            new Dictionary<string, object>()
            {
                {"userId", Guid.NewGuid()}
            }
        );

        // Act
        AccessibleBy AccessibleByAttr = new(AccessFlags.Self);
        AccessibleByAttr.OnActionExecuting(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(JsonResult));
        Assert.AreEqual(((JsonResult)context.Result).StatusCode, 401);
    }

    /// <summary>
    /// Tests if attribute can be accessed using correct method signature.
    /// </summary>
    [TestMethod]
    public void TestAccessibleBySelfCorrectMethodSignature()
    {
        // Arrange
        ActionExecutingContext context = CreateActionExecutingContext(
            Guid.NewGuid(),
            [],
            new Dictionary<string, object>()
            {
                {"userId", Guid.NewGuid()}
            }
        );

        // Act
        AccessibleBy AccessibleByAttr = new(AccessFlags.Self);
        AccessibleByAttr.OnActionExecuting(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(JsonResult));
        Assert.AreEqual(((JsonResult)context.Result).StatusCode, 401);
    }

    /// <summary>
    /// Tests if exception is thrown when attribute is used on invalid methods.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAccessibleBySelfWrongMethodSignature()
    {
        // Arrange
        ActionExecutingContext context = CreateActionExecutingContext(
            Guid.NewGuid(),
            [],
            new Dictionary<string, object>()
            {
                {"notUserId", Guid.NewGuid()}
            }
        );

        // Act
        AccessibleBy AccessibleByAttr = new(AccessFlags.Self);
        AccessibleByAttr.OnActionExecuting(context);
    }

    /// <summary>
    /// Simple class used by tests as a dto.
    /// </summary>
    private class SimpleDTOWithUserId
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string SomeData { get; set; }

        public SimpleDTOWithUserId(Guid? userId = null)
        {
            Id = Guid.NewGuid();
            UserId = userId ?? Guid.NewGuid();
            SomeData = "Some data";
        }
    }

    /// <summary>
    /// Tests if a method can be accessed with valid userId in dto.
    /// </summary>
    [TestMethod]
    public void TestAccessibleBySelfSuccessWithDTO()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        ActionExecutingContext context = CreateActionExecutingContext(
            userId,
            [],
            new Dictionary<string, object>()
            {
                {"simpleDTO", new SimpleDTOWithUserId(userId)}
            }
        );

        // Act
        AccessibleBy AccessibleByAttr = new(AccessFlags.Self);
        AccessibleByAttr.OnActionExecuting(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(OkResult));
    }

    /// <summary>
    /// Tests if method can be accessed with invalid userId in dto.
    /// </summary>
    [TestMethod]
    public void TestAccessibleBySelfUnauthorizedWithDTO()
    {
        // Assert
        Guid userId = Guid.NewGuid();
        ActionExecutingContext context = CreateActionExecutingContext(
            userId,
            [],
            new Dictionary<string, object>()
            {
                {"simpleDTO", new SimpleDTOWithUserId()}
            }
        );

        // Act
        AccessibleBy AccessibleByAttr = new(AccessFlags.Self);
        AccessibleByAttr.OnActionExecuting(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(JsonResult));
        Assert.AreEqual(((JsonResult)context.Result).StatusCode, 401);
    }

    /// <summary>
    /// Simple class used by tests as dto.
    /// </summary>
    private class SimpleDTOWithoutUserId
    {
        public Guid Id { get; set; }
        public required string SomeData { get; set; }
    }

    /// <summary>
    /// Tests if exception is thrown when attribute is used on invalid methods.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAccessibleBySelfWrongMethodSignatureWithDTO()
    {
        // Arrange

        SimpleDTOWithUserId x = new();
        ActionExecutingContext context = CreateActionExecutingContext(
            Guid.NewGuid(),
            [],
            new Dictionary<string, object>()
            {
                {"simpleDTO", x}
            }
        );

        // Act
        AccessibleBy AccessibleByAttr = new(AccessFlags.Self);
        AccessibleByAttr.OnActionExecuting(context);
    }

    /// <summary>
    /// Will create an ActionExecutingContext with all the required parameters to test the AccessibleBy attribute
    /// </summary>
    /// <param name="userId">(optional) userId of the currently logged in user</param>
    /// <param name="roles">(optional) a list of the roles the currently logged in user is</param>
    /// <param name="parameters">(optional) a dictionary of all the parameters of a Api controller method and their values</param>
    /// <returns>an ActionExecutingContext with all the required parameters to test the AccessibleBy attribute</returns>
    private static ActionExecutingContext CreateActionExecutingContext(Guid? userId = null, List<string> roles = null!, Dictionary<string, object> parameters = null!)
    {
        ActionExecutingContext actionExecutingContext = new(
            new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor(),
                new ModelStateDictionary()
            ),
            [],
            (parameters ?? [])!,
            controller: null!
        );

        actionExecutingContext.ActionDescriptor.Parameters = [];   // Add all parameters from the dictionary to the ActionDescriptor
        foreach (KeyValuePair<string, object> parameter in parameters ?? [])
        {
            actionExecutingContext.ActionDescriptor.Parameters.Add(new ParameterDescriptor()
            {
                Name = parameter.Key,
                ParameterType = parameter.Value!.GetType(),
            });
        }

        actionExecutingContext.Result = new OkResult();

        List<Claim> claims =
        [
            new Claim(AppClaimTypes.Id, (userId ?? Guid.Empty).ToString()),
            new Claim(ClaimTypes.NameIdentifier, (userId ?? Guid.Empty).ToString()),
            .. from role in roles ?? [] select new Claim(ClaimTypes.Role, role ?? string.Empty),
        ];

        actionExecutingContext.HttpContext.User = new ClaimsPrincipal(
            new ClaimsIdentity(claims)
        );

        return actionExecutingContext;
    }
}
