using LearningDotNetCoreAPI.Controllers;
using LearningDotNetCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Tests
{
    [TestClass]
    public class AuthControllerTests
    {
        private readonly Mock<IUserValidator> _mockValidator = new();
        private readonly Mock<IConfiguration> _mockConfig = new();

        [Fact]
        public void Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            _mockValidator.Setup(v => v.IsValid("wrong", "wrong")).Returns(false);
            var controller = new AuthController(_mockConfig.Object, _mockValidator.Object);
            var request = new AuthController.LoginRequest("wrong", "wrong");

            // Act
            var result = controller.Login(request);

            // Assert
            Xunit.Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public void Login_ReturnsOk_WhenCredentialsAreValid()
        {
            // Arrange
            _mockValidator.Setup(v => v.IsValid("admin", "password123")).Returns(true);
            _mockConfig.Setup(c => c["Jwt:Key"]).Returns("YourSuperSecretKeyThatIsAtLeast32CharsLong!");
            _mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("MyFirstApi");
            _mockConfig.Setup(c => c["Jwt:Audience"]).Returns("MyFirstApiUsers");
            _mockConfig.Setup(c => c["Jwt:ExpiryMinutes"]).Returns("60");

            var controller = new AuthController(_mockConfig.Object, _mockValidator.Object);
            var request = new AuthController.LoginRequest("admin", "password123");

            // Act
            var result = controller.Login(request);

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }
    }
}
