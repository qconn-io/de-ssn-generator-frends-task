namespace Frends.GermanSSN.Generate.Definitions;

/// <summary>
/// SSN format options.
/// </summary>
public enum SSNFormat
{
    /// <summary>
    /// Standard 12-digit format without spaces (e.g., 123456789012).
    /// </summary>
    Standard12Digit,

    /// <summary>
    /// Formatted with spaces (e.g., 12 345678 9 012).
    /// </summary>
    FormattedWithSpaces,
}
