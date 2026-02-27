using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;

namespace CommBank.Tests;

public class TransactionControllerTests
{
    private readonly FakeCollections collections;

    public TransactionControllerTests()
    {
        collections = new();
    }

    [Fact]
    public async void GetAll()
    {
        // Arrange
        var transactions = collections.GetTransactions();
        ITransactionsService service = new FakeTransactionsService(transactions, transactions[0]);
        TransactionController controller = new(service);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get();

        // Assert
        var index = 0;
        foreach (Transaction transaction in result)
        {
            Assert.IsAssignableFrom<Transaction>(transaction);
            Assert.Equal(transactions[index].Id, transaction.Id);
            index++;
        }
    }

    [Fact]
    public async void Get()
    {
        // Arrange
        var transactions = collections.GetTransactions();
        ITransactionsService service = new FakeTransactionsService(transactions, transactions[0]);
        TransactionController controller = new(service);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get(transactions[0].Id!);

        // Assert
        Assert.IsAssignableFrom<Transaction>(result.Value);
        Assert.Equal(transactions[0], result.Value);
        Assert.NotEqual(transactions[1], result.Value);
    }
}