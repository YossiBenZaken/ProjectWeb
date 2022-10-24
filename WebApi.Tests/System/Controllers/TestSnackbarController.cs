using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services;
using WebApi.Tests.MockData;
using WebAPI.Models;

namespace WebApi.Tests.System.Controllers;

public class TestSnackbarController
{
    [Fact]
    public async Task GetAllAsync_ShouldReturn200()
    {
        // Arrange
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbars()).ReturnsAsync(SnackbarMockData.GetSnackbars());
        var sut = new SnackbarsController(snackbarService.Object);

        // Act
        var result = (OkObjectResult)await sut.GetSnackbar();

        result.GetType().Should().Be(typeof(OkObjectResult));
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturn204()
    {
        // Arrange
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbars()).ReturnsAsync(SnackbarMockData.GetEmptySnackbars());
        var sut = new SnackbarsController(snackbarService.Object);

        // Act
        var result = (NoContentResult)await sut.GetSnackbar();

        result.GetType().Should().Be(typeof(NoContentResult));
        result.StatusCode.Should().Be(204);
        snackbarService.Verify(_ => _.GetSnackbars(), Times.Exactly(1));
    }

    [Fact]
    public async Task GetSnackbarById()
    {
        // Arrange
        var snackbar = new Snackbar { id = 25, product = "Test", price = 5 };
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbar(snackbar.id)).ReturnsAsync(snackbar);
        var sut = new SnackbarsController(snackbarService.Object);

        // Act
        var result = await sut.GetSnackbar(snackbar.id);

        Assert.Equal(result.Value!.id, snackbar.id);
    }

    [Fact]
    public async Task SaveSnackbar_ShouldCallPostOnce()
    {
        // Arrange
        var snackbarService = new Mock<ISnackbarService>();
        var newSnackbar = SnackbarMockData.addSnackbar();
        var sut = new SnackbarsController(snackbarService.Object);

        // Act
        var result = (CreatedAtActionResult)await sut.PostSnackbar(newSnackbar);

        // Assert
        result.GetType().Should().Be(typeof(CreatedAtActionResult));
        result.StatusCode.Should().Be(201);
        snackbarService.Verify(_ => _.createSnackbar(newSnackbar), Times.Exactly(1));
    }
    [Fact]
    public async Task UpdateSnackbar_ShouldCallPutOnce()
    {
        // Arrage
        var snackbarService = new Mock<ISnackbarService>();
        var newSnackbar = SnackbarMockData.addSnackbar();
        var sut = new SnackbarsController(snackbarService.Object);
        var updatedSnackbar = SnackbarMockData.updateSnackbar();

        // Act
        await sut.PostSnackbar(newSnackbar);
        var result = (NoContentResult)await sut.PutSnackbar(newSnackbar.id, updatedSnackbar);

        // Assert
        result.GetType().Should().Be(typeof(NoContentResult));
        result.StatusCode.Should().Be(204);
        snackbarService.Verify(_ => _.createSnackbar(newSnackbar), Times.Exactly(1));
        snackbarService.Verify(_ => _.updateSnackbar(updatedSnackbar), Times.Exactly(1));
    }
}
