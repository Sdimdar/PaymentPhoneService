using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using PaymentPhoneService.API.Models;
using PaymentPhoneService.Tests.Repository;

namespace PaymentPhoneService.Tests;

public class PaymentTests : IClassFixture<CustomFixture<Program>>
{
    private readonly HttpClient _httpClient;
    
    public PaymentTests(CustomFixture<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }
    
    public static IEnumerable<object[]> ValidRequests()
    {
        yield return new object[] { "+77013339800", 1 };
        yield return new object[] { "+7 777 333 9800", 1 };
        yield return new object[] { "8 705 333 9800", 1 };
        yield return new object[] { "8 747 333 9800", 1 };
        yield return new object[] { "8 700 333 9800", 1 };
        yield return new object[] { "+7 (707) 837 64 54", 1.2M };
        yield return new object[] { "+7 (708) 837 64 54", decimal.MaxValue };
    }

    [Theory]
    [MemberData(nameof(ValidRequests))]
    public async void SendValidRequest_ReturnOkStatus(string phoneNumber, decimal amount)
    {
        // Arrange
        PaymentRequest request = new()
        {
            PhoneNumber = phoneNumber,
            Amount = amount
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Payment/PhonePayment", request);
        var dataAsString = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ResponseVM>(dataAsString);

        // Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data!.IsSuccess.Should().BeTrue();
        data.Error.Should().BeNull();
    }
    
    public static IEnumerable<object[]> InvalidPhoneNumbers()
    {
        yield return new object[] { "qwerty", 1 };
        yield return new object[] { "870733398003", 1 };
        yield return new object[] { "8707333980032231", 1 };
        yield return new object[] { "+7705qwertyu", 1 };
        yield return new object[] { "8705qwertyu", 1 };
        yield return new object[] { "+7707qwer", 1 };
        yield return new object[] { "8707qwer", 1 };
    }
    
    [Theory]
    [MemberData(nameof(InvalidPhoneNumbers))]
    public async void SendInvalidPhoneNumber_ReturnOkAndErrorMessage(string phoneNumber, decimal amount)
    {
        // Arrange
        PaymentRequest request = new()
        {
            PhoneNumber = phoneNumber,
            Amount = amount
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Payment/PhonePayment", request);
        var dataAsString = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ResponseVM>(dataAsString);

        // Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data!.IsSuccess.Should().BeFalse();
        data.Error.Should().NotBeNull();
    }
    
    public static IEnumerable<object[]> InvalidAmounts()
    {
        yield return new object[] { "+7 (707) 333 98 30", 0 };
        yield return new object[] { "+7 (700) 333 9830", 0.05 };
        yield return new object[] { "8708 333 9830", 0.5 };
        yield return new object[] { "8 (777) 3339830", decimal.MinValue };
        yield return new object[] { "+77773339830", decimal.MinusOne };
    }
    [Theory]
    [MemberData(nameof(InvalidAmounts))]
    public async void SendInvalidAmount_ReturnOkAndErrorMessage(string phoneNumber, decimal amount)
    {
        // Arrange
        PaymentRequest request = new()
        {
            PhoneNumber = phoneNumber,
            Amount = amount
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Payment/PhonePayment", request);
        var dataAsString = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ResponseVM>(dataAsString);

        // Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data!.IsSuccess.Should().BeFalse();
        data.Error.Should().NotBeNull();
    }
    
    public static IEnumerable<object[]> UnsupportedMobileCodes()
    {
        yield return new object[] { "+7 (776) 333 9830", 1 };
        yield return new object[] { "+81013339830", 1 };
    }
    [Theory]
    [MemberData(nameof(InvalidAmounts))]
    public async void SendInvalidMobileCod_ReturnOkAndErrorMessage(string phoneNumber, decimal amount)
    {
        // Arrange
        PaymentRequest request = new()
        {
            PhoneNumber = phoneNumber,
            Amount = amount
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/Payment/PhonePayment", request);
        var dataAsString = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ResponseVM>(dataAsString);

        // Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data!.IsSuccess.Should().BeFalse();
        data.Error.Should().NotBeNull();
    }
    
}