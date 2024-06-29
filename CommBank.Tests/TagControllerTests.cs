using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;
using Microsoft.AspNetCore.Http;
using Xunit;
using System.Threading.Tasks;

namespace CommBank.Tests;

public class TagControllerTests
{
    private readonly FakeCollections _collections;

    public TagControllerTests()
    {
        _collections = new FakeCollections();
    }

    [Fact]
    public async Task GetAllTags_ShouldReturnAllTags()
    {
        // Arrange
        var expectedTags = _collections.GetTags();
        ITagsService service = new FakeTagsService(expectedTags, expectedTags[0]);
        var controller = new TagController(service);

        // Set up HttpContext
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        // Act
        var result = await controller.Get();

        // Assert
        Assert.Equal(expectedTags.Count, result.Count);
        for (int i = 0; i < result.Count; i++)
        {
            Assert.IsType<Tag>(result[i]);
            Assert.Equal(expectedTags[i].Id, result[i].Id);
            Assert.Equal(expectedTags[i].Name, result[i].Name);
        }
    }

    [Fact]
    public async Task GetTagById_ShouldReturnCorrectTag()
    {
        // Arrange
        var expectedTags = _collections.GetTags();
        var expectedTag = expectedTags[0];
        ITagsService service = new FakeTagsService(expectedTags, expectedTag);
        var controller = new TagController(service);

        // Set up HttpContext
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        // Act
        var result = await controller.Get(expectedTag.Id!);

        // Assert
        Assert.IsType<Tag>(result.Value);
        Assert.Equal(expectedTag.Id, result.Value.Id);
        Assert.Equal(expectedTag.Name, result.Value.Name);
        Assert.NotEqual(expectedTags[1].Id, result.Value.Id);
    }
}

