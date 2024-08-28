using Todo.Api.Controllers;
using Todo.Domain.DTO.User;
using Todo.Domain.Models;
using Todo.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Todo.Tests.Controllers;

[TestClass]
public class AuthControllerTest
{
    private AuthController _authController = null!;
    private readonly Mock<IAuthService> _authServiceMock = new();

    [TestInitialize]
    public void Setup()
    {
        List<KeyValuePair<string, string?>> inMemorySettings =
        [
            new KeyValuePair<string, string?>("Jwt:Key", "JWT_KEYqeipruhgyxcknpoaerjtowperituldkcbnklsdnövmxlckvj0arituq'riewüpfoasvlkmcxyoijv0'w8üaerpjadskvmcxlkmvölkjapofijuwpoiuerj")
        ];

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _authController = new AuthController(configuration, _authServiceMock.Object);
    }

    [TestMethod]
    public void Register_UserDTOIsNUll_ReturnsBadRequest()
    {
        // Arrange
        UserRegistrationDTO newUser = null!;

        // Act
        Task<ActionResult> actual = _authController.Register(newUser);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Register_UserDTOIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        UserRegistrationDTO newUser = new();

        // Act
        Task<ActionResult> actual = _authController.Register(newUser);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Register_RegisteredUserIsNUll_ReturnsBadRequest()
    {
        // Arrange
        UserRegistrationDTO newUser = new() { Email = "Test", FirstName = "Test", Password = "Test", LastName = "test", Username = "test" };

        _ = _authServiceMock.Setup(r => r.RegisterAsync(It.IsAny<UserRegistrationDTO>())).ReturnsAsync(null as User);

        // Act
        Task<ActionResult> actual = _authController.Register(newUser);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Register_ReturnsOk()
    {
        // Arrange
        UserRegistrationDTO newUser = new() { Email = "Test", FirstName = "Test", Password = "Test", LastName = "test", Username = "test" };
        User user = new() { Email = "Test", FirstName = "Test", LastName = "test", Username = "test", Salt = "aPvpoOXmAQ5trcofo5vfDA==", Hash = "FhA5rX7AAODqo5qLd8s8a03pTRWxh2C7KheuFC3KbQ8=" };

        _ = _authServiceMock.Setup(r => r.RegisterAsync(It.IsAny<UserRegistrationDTO>())).ReturnsAsync(user);

        // Act
        Task<ActionResult> actual = _authController.Register(newUser);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }


    [TestMethod]
    public void Login_UserDTOIsNUll_ReturnsBadRequest()
    {
        // Arrange
        UserLoginDTO user = null!;

        // Act
        Task<ActionResult> actual = _authController.Login(user);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Login_UserDTOIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        UserLoginDTO user = new();

        // Act
        Task<ActionResult> actual = _authController.Login(user);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Login_LogedInUserIsNUll_ReturnsBadRequest()
    {
        // Arrange
        UserLoginDTO user = new() { Email = "Test", Password = "Test" };

        _ = _authServiceMock.Setup(r => r.RegisterAsync(It.IsAny<UserRegistrationDTO>())).ReturnsAsync(null as User);

        // Act
        Task<ActionResult> actual = _authController.Login(user);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Login_ReturnsOk()
    {
        // Arrange
        UserLoginDTO user = new() { Email = "Test", Password = "Test" };
        User logedInUser = new() { Email = "Test", FirstName = "Test", LastName = "test", Username = "test", Salt = "aPvpoOXmAQ5trcofo5vfDA==", Hash = "FhA5rX7AAODqo5qLd8s8a03pTRWxh2C7KheuFC3KbQ8=" };

        _ = _authServiceMock.Setup(r => r.LoginAsync(It.IsAny<UserLoginDTO>())).ReturnsAsync(logedInUser);

        // Act
        Task<ActionResult> actual = _authController.Login(user);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }
}
