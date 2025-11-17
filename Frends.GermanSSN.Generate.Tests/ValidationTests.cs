using System;
using System.Threading;
using Frends.GermanSSN.Generate.Definitions;
using NUnit.Framework;

namespace Frends.GermanSSN.Generate.Tests;

[TestFixture]
public class ValidationTests
{
    [Test]
    public void ShouldThrowErrorForEmptyFirstName()
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
        Assert.That(ex.Message, Contains.Substring("FirstName"));
    }

    [Test]
    public void ShouldThrowErrorForEmptyLastName()
    {
        var input = new Input
        {
            FirstName = "Test",
            LastName = string.Empty,
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
        Assert.That(ex.Message, Contains.Substring("LastName"));
    }

    [Test]
    public void ShouldThrowErrorForInvalidDateFormat()
    {
        var input = new Input
        {
            FirstName = "Test",
            LastName = "User",
            DateOfBirth = "invalid-date",
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
        Assert.That(ex.Message, Contains.Substring("DateOfBirth"));
    }

    [Test]
    public void ShouldThrowErrorForFutureDateInStrictMode()
    {
        var futureDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
        var input = new Input
        {
            FirstName = "Test",
            LastName = "User",
            DateOfBirth = futureDate,
            Gender = Gender.Male,
        };

        var options = new Options
        {
            ValidationMode = ValidationMode.Strict,
            ThrowErrorOnFailure = true,
        };

        var ex = Assert.Throws<ArgumentException>(() =>
            GermanSSN.Execute(input, options, CancellationToken.None));

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Contains.Substring("future").Or.Contains("age"));
    }

    [Test]
    public void ShouldAllowValidationNoneMode()
    {
        var input = new Input
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            ValidationMode = ValidationMode.None,
            ThrowErrorOnFailure = false,
        };

        var result = GermanSSN.Execute(input, options, CancellationToken.None);

        // Should still fail but not during validation phase
        Assert.That(result.Success, Is.False);
    }

    [Test]
    public void ShouldValidateNameFormatInStrictMode()
    {
        var input = new Input
        {
            FirstName = "Test123",
            LastName = "User",
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            ValidationMode = ValidationMode.Strict,
            ThrowErrorOnFailure = true,
        };

        var ex = Assert.Throws<ArgumentException>(() =>
            GermanSSN.Execute(input, options, CancellationToken.None));

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Contains.Substring("invalid characters"));
    }

    [Test]
    public void ShouldAcceptValidNamesWithHyphensAndApostrophes()
    {
        var input = new Input
        {
            FirstName = "Jean-Pierre",
            LastName = "O'Connor",
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            ValidationMode = ValidationMode.Strict,
            ThrowErrorOnFailure = true,
        };

        var result = GermanSSN.Execute(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
    }
}
