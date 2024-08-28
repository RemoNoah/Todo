using Todo.Api.Controllers;
using Todo.Domain.DTO.Role;
using Todo.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Todo.Tests.Controllers;

[TestClass]
public class RoleControllerTest
{
    private RoleController _roleController = null!;
    private readonly Mock<IRoleService> _roleServiceMock = new();

    [TestInitialize]
    public void Setup()
    {
        _roleController = new RoleController(_roleServiceMock.Object);
    }

    [TestMethod]
    public async Task GetAllWithoutId_ReturnsOK()
    {
        // Arrange
        IEnumerable<RoleWithoutIdDTO> roleDTOs = [new() { Name = "test" }, new() { Name = "test1" }];

        _ = _roleServiceMock.Setup(r => r.GetAllWithoutId()).ReturnsAsync(roleDTOs);

        // Act
        ActionResult<IEnumerable<RoleWithoutIdDTO>> actual = await _roleController.GetAllWithoutId();

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<RoleWithoutIdDTO>));
        if (result.Value is not IEnumerable<RoleWithoutIdDTO>)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetAllWithId_ReturnsOK()
    {
        // Arrange
        IEnumerable<RoleWithIdDTO> roleDTOs = [new() { Name = "test" }, new() { Name = "test1" }];

        _ = _roleServiceMock.Setup(r => r.GetAllWithId()).ReturnsAsync(roleDTOs);

        // Act
        ActionResult<IEnumerable<RoleWithoutIdDTO>> actual = await _roleController.GetAllWithId();

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<RoleWithIdDTO>));
        if (result.Value is not IEnumerable<RoleWithIdDTO>)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetIdByName_NameIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        string name = "";

        // Act
        ActionResult<Guid> actual = await _roleController.GetIdByName(name);

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
    public async Task GetIdByName_ResultIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        string name = "test";

        _ = _roleServiceMock.Setup(r => r.GetIdByName(It.IsAny<string>())).ReturnsAsync(Guid.Empty);

        // Act
        ActionResult<Guid> actual = await _roleController.GetIdByName(name);

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
    public async Task GetIdByName_ReturnsOk()
    {
        // Arrange
        string name = "test";

        _ = _roleServiceMock.Setup(r => r.GetIdByName(It.IsAny<string>())).ReturnsAsync(Guid.NewGuid());

        // Act
        ActionResult<Guid> actual = await _roleController.GetIdByName(name);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(Guid));
        if (result.Value is not Guid)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetNameById_NameIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        Guid id = Guid.Empty;

        // Act
        ActionResult<string> actual = await _roleController.GetNameById(id);

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
    public async Task GetNameById_ResultIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        _ = _roleServiceMock.Setup(r => r.GetNameById(It.IsAny<Guid>())).ReturnsAsync("");

        // Act
        ActionResult<string> actual = await _roleController.GetNameById(id);

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
    public async Task GetNameById_ReturnsOk()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        _ = _roleServiceMock.Setup(r => r.GetNameById(It.IsAny<Guid>())).ReturnsAsync("Test");

        // Act
        ActionResult<string> actual = await _roleController.GetNameById(id);

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
    public async Task Create_NameIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO role = new() { Name = "" };

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.Create(role);

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
    public async Task Create_DTOIsNull_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO role = null!;

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.Create(role);

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
    public async Task Create_ResultIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO role = new() { Name = "aa" };

        _ = _roleServiceMock.Setup(r => r.Create(It.IsAny<RoleWithoutIdDTO>()))!.ReturnsAsync(new RoleWithoutIdDTO());

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.Create(role);

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
    public async Task Create_ReturnsOk()
    {
        // Arrange
        RoleWithoutIdDTO role = new() { Name = "Test" };

        _ = _roleServiceMock.Setup(r => r.Create(It.IsAny<RoleWithoutIdDTO>())).ReturnsAsync(role);

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.Create(role);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(RoleWithoutIdDTO));
        if (result.Value is not RoleWithoutIdDTO)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task UpdateById_DTOIsNull_ReturnsBadRequest()
    {
        // Arrange
        RoleWithIdDTO roleWithIdDTO = null!;

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.UpdateById(roleWithIdDTO);

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
    public async Task UpdateById_NameIsNull_ReturnsBadRequest()
    {
        // Arrange
        RoleWithIdDTO roleWithIdDTO = new() { Id = Guid.NewGuid(), Name = "" };

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.UpdateById(roleWithIdDTO);

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
    public async Task UpdateById_ResultIsNUll_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO roleWithoutIdDTO = new() { Name = "test" };
        RoleWithIdDTO roleWithIdDTO = new() { Id = Guid.NewGuid(), Name = roleWithoutIdDTO.Name };

        _ = _roleServiceMock.Setup(r => r.UpdateById(It.IsAny<RoleWithIdDTO>()))!.ReturnsAsync(new RoleWithoutIdDTO());

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.UpdateById(roleWithIdDTO);

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
    public async Task UpdateById_ReturnsOk()
    {
        // Arrange
        RoleWithoutIdDTO roleWithoutIdDTO = new() { Name = "Test" };
        RoleWithIdDTO roleWithIdDTO = new() { Id = Guid.NewGuid(), Name = roleWithoutIdDTO.Name };

        _ = _roleServiceMock.Setup(r => r.UpdateById(It.IsAny<RoleWithIdDTO>())).ReturnsAsync(roleWithoutIdDTO);

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.UpdateById(roleWithIdDTO);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(RoleWithoutIdDTO));
        if (result.Value is not RoleWithoutIdDTO)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task UpdateByOldName_NameIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO roleWithoutIdDTO = new() { Name = "New" };
        RoleUpdateByOldNameDTO roleUpdateByOldNameDTO = new() { NewName = roleWithoutIdDTO.Name, OldName = "" };

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.UpdateByOldName(roleUpdateByOldNameDTO);

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
    public async Task UpdateByOldName_ResultIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO roleWithoutIdDTO = new() { Name = "New" };
        RoleUpdateByOldNameDTO roleUpdateByOldNameDTO = new() { NewName = roleWithoutIdDTO.Name, OldName = "Test" };

        _ = _roleServiceMock.Setup(r => r.UpdateByOldName(It.IsAny<RoleUpdateByOldNameDTO>())).ReturnsAsync(new RoleWithoutIdDTO());

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.UpdateByOldName(roleUpdateByOldNameDTO);

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
    public async Task UpdateByOldName_ReturnsOk()
    {
        // Arrange
        RoleWithoutIdDTO roleWithoutIdDTO = new() { Name = "New" };
        RoleUpdateByOldNameDTO roleUpdateByOldNameDTO = new() { NewName = roleWithoutIdDTO.Name, OldName = "Test" };

        _ = _roleServiceMock.Setup(r => r.UpdateByOldName(It.IsAny<RoleUpdateByOldNameDTO>())).ReturnsAsync(roleWithoutIdDTO);

        // Act
        ActionResult<RoleWithoutIdDTO> actual = await _roleController.UpdateByOldName(roleUpdateByOldNameDTO);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(RoleWithoutIdDTO));
        if (result.Value is not RoleWithoutIdDTO)
        {
            throw new ArgumentNullException();
        }

        Assert.AreEqual(roleWithoutIdDTO, result.Value);
    }

    [TestMethod]
    public async Task DeleteById_IdIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        Guid id = Guid.Empty;

        // Act
        ActionResult<bool> actual = await _roleController.DeleteById(id);

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
    public async Task DeleteById_ResultIsFalse_ReturnsBadRequest()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        _ = _roleServiceMock.Setup(r => r.DeleteById(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        ActionResult<bool> actual = await _roleController.DeleteById(id);

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
    public async Task DeleteById_ReturnsOk()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        _ = _roleServiceMock.Setup(r => r.DeleteById(It.IsAny<Guid>())).ReturnsAsync(true);

        // Act
        ActionResult<bool> actual = await _roleController.DeleteById(id);

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
    public async Task DeleteByName_NameIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        string name = "";

        // Act
        ActionResult<bool> actual = await _roleController.DeleteByName(name);

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
    public async Task DeleteByName_ResultIsFalse_ReturnsBadRequest()
    {
        // Arrange
        string name = "Test";

        _ = _roleServiceMock.Setup(r => r.DeleteByName(It.IsAny<string>())).ReturnsAsync(false);

        // Act
        ActionResult<bool> actual = await _roleController.DeleteByName(name);

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
    public async Task DeleteByName_ReturnsOk()
    {
        // Arrange
        string name = "Test";

        _ = _roleServiceMock.Setup(r => r.DeleteByName(It.IsAny<string>())).ReturnsAsync(true);

        // Act
        ActionResult<bool> actual = await _roleController.DeleteByName(name);

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