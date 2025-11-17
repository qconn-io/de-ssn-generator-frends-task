namespace Frends.GermanSSN.Generate.Definitions;

/// <summary>
/// Result of the German SSN generation task.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates if the task completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Generated German Social Security Number (Sozialversicherungsnummer).
    /// </summary>
    /// <example>65150315M12345</example>
    public string PersonalIdentityNumber { get; set; } = string.Empty;

    /// <summary>
    /// Formatted SSN with spaces for readability.
    /// </summary>
    /// <example>65 150315 M 123 4</example>
    public string FormattedSSN { get; set; } = string.Empty;

    /// <summary>
    /// Breakdown of SSN components for transparency.
    /// </summary>
    /// <example>object { AreaNumber = "65", BirthDate = "150315", SurnameCode = "13", SerialNumber = "123", CheckDigit = "4" }</example>
    public SSNComponents Components { get; set; } = new();

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { Message = "Invalid date format", AdditionalInfo = Exception }</example>
    public Error? Error { get; set; }
}
