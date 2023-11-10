using System.Reflection;
using GoodFood.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Moq;

namespace GoodFood.Tests.Web;

public class FoodImagePathServiceTests
{
    [Fact]
    public void GetPath_ShouldReturnCorrectPath()
    {
        // Arrange

        var enviromentMock = new Mock<IWebHostEnvironment>();
        var location = typeof(BannedWordCheckerTests).GetTypeInfo().Assembly.Location;
        var path = Path.GetDirectoryName(location);
        enviromentMock.Setup(e => e.ContentRootPath).Returns(path);

        var sut = new FoodImagePathService(enviromentMock.Object);

        // Act
        var result = sut.GetPath();

        // Assert
        Assert.EndsWith(Path.Combine("Files", "FoodImages"), result, StringComparison.Ordinal);
    }
}
