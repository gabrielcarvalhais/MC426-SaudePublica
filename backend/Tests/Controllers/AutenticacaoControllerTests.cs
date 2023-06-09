using MC426_Backend.Controllers;
using MC426_Backend.Infrastructure.Identity;
using MC426_Backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Xunit;

namespace MC426_Backend.Tests.Controllers
{
    public class AutenticacaoControllerTests
    {
        [Fact]
        public async Task LoginValido()
        {
            var userModel = new LoginModel { Email = "admin@saudepublica.com", Password = "Admin@123" };

            var userManagerMock = new Mock<UserManager<Usuario>>(Mock.Of<IUserStore<Usuario>>(), null, null, null, null, null, null, null, null);
            var usuario = new Usuario { Id = "1", UserName = "Usuário teste", Email = userModel.Email };
            userManagerMock.Setup(x => x.FindByEmailAsync(userModel.Email)).ReturnsAsync(usuario);

            var signInManagerMock = new Mock<SignInManager<Usuario>>(userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<Usuario>>(), null, null, null, null);
            signInManagerMock.Setup(x => x.PasswordSignInAsync(userModel.Email, userModel.Password, true, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var roles = new List<string> { "Administrador" };
            userManagerMock.Setup(x => x.GetRolesAsync(usuario)).ReturnsAsync(roles);

            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>())).Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IAuthenticationService))).Returns(authServiceMock.Object);

            var controller = new AutenticacaoController(signInManagerMock.Object, userManagerMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };

            var result = await controller.Login(userModel);

            Assert.IsType<JsonResult>(result);
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(typeof(OkResult), jsonResult.Value.GetType());
        }

        [Fact]
        public async Task LoginInvalido()
        {
            var userModel = new LoginModel { Email = "admin@saudepublica.com", Password = "SenhaIncorreta@123" };

            var userManagerMock = new Mock<UserManager<Usuario>>(Mock.Of<IUserStore<Usuario>>(), null, null, null, null, null, null, null, null);
            var usuario = new Usuario { Id = "1", UserName = "Usuário teste", Email = userModel.Email };
            userManagerMock.Setup(x => x.FindByEmailAsync(userModel.Email)).ReturnsAsync(usuario);

            var signInManagerMock = new Mock<SignInManager<Usuario>>(userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<Usuario>>(), null, null, null, null);
            signInManagerMock.Setup(x => x.PasswordSignInAsync(userModel.Email, userModel.Password, true, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var roles = new List<string> { "Administrador" };
            userManagerMock.Setup(x => x.GetRolesAsync(usuario)).ReturnsAsync(roles);

            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(x => x.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>())).Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IAuthenticationService))).Returns(authServiceMock.Object);

            var controller = new AutenticacaoController(signInManagerMock.Object, userManagerMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };

            var result = await controller.Login(userModel);

            Assert.IsType<JsonResult>(result);
            var jsonResult = Assert.IsType<JsonResult>(result);
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(jsonResult.Value);
            var errorArray = JArray.FromObject(badRequestObjectResult.Value);
            var errorMessage = errorArray[0].ToString();
            var errorMessageObject = JsonConvert.DeserializeObject<dynamic>(errorMessage);
            var errorMessageString = (string)errorMessageObject.ErrorMessage;
            Assert.Equal("E-mail ou senha inválidos.", errorMessageString);
        }
    }
}
