using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Recruitment.API.Controllers;
using Recruitment.API.Services;
using Recruitment.Contracts;
using System.Threading.Tasks;
using Xunit;

namespace Recruitment.Tests
{
    public class HashTests
    {
        private readonly HashController _hashController;
        private readonly Mock<IHashService> _hashService;

        public HashTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            _hashService = fixture.Freeze<Mock<IHashService>>();
            _hashController = fixture.Build<HashController>().OmitAutoProperties().Create();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(null, "12qwasZX")]
        [InlineData("testname", null)]
        [InlineData("a", "12qwasZX")]
        [InlineData("testname", "4")]
        [InlineData("123456789012345678901", "12qwasZX")]
        [InlineData("testname", "123456789012345678901")]
        public async Task CreateAsync_WrongInputData_ShouldReturnBadRequestResultAsync(string login, string passwod)
        {
            ContractModel contractModel = new ContractModel() { Login = login, Password = passwod};
            var result = await _hashController.CreateAsync(contractModel);
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateAsync_CorrectInputData_ShouldReturnOkAsync()
        {
            ContractModel contractModel = new ContractModel() { Login = "testname", Password = "12qwasZX" };
            string expectedHash = "test";
            _hashService.Setup(x => x.CreateHashAsync(contractModel)).ReturnsAsync(expectedHash);
            var result = await _hashController.CreateAsync(contractModel);
            result.Should().BeOfType<OkResult>();
        }
    }
}
