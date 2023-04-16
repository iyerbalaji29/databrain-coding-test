using DataBrain.PAYG.Api.Controllers;
using DataBrain.PAYG.Exceptions;
using DataBrain.PAYG.Service.Constants;
using DataBrain.PAYG.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace DataBrain.PAYG.Api.Tests
{
    public class PAYGControllerTests
    {
        private readonly PAYGController _controller;
        private readonly Mock<IPAYGService> _paygServiceMock;
        private Mock<ILogger<PAYGController>> _loggerMock;

        public PAYGControllerTests()
        {
            _paygServiceMock = new Mock<IPAYGService>();
            _loggerMock = new Mock<ILogger<PAYGController>>();
            _controller = new PAYGController(_paygServiceMock.Object, _loggerMock.Object);
        }

        [Theory]
        [InlineData(2500.00f, PaymentFrequency.Weekly, 689.00f)]
        [InlineData(3462.69f, PaymentFrequency.Weekly, 1064.00f)]
        [InlineData(4807.69f, PaymentFrequency.Fortnightly, 1302.00f)]
        [InlineData(4807.69f, PaymentFrequency.Monthly, 867.00f)]
        [InlineData(4807.69f, PaymentFrequency.FourWeekly, 928.00f)]
        public void Get_ReturnsOk_WhenIncomeAndFrequencyIsValid(float earnings, PaymentFrequency frequency, float expectedTax)
        {
            // Arrange
            _paygServiceMock.Setup(x => x.GetTax(earnings, frequency)).Returns(expectedTax);

            // Act
            var result = _controller.Get(earnings, frequency);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<float>(okObjectResult.Value);
            Assert.Equal(expectedTax, response);
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(-123.00, null)]
        public void GetTax_ReturnsBadRequest_WhenIncomeAndFrequencyIsIncorrect(float earnings, PaymentFrequency frequency)
        {
            // Arrange
            _paygServiceMock.Setup(x => x.GetTax(earnings, frequency)).Throws(new BadRequestException("Payg service exception"));

            // Act
            var result = _controller.Get(earnings, frequency);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            
            var response = Assert.IsType<string>(badRequestObjectResult.Value);
            Assert.True(response.Contains("Payg service exception") == true);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestObjectResult.StatusCode);
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(-123.00, null)]
        public void GetTax_ReturnsInternalServerError_WhenPAYGServiceThrowsException(float earnings, PaymentFrequency frequency)
        {
            // Arrange
            _paygServiceMock.Setup(x => x.GetTax(earnings, frequency)).Throws(new Exception("Internal server error"));

            // Act
            var result = _controller.Get(earnings, frequency);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = (StatusCodeResult)result;
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }
    }
}