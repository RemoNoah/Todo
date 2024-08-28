using Todo.Domain.Models;

namespace Todo.Tests.Models;

[TestClass]
public class UserTest
{
    [TestMethod]
    public void TestVerifyPassword()
    {
        // Arrange
        string password = "testPassword";
        string salt = "testSalt";
        string hash = "r1IAISBN7yRSCxBf3yuvd90FwBDwJ4l2mrhcrYasoCc=";

        User user = new()
        {
            Salt = salt,
            Hash = hash
        };

        // Act
        bool isValidPassword = user.VerifyPassword(password);

        // Assert
        Assert.IsTrue(isValidPassword);
    }

    [TestMethod]
    public void TestSecondConstructor()
    {
        // Arrange
        string password = "testPassword";

        // Act
        User user = new(password);

        // Assert
        Assert.IsNotNull(user.Salt);
        Assert.IsNotNull(user.Hash);
        Assert.IsTrue(user.Salt.Length > 0);
        Assert.IsTrue(user.Hash.Length > 0);
        Assert.IsTrue(user.VerifyPassword(password));
    }
}
