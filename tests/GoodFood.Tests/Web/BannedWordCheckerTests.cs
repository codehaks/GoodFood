using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GoodFood.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace GoodFood.Tests.Web;

public class MyWebHostEnviroment : IWebHostEnvironment
{
    public string WebRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ContentRootPath
    {
        get
        {
            var location = typeof(BannedWordCheckerTests).GetTypeInfo().Assembly.Location;
            return Path.GetDirectoryName(location);
        }
        set => throw new NotImplementedException();
    }
    public string EnvironmentName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
public class BannedWordCheckerTests
{
    [Fact]
    public async Task Input_Contains_Banned_Word_returns_true()
    {
        //Arrange
        var sut = new BannedWordChecker(new MyWebHostEnviroment());

        //Act
        var result = await sut.CheckForBannedWordAsync("nazi");
        //Assert
        Assert.True(result);
    }
}
