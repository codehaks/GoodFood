using System.Security.Claims;
using GoodFood.Application.Contracts;
using GoodFood.Infrastructure.Persistence.Models;
using GoodFood.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Moq;

namespace GoodFood.Tests.Web;
public class GatewayControllerTests
{
    [Fact]
    public async Task Sends_email_after_order_confirmed()
    {
        //Arrange

        var orderMock = new Mock<IOrderService>();
        var emailSeder = new Mock<IEmailSender>();

        var user = new ApplicationUser { Email = "user@gmail.com" };

        var store = new Mock<IUserStore<ApplicationUser>>();
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(
            store.Object, null, null, null, null, null, null, null, null);

        userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

        var sut = new GatewayController(orderMock.Object, emailSeder.Object, userManagerMock.Object);

        //Act
        await sut.GetAsync(Guid.NewGuid().ToString());

        //Assert
        emailSeder.Verify(e => e.SendEmailAsync(user.Email, "ثبت سفارش", "سفارش با موفقیت ثبت شد"), Times.Once);
    }
}
