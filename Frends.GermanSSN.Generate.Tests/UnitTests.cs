using System.Threading;
using Frends.GermanSSN.Generate.Definitions;
using NUnit.Framework;

namespace Frends.GermanSSN.Generate.Tests;

[TestFixture]
public class UnitTests
{
    [Test]
    public void ShouldGenerateValidSSNForMale()
    {
        var input = new Input
        {
            FirstName = "Max",
            LastName = "Mustermann",
            DateOfBirth = "1990-01-15",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            Format = SSNFormat.Standard12Digit,
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = true,
        };

        var result = GermanSSN.Execute(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(result.PersonalIdentityNumber, Is.Not.Empty);
        Assert.That(result.PersonalIdentityNumber.Length, Is.EqualTo(12));
        Assert.That(result.FormattedSSN, Is.Not.Empty);
        Assert.That(result.Components.AreaNumber, Is.Not.Empty);
        Assert.That(result.Components.BirthDate, Is.EqualTo("150190"));
        Assert.That(result.Error, Is.Null);
    }

    [Test]
    public void ShouldGenerateValidSSNForFemale()
    {
        var input = new Input
        {
            FirstName = "Anna",
            LastName = "Schmidt",
            DateOfBirth = "1985-05-20",
            Gender = Gender.Female,
        };

        var options = new Options
        {
            Format = SSNFormat.Standard12Digit,
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = true,
        };

        var result = GermanSSN.Execute(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(result.PersonalIdentityNumber, Is.Not.Empty);
        Assert.That(result.PersonalIdentityNumber.Length, Is.EqualTo(12));
        Assert.That(result.Components.BirthDate, Is.EqualTo("200585"));
    }

    [Test]
    public void ShouldReturnFormattedSSNWhenRequested()
    {
        var input = new Input
        {
            FirstName = "Hans",
            LastName = "Mueller",
            DateOfBirth = "1975-12-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            Format = SSNFormat.FormattedWithSpaces,
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = true,
        };

        var result = GermanSSN.Execute(input, options, CancellationToken.None);

        Assert.That(result.Success, Is.True);
        Assert.That(result.PersonalIdentityNumber, Contains.Substring(" "));
        Assert.That(result.FormattedSSN, Contains.Substring(" "));
    }

    [Test]
    public void ShouldGenerateConsistentSSNForSamePerson()
    {
        var input1 = new Input
        {
            FirstName = "Thomas",
            LastName = "Weber",
            DateOfBirth = "1980-03-15",
            Gender = Gender.Male,
        };

        var input2 = new Input
        {
            FirstName = "Thomas",
            LastName = "Weber",
            DateOfBirth = "1980-03-15",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            Format = SSNFormat.Standard12Digit,
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = true,
        };

        var result1 = GermanSSN.Execute(input1, options, CancellationToken.None);
        var result2 = GermanSSN.Execute(input2, options, CancellationToken.None);

        Assert.That(result1.PersonalIdentityNumber, Is.EqualTo(result2.PersonalIdentityNumber));
    }

    [Test]
    public void ShouldCalculateSurnameCodeCorrectly()
    {
        var inputA = new Input
        {
            FirstName = "Test",
            LastName = "Anderson",
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var inputZ = new Input
        {
            FirstName = "Test",
            LastName = "Zimmermann",
            DateOfBirth = "1990-01-01",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            Format = SSNFormat.Standard12Digit,
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = true,
        };

        var resultA = GermanSSN.Execute(inputA, options, CancellationToken.None);
        var resultZ = GermanSSN.Execute(inputZ, options, CancellationToken.None);

        Assert.That(resultA.Components.SurnameCode, Is.EqualTo("01")); // A = 01
        Assert.That(resultZ.Components.SurnameCode, Is.EqualTo("26")); // Z = 26
    }

    [Test]
    public void ShouldIncludeAllSSNComponents()
    {
        var input = new Input
        {
            FirstName = "Peter",
            LastName = "Fischer",
            DateOfBirth = "1995-07-10",
            Gender = Gender.Male,
        };

        var options = new Options
        {
            Format = SSNFormat.Standard12Digit,
            ValidationMode = ValidationMode.Basic,
            ThrowErrorOnFailure = true,
        };

        var result = GermanSSN.Execute(input, options, CancellationToken.None);

        Assert.That(result.Components.AreaNumber, Is.Not.Empty);
        Assert.That(result.Components.BirthDate, Is.Not.Empty);
        Assert.That(result.Components.SurnameCode, Is.Not.Empty);
        Assert.That(result.Components.SerialNumber, Is.Not.Empty);
        Assert.That(result.Components.CheckDigit, Is.Not.Empty);

        Assert.That(result.Components.AreaNumber.Length, Is.EqualTo(2));
        Assert.That(result.Components.BirthDate.Length, Is.EqualTo(6));
        Assert.That(result.Components.SurnameCode.Length, Is.EqualTo(2));
        Assert.That(result.Components.SerialNumber.Length, Is.GreaterThanOrEqualTo(1).And.LessThanOrEqualTo(2));
        Assert.That(result.Components.CheckDigit.Length, Is.EqualTo(1));
    }
}
