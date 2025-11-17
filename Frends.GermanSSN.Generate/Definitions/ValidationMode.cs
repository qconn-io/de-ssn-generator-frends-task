namespace Frends.GermanSSN.Generate.Definitions;

/// <summary>
/// Validation mode for input parameters.
/// </summary>
public enum ValidationMode
{
    /// <summary>
    /// No validation performed.
    /// </summary>
    None,

    /// <summary>
    /// Basic validation (required fields, date format).
    /// </summary>
    Basic,

    /// <summary>
    /// Strict validation (age limits, name format, etc.).
    /// </summary>
    Strict,
}
