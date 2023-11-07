using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodFood.Web.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace GoodFood.Tests.Web;
public class EmailSenderTests
{
    [Fact]
    public async Task Sends_email_with_valid_config()
    {
        // Arrange

        var configuration = new Mock<IConfiguration>();
        configuration.Setup(x => x["email:address"]).Returns("your-email@gmail.com");
        configuration.Setup(x => x["email:password"]).Returns("your-password");


        var sut = new EmailSender(configuration.Object);
        // Act

        await sut.SendEmailAsync("user@email", "test", "content");
        // Assert

    }
}
