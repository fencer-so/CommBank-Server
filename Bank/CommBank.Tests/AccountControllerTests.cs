using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;
using Microsoft.AspNetCore.Mvc;

namespace CommBank.Tests;

public class AccountControllerTests
{
    private readonly FakeCollections collections;

    public AccountControllerTests()
    {
        collections = new();
    }

    [Fact]
    public async void GetAll()
    {
        // Arrange
        var accounts = collections.GetAccounts();
        IAccountsService service = new FakeAccountsService(accounts, accounts[0]);
        AccountController controller = new(service);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get();

        // Assert
        var index = 0;
        foreach (Account account in result)
        {
            Assert.IsAssignableFrom<Account>(account);
            Assert.Equal(accounts[index].Id, account.Id);
            index++;
        }
    }

    [Fact]
    public async void Get()
    {
        // Arrange
        var accounts = collections.GetAccounts();
        IAccountsService service = new FakeAccountsService(accounts, accounts[0]);
        AccountController controller = new(service);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get(accounts[0].Id!);

        // Assert
        Assert.IsAssignableFrom<Account>(result.Value);
        Assert.Equal(accounts[0], result.Value);
        Assert.NotEqual(accounts[1], result.Value);
    }
}
