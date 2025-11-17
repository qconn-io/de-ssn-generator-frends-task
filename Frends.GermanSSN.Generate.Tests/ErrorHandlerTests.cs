using System;
using System.Threading;
using Frends.GermanSSN.Generate.Definitions;
using NUnit.Framework;

namespace Frends.GermanSSN.Generate.Tests;

[TestFixture]
public class ErrorHandlerTests
{
    [Test]
    public void ShouldThrowErrorWhenThrowErrorOnFailureIsTrue()
    {
        var input = new Input
        {
            FirstName = string.Empty,
            LastName = "Test",
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = true,
        };

        var ex = Assert.Throws<ArgumentException>(() =>
            GermanSSN.Execute(input, options, CancellationToken.None));

        Assert.That(ex, Is.Not.Null);
    }

    [Test]
    public void ShouldReturnFailedResultWhenThrowErrorOnFailureIsFalse()
    {
        var input = new Input
        {
            FirstName = string.Empty,
            LastName = "Test",
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = false,
        };

        var result = GermanSSN.Execute(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
        Assert.That(result.Error.Message, Is.Not.Empty);
    }

    [Test]
    public void ShouldUseCustomErrorMessageOnFailure()
    {
        const string customErrorMessage = "Custom error occurred during SSN generation";

        var input = new Input
        {
            FirstName = string.Empty,
            LastName = "Test",
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = true,
            ErrorMessageOnFailure = customErrorMessage,
        };

        var ex = Assert.Throws<Exception>(() =>
            GermanSSN.Execute(input, options, CancellationToken.None));

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Contains.Substring(customErrorMessage));
    }

    [Test]
    public void ShouldIncludeOriginalExceptionInError()
    {
        var input = new Input
        {
            FirstName = string.Empty,
            LastName = "Test",
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = false,
        };

        var result = GermanSSN.Execute(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
        Assert.That(result.Error.AdditionalInfo, Is.Not.Null);
        Assert.That(result.Error.AdditionalInfo, Is.InstanceOf<Exception>());
    }
}
