namespace Frends.GermanSSN.Generate.Definitions;

/// <summary>
/// Breakdown of German SSN components.
/// </summary>
public class SSNComponents
{
    /// <summary>
    /// Area number (2 digits, 01-99).
    /// </summary>
    /// <example>65</example>
    public string AreaNumber { get; set; } = string.Empty;

    /// <summary>
    /// Birth date in DDMMYY format (6 digits).
    /// </summary>
    /// <example>150315</example>
    public string BirthDate { get; set; } = string.Empty;

    /// <summary>
    /// First letter of surname converted to number (2 digits, 01-26).
    /// </summary>
    /// <example>13</example>
    public string SurnameCode { get; set; } = string.Empty;

    /// <summary>
    /// Serial number (1 digit, 1-9).
    /// </summary>
    /// <example>3</example>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Check digit (1 digit, 0-9).
    /// </summary>
    /// <example>4</example>
    public string CheckDigit { get; set; } = string.Empty;
}
