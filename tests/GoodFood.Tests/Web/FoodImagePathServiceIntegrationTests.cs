using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodFood.Web.Pages;
using GoodFood.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;

namespace GoodFood.Tests.Web;
public class FoodImagePathServiceIntegrationTests : IClassFixture<WebApplicationFactory<IndexModel>>
{
    private readonly WebApplicationFactory<IndexModel> _factory;

    public FoodImagePathServiceIntegrationTests(WebApplicationFactory<IndexModel> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void GetPath_ShouldReturnCorrectPathInIntegrationTest()
    {
        // Arrange
        var client = _factory.CreateClient(); // This will create a test server and client

        client.

        var service = new FoodImagePathService(new HostingEnvironment
        {
            ContentRootPath = Directory.GetCurrentDirectory(),
            ContentRootFileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory())
        });

        // Act
        var result = service.GetPath();

        // Assert
        var expectedPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "FoodImages");
        Assert.Equal(expectedPath, result);
    }
}
