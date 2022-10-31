using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;

namespace CommBank.Tests;

public class UserControllerTests
{
    private readonly FakeCollections collections;

    public UserControllerTests()
    {
        collections = new();
    }

    [Fact]
    public async void GetAll()
    {
        // Arrange
        var users = collections.GetUsers();
        IUsersService service = new FakeUsersService(users, users[0]);
        UserController controller = new(service);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get();

        // Assert
        var index = 0;
        foreach (User user in result)
        {
            Assert.IsAssignableFrom<User>(user);
            Assert.Equal(users[index].Id, user.Id);
            Assert.Equal(users[index].Name, user.Name);
            index++;
        }
    }

    [Fact]
    public async void Get()
    {
        // Arrange
        var users = collections.GetUsers();
        IUsersService service = new FakeUsersService(users, users[0]);
        UserController controller = new(service);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get(users[0].Id!);

        // Assert
        Assert.IsAssignableFrom<User>(result.Value);
        Assert.Equal(users[0], result.Value);
        Assert.NotEqual(users[1], result.Value);
    }
}