using System;
using System.Globalization;
using Frends.GermanSSN.Generate.Definitions;

namespace Frends.GermanSSN.Generate.Helpers;

internal static class InputValidator
{
    internal static void Validate(Input input, ValidationMode mode)
    {
        if (mode == ValidationMode.None)
            return;

        // Basic validation
        if (string.IsNullOrWhiteSpace(input.FirstName))
            throw new ArgumentException("FirstName cannot be empty.", nameof(input.FirstName));

        if (string.IsNullOrWhiteSpace(input.LastName))
            throw new ArgumentException("LastName cannot be empty.", nameof(input.LastName));

        if (string.IsNullOrWhiteSpace(input.DateOfBirth))
            throw new ArgumentException("DateOfBirth cannot be empty.", nameof(input.DateOfBirth));

        // Validate date format
        if (!DateTime.TryParse(input.DateOfBirth, CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
            throw new ArgumentException($"Invalid DateOfBirth format: {input.DateOfBirth}. Expected format: yyyy-MM-dd", nameof(input.DateOfBirth));

        // Strict validation
        if (mode == ValidationMode.Strict)
        {
            var age = DateTime.Now.Year - birthDate.Year;
            if (birthDate > DateTime.Now.AddYears(-age))
                age--;

            if (age < 0 || age > 150)
                throw new ArgumentException($"Invalid age calculated from DateOfBirth: {age} years.", nameof(input.DateOfBirth));

            if (birthDate > DateTime.Now)
                throw new ArgumentException("DateOfBirth cannot be in the future.", nameof(input.DateOfBirth));

            // Validate name format (letters, spaces, hyphens, apostrophes only)
            if (!IsValidName(input.FirstName))
                throw new ArgumentException($"FirstName contains invalid characters: {input.FirstName}", nameof(input.FirstName));

            if (!IsValidName(input.LastName))
                throw new ArgumentException($"LastName contains invalid characters: {input.LastName}", nameof(input.LastName));
        }
    }

    private static bool IsValidName(string name)
    {
        foreach (var c in name)
        {
            if (!char.IsLetter(c) && c != ' ' && c != '-' && c != '\'' && c != '.')
                return false;
        }

        return true;
    }
}
