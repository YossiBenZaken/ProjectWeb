using WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Services;
using WebApi.Tests.MockData;
using WebAPI.Models;

namespace WebApi.Tests.System.Controllers;
public class TestSnackbarController
{
    [Fact]
    public async Task GetAllSnackbars_ShouldReturn200()
    {
        // Arrange
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbars()).ReturnsAsync(SnackbarMockData.GetSnackbars());
        var sut = new SnackbarsController(snackbarService.Object);

        // Act
        var result = (OkObjectResult)await sut.GetSnackbar();

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(result.StatusCode, 200);
    }

    [Fact]
    public async Task GetAllSnackbars_ShouldReturn204()
    {
        // Arrange
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbars()).ReturnsAsync(SnackbarMockData.GetEmptySnackbars());
        var sut = new SnackbarsController(snackbarService.Object);

        // Act
        var result = (NoContentResult)await sut.GetSnackbar();

        /// Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, result.StatusCode);
        snackbarService.Verify(_ => _.GetSnackbars(), Times.Exactly(1));
    }

    [Fact]
    public async Task GetSnackbarById_ShouldReturnSnackbar()
    {
        // Arrange
        var snackbar = new Snackbar { id = 25, product = "Test", price = 5 };
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbar(snackbar.id)).ReturnsAsync(snackbar);
        var sut = new SnackbarsController(snackbarService.Object);

        // Act
        var actionResult = await sut.GetSnackbar(snackbar.id);
        // Assert
        var okObjectResult = actionResult as OkObjectResult;
        Assert.NotNull(okObjectResult);
        Assert.Equal(okObjectResult.StatusCode, 200);
        var model = okObjectResult.Value as Snackbar;
        Assert.NotNull(model);

        Assert.Equal(snackbar.id, model.id);
    }
    [Fact]
    public async Task GetSnackbarById_ShouldReturn404()
    {
        // Arrange
        var snackbar = new Snackbar { id = 1, product = "Test", price = 5 };
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbar(snackbar.id)).ReturnsAsync(snackbar);
        var sut = new SnackbarsController(snackbarService.Object);
        int fakeId = 25;
        // Act
        var actionResult = await sut.GetSnackbar(fakeId);

        // Assert
        var notFoundResult = actionResult as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.IsType<NotFoundResult>(actionResult);
    }
    [Fact]
    public async Task DeleteSnackbarById_ShouldReturn204()
    {
        // Arrange
        var snackbar = new Snackbar { id = 25, product = "Test", price = 5 };
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbar(snackbar.id)).ReturnsAsync(snackbar);
        var sut = new SnackbarsController(snackbarService.Object);

        // Act
        var result = (NoContentResult)await sut.DeleteSnackbar(snackbar.id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, result.StatusCode);
    }
    [Fact]
    public async Task DeleteSnackbarById_ShouldReturn404()
    {
        // Arrange
        var snackbar = new Snackbar { id = 1, product = "Test", price = 5 };
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbar(snackbar.id)).ReturnsAsync(snackbar);
        var sut = new SnackbarsController(snackbarService.Object);
        var fakeId = 25;
        // Act
        var actionResult = await sut.DeleteSnackbar(fakeId);

        // Assert
        var notFoundResult = actionResult as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.IsType<NotFoundResult>(actionResult);
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
        Assert.NotNull(result);
        Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, result.StatusCode);
        snackbarService.Verify(_ => _.createSnackbar(newSnackbar), Times.Exactly(1));
    }
    [Fact]
    public async Task UpdateSnackbar_ShouldCallPutOnce()
    {
        // Arrage
        var snackbars = SnackbarMockData.GetSnackbars();
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbar(snackbars[2].id)).ReturnsAsync(snackbars[2]);
        var sut = new SnackbarsController(snackbarService.Object);
        var updatedSnackbar = SnackbarMockData.updateSnackbar();

        // Act
        var result = (NoContentResult)await sut.PutSnackbar(snackbars[2].id, updatedSnackbar);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, result.StatusCode);
        snackbarService.Verify(_ => _.updateSnackbar(updatedSnackbar), Times.Exactly(1));
    }
    [Fact]
    public async Task UpdateSnackbar_ShouldReturnBadRequest()
    {
        // Arrage
        var snackbars = SnackbarMockData.GetSnackbars();
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbar(snackbars[0].id)).ReturnsAsync(snackbars[0]);
        var sut = new SnackbarsController(snackbarService.Object);
        var updatedSnackbar = SnackbarMockData.updateSnackbar();
        updatedSnackbar.id = 1;

        // Act
        var result = (BadRequestResult)await sut.PutSnackbar(snackbars[0].id, updatedSnackbar);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        Assert.Equal(400, result.StatusCode);
        snackbarService.Verify(_ => _.updateSnackbar(updatedSnackbar), Times.Exactly(0));
    }
    [Fact]
    public async Task UpdateSnackbar_ShouldReturnNotFound()
    {
        // Arrage
        var snackbars = SnackbarMockData.GetSnackbars();
        var snackbarService = new Mock<ISnackbarService>();
        snackbarService.Setup(_ => _.GetSnackbar(snackbars[0].id)).ReturnsAsync(snackbars[0]);
        var sut = new SnackbarsController(snackbarService.Object);
        var updatedSnackbar = SnackbarMockData.updateSnackbar();

        // Act
        snackbars[0].id = 1;
        updatedSnackbar.id = 1;
        var result = (NotFoundResult)await sut.PutSnackbar(snackbars[0].id, updatedSnackbar);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, result.StatusCode);
        snackbarService.Verify(_ => _.updateSnackbar(updatedSnackbar), Times.Exactly(0));
    }
}
