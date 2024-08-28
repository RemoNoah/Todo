using Todo.Domain.DTO.User;
using Todo.Domain.Models;
using Todo.Domain.UnitOfWork;
using Todo.Services.Services;
using Moq;
using System.Linq.Expressions;

namespace Todo.Tests.Services;

[TestClass]
public class AuthServiceTest
{
    private Mock<IUnitOfWork> _uowMock = null!;
    private AuthService _authService = null!;


    [TestInitialize]
    public void Setup()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _authService = new(_uowMock.Object);
    }

    [TestMethod]
    public void RegisterAsync_UserDTOIsNUll_ReturnsNull()
    {
        // Arrange
        UserRegistrationDTO newUser = null!;

        // Act
        Task<User?> result = _authService.RegisterAsync(newUser);

        // Assert
        Assert.IsNull(result.Result);
    }

    [TestMethod]
    public void RegisterAsync_UserWasFound_ReturnsNull()
    {
        // Arrange
        UserRegistrationDTO newUser = new() { Email = "a", Password = "a" };
        User user = new() { Email = newUser.Email };

        _ = _uowMock.Setup(r => r.Users.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), CancellationToken.None)).ReturnsAsync(user);

        // Act
        Task<User?> result = _authService.RegisterAsync(newUser);

        // Assert
        Assert.IsNull(result.Result);
    }

    [TestMethod]
    public void RegisterAsync_UserNotFound_FirstRegisteredUser_ReturnsUser()
    {
        // Arrange
        UserRegistrationDTO newUser = new() { Email = "a", Password = "a" };
        User user = new() { Email = newUser.Email };

        _ = _uowMock.Setup(r => r.Users.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), CancellationToken.None)).ReturnsAsync(null as User);
        _ = _uowMock.Setup(r => r.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(new Role());
        _ = _uowMock.Setup(r => r.Users.GetAllAsync()).ReturnsAsync([]);
        _ = _uowMock.Setup(r => r.Users.Create(It.IsAny<User>()));

        // Act
        User? result = _authService.RegisterAsync(newUser).Result;

        // Assert
        Assert.AreEqual(newUser.Email, result!.Email);
    }

    [TestMethod]
    public void RegisterAsync_UserNotFound_NotFirstRegisteredUser_ReturnsUser()
    {
        // Arrange
        UserRegistrationDTO newUser = new() { Email = "a", Password = "a" };
        User user = new() { Email = newUser.Email };

        _ = _uowMock.Setup(r => r.Users.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), CancellationToken.None)).ReturnsAsync(null as User);
        _ = _uowMock.Setup(r => r.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(new Role());
        _ = _uowMock.Setup(r => r.Users.GetAllAsync()).ReturnsAsync([new()]);
        _ = _uowMock.Setup(r => r.Users.Create(It.IsAny<User>()));

        // Act
        User? result = _authService.RegisterAsync(newUser).Result;

        // Assert
        Assert.AreEqual(newUser.Email, result!.Email);
    }

    [TestMethod]
    public void LoginAsync_UserDTOIsNUll_ReturnsNull()
    {
        // Arrange
        UserLoginDTO newUser = null!;

        // Act
        Task<User?> result = _authService.LoginAsync(newUser);

        // Assert
        Assert.IsNull(result.Result);
    }

    [TestMethod]
    public void LoginAsync_UserNotFound_ReturnsNull()
    {
        // Arrange
        UserLoginDTO newUser = new();
        _ = _uowMock.Setup(r => r.Users.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), CancellationToken.None)).ReturnsAsync(null as User);

        // Act
        Task<User?> result = _authService.LoginAsync(newUser);

        // Assert
        Assert.IsNull(result.Result);
    }

    [TestMethod]
    public void LoginAsync_UserCredentialsAreCorrect_ReturnsUser()
    {
        // Arrange
        UserLoginDTO userDTO = new() { Email = "a", Password = "aa" };
        User user = new(userDTO.Password) { Id = Guid.NewGuid(), Email = userDTO.Email };
        _ = _uowMock.Setup(r => r.Users.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), CancellationToken.None)).ReturnsAsync(user);

        // Act
        User? result = _authService.LoginAsync(userDTO).Result;

        // Assert
        Assert.AreEqual(user.Email, result!.Email);
    }
}
