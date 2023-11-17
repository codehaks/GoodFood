using System.Reflection;
using GoodFood.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
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

    public class FoodImagePathServiceIntegrationTests
    {
        [Fact]
        public void GetPath_ShouldReturnCorrectPathInIntegrationTest()
        {
            // Arrange
            var contentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "TestContentRoot");
            var environment = new HostingEnvironment
            {
                ContentRootPath = contentRootPath,
                ContentRootFileProvider = new PhysicalFileProvider(contentRootPath)
            };

            var service = new FoodImagePathService(environment);

            // Act
            var result = service.GetPath();

            // Assert
            var expectedPath = Path.Combine(contentRootPath, "Files", "FoodImages");
            Assert.Equal(expectedPath, result);
        }
    }
}
