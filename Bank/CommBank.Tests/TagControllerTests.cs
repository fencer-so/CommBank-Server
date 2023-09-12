using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;

namespace CommBank.Tests;

public class TagControllerTests
{
    private readonly FakeCollections collections;

    public TagControllerTests()
    {
        collections = new();
    }

    [Fact]
    public async void GetAll()
    {
        // Arrange
        var tags = collections.GetTags();
        ITagsService service = new FakeTagsService(tags, tags[0]);
        TagController controller = new(service);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get();

        // Assert
        var index = 0;
        foreach (Tag tag in result)
        {
            Assert.IsAssignableFrom<Tag>(tag);
            Assert.Equal(tags[index].Id, tag.Id);
            Assert.Equal(tags[index].Name, tag.Name);
            index++;
        }
    }

    [Fact]
    public async void Get()
    {
        // Arrange
        var tags = collections.GetTags();
        ITagsService service = new FakeTagsService(tags, tags[0]);
        TagController controller = new(service);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get(tags[0].Id!);

        // Assert
        Assert.IsAssignableFrom<Tag>(result.Value);
        Assert.Equal(tags[0], result.Value);
        Assert.NotEqual(tags[1], result.Value);
    }
}