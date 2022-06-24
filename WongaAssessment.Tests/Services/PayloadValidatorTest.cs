using Common.Models;
using Common.Validators;
using ServiceA.Services;
using Xunit;

namespace WongaAssessment.Tests.Services;

public class ProducerServiceTest
{

    [Theory]
    [InlineData(true, "Peter", "Hello my name is, Peter")]
    [InlineData(true, "Jack", "Hello my name is, Jack")]
    [InlineData(true, "John", "Hello my name is, John")]
    [InlineData(true, "Sam", "Hello my name is, Sam")]
    public void Validate_ShouldReturnTrue_WhenPayloadIsValid(bool expected, string name, string message)
    {
        var payload = new PayloadBase
        {
            Message = message,
            Name = name
        };

        var validator = new PayLoadBaseValidator();
        var result = validator.Validate(payload);

        Assert.Equal(expected, result.IsValid);
    }

    [Theory]
    [InlineData(false, "peter", "Hello my name is, Peter")]
    [InlineData(false, "Jack", "")]
    [InlineData(false, "john", "Hello my name is, John")]
    [InlineData(false, "", "Hello my name is, Sam")]
    [InlineData(false, null, "Hello my name is, Sam")]
    [InlineData(false, null, null)]
    [InlineData(false, "Sam", null)]
    public void Validate_ShouldReturnTrue_WhenPayloadIsInvalid(bool expected, string? name, string? message)
    {
        var payload = new PayloadBase
        {
            Message = message,
            Name = name
        };

        var validator = new PayLoadBaseValidator();
        var result = validator.Validate(payload);

        Assert.Equal(expected, result.IsValid);
    }
}