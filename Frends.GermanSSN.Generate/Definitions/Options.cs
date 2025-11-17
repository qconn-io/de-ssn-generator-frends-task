using System.ComponentModel;

namespace Frends.GermanSSN.Generate.Definitions;

/// <summary>
/// Additional parameters for German SSN generation.
/// </summary>
public class Options
{
    /// <summary>
    /// Format of the generated SSN.
    /// </summary>
    /// <example>SSNFormat.Standard12Digit</example>
    [DefaultValue(SSNFormat.Standard12Digit)]
    public SSNFormat Format { get; set; }

    /// <summary>
    /// Validation mode for input parameters.
    /// </summary>
    /// <example>ValidationMode.Basic</example>
    [DefaultValue(ValidationMode.Basic)]
    public ValidationMode ValidationMode { get; set; }

    /// <summary>
    /// Whether to throw an error on failure.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Overrides the error message on failure.
    /// </summary>
    /// <example>Custom error message</example>
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
