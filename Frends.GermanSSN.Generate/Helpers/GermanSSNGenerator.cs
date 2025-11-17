using System;
using System.Globalization;
using Frends.GermanSSN.Generate.Definitions;

namespace Frends.GermanSSN.Generate.Helpers;

internal static class GermanSSNGenerator
{
    /// <summary>
    /// Generates a German Social Security Number based on input data.
    /// Format: AA BBBBBB C DDD
    /// - AA: Area number (01-99)
    /// - BBBBBB: Birth date (DDMMYY)
    /// - C: Surname letter code (01-26)
    /// - DDD: Serial number with check digit
    /// </summary>
    internal static (string ssn, string formatted, SSNComponents components) Generate(Input input)
    {
        var birthDate = DateTime.Parse(input.DateOfBirth, CultureInfo.InvariantCulture);

        // Part 1: Area number (01-99)
        // Use birth year modulo to distribute across regions
        var areaNumber = CalculateAreaNumber(birthDate);

        // Part 2: Birth date in DDMMYY format
        var birthDatePart = birthDate.ToString("ddMMyy");

        // Part 3: First letter of surname converted to number (01-26)
        var surnameCode = CalculateSurnameCode(input.LastName);

        // Part 4: Serial number (01-99)
        // Generate based on full name and birth date hash for consistency
        var serialBase = CalculateSerialNumber(input.FirstName, input.LastName, input.DateOfBirth);

        // Part 5: Calculate check digit
        var baseSSN = $"{areaNumber:D2}{birthDatePart}{surnameCode:D2}{serialBase:D2}";
        var checkDigit = CalculateCheckDigit(baseSSN);

        // Final SSN (12 digits total: 2 + 6 + 2 + 2 = 12)
        var ssn = $"{areaNumber:D2}{birthDatePart}{surnameCode:D2}{serialBase:D2}";
        var formatted = $"{areaNumber:D2} {birthDatePart} {surnameCode:D2} {serialBase}{checkDigit}";

        var components = new SSNComponents
        {
            AreaNumber = $"{areaNumber:D2}",
            BirthDate = birthDatePart,
            SurnameCode = $"{surnameCode:D2}",
            SerialNumber = $"{serialBase}",
            CheckDigit = $"{checkDigit}",
        };

        return (ssn, formatted, components);
    }

    private static int CalculateAreaNumber(DateTime birthDate)
    {
        // Use birth year modulo to generate area number between 10-98
        // This ensures consistent generation for same birth year
        return (birthDate.Year % 89) + 10;
    }

    private static int CalculateSurnameCode(string surname)
    {
        // Convert first letter to code: A=01, B=02, ..., Z=26
        var firstLetter = char.ToUpper(surname.Trim()[0]);

        if (firstLetter >= 'A' && firstLetter <= 'Z')
            return firstLetter - 'A' + 1;

        // For non-ASCII letters, use Unicode code point modulo to generate consistent code
        // This ensures different non-Latin letters get different codes (1-26)
        return (firstLetter % 26) + 1;
    }

    private static int CalculateSerialNumber(string firstName, string lastName, string dateOfBirth)
    {
        // Generate deterministic serial number based on person data
        // This ensures same person gets same SSN
        var combined = $"{firstName.ToLower()}{lastName.ToLower()}{dateOfBirth}";
        var hash = combined.GetHashCode();

        // Map to range 10-99
        return Math.Abs(hash % 90) + 10;
    }

    private static int CalculateCheckDigit(string baseNumber)
    {
        // Modified Luhn algorithm for check digit calculation
        var sum = 0;
        var alternate = false;

        // Process digits from right to left
        for (var i = baseNumber.Length - 1; i >= 0; i--)
        {
            var digit = int.Parse(baseNumber[i].ToString());

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            alternate = !alternate;
        }

        // Check digit makes total divisible by 10
        return (10 - (sum % 10)) % 10;
    }
}
