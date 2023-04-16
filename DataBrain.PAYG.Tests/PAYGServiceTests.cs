namespace DataBrain.PAYG.Tests;
using DataBrain.PAYG.Service.Services;
using DataBrain.PAYG.Service.Constants;
using Moq;
using DataBrain.PAYG.Exceptions;
using Microsoft.Extensions.Logging;

public class PAYGServiceTests
{
    private readonly IPAYGService serviceUnderTest;
    private Mock<ILogger<PAYGService>> _loggerMock;
    public PAYGServiceTests()
    {
        _loggerMock = new Mock<ILogger<PAYGService>>();
        serviceUnderTest = new PAYGService(_loggerMock.Object);
    }

    [Theory]
    [InlineData(2500.00f, PaymentFrequency.Weekly, 689.00f)]
    [InlineData(3462.69f, PaymentFrequency.Weekly, 1064.00f)]
    [InlineData(4807.69f, PaymentFrequency.Fortnightly, 1302.00f)]
    [InlineData(4807.69f, PaymentFrequency.Monthly, 867.00f)]
    [InlineData(4807.69f, PaymentFrequency.FourWeekly, 928.00f)]
    public void Should_Successfully_Match_Expected_PAYG_Tax_Based_on_Earnings_And_Frequency(float earnings, PaymentFrequency frequency, float expectedTax)
    {
        var payg = serviceUnderTest.GetTax(earnings, frequency);
        Assert.True(payg > 0);
        Assert.True(payg == expectedTax);
    }

    [Theory]
    [InlineData(0, null)]
    [InlineData(-123.00, null)]
    public void Should_Throw_BadRequest_Exception_For_Invalid_Incorrect_Earnings_Or_Frequency(float earnings, PaymentFrequency frequency)
    {
        var ex =  Assert.Throws<BadRequestException>(() => serviceUnderTest.GetTax(earnings, frequency)); 
        Assert.Equal($"Frequency is invalid", ex.Message);
    }
}