using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services;
using WebApi.Tests.MockData;
using Xunit;

namespace WebApi.Tests.System.Services;

public class TestSnackbarService : IDisposable
{
    protected readonly WebAPIContext _context;
    public TestSnackbarService()
    {
        var options = new DbContextOptionsBuilder<WebAPIContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new WebAPIContext(options);
        _context.Database.EnsureCreated();
    }
    [Fact]
    public async Task GetSnackbar_ReturnSnackbarCollection()
    {
        // Arrange
        _context.Snackbar.AddRange(SnackbarMockData.GetSnackbars());
        _context.SaveChanges();

        var sut = new SnackbarService(_context);

        // Act
        var result = await sut.GetSnackbars();

        // Assert
        Assert.True(result.Count == SnackbarMockData.GetSnackbars().Count);
    }
    [Fact]
    public async Task GetSnackbarOne_ReturnSnackbar()
    {
        // Arrange
        _context.Snackbar.AddRange(SnackbarMockData.GetSnackbars());
        _context.SaveChanges();
        int snackBarId = 1;

        var sut = new SnackbarService(_context);
        // Act
        var result = await sut.GetSnackbar(snackBarId);

        // Assert
        Assert.Equal(snackBarId, result!.id);
    }
    [Fact]
    public async Task createSnackbar_CheckCount()
    {
        // Arrange
        _context.Snackbar.AddRange(SnackbarMockData.GetSnackbars());
        _context.SaveChanges();

        var newSnackbar = SnackbarMockData.addSnackbar();
        newSnackbar.id = 30;
        var sut = new SnackbarService(_context);

        // Act
        await sut.createSnackbar(newSnackbar);

        // Assert
        int expectRecordCount = SnackbarMockData.GetSnackbars().Count + 1;
        Assert.Equal(_context.Snackbar.Count(), expectRecordCount);
    }
    [Fact]
    public async Task deleteSnackbar_CheckCount()
    {
        // Arrange
        _context.Snackbar.AddRange(SnackbarMockData.GetSnackbars());
        _context.SaveChanges();

        var currentCount = SnackbarMockData.GetSnackbars().Count;
        var sut = new SnackbarService(_context);

        // Act
        await sut.deleteSnackbar(2);

        // Assert
        int expectRecordCount = currentCount - 1;
        Assert.Equal(_context.Snackbar.Count(), expectRecordCount);
    }
    [Fact]
    public async Task updateSnackbar_CheckIfChange()
    {
        // Arrange
        _context.Snackbar.AddRange(SnackbarMockData.GetSnackbars());
        _context.SaveChanges();

        var sut = new SnackbarService(_context);
        var snackBar = SnackbarMockData.GetSnackbars()[0];
        var oldTitle = snackBar.product;
        snackBar.product = "ChangedName";
        // Act
        await sut.updateSnackbar(snackBar);

        // Assert
        Assert.NotEqual(snackBar.product, oldTitle);
    }
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}