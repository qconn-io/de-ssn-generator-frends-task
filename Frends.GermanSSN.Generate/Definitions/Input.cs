using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.GermanSSN.Generate.Definitions;

/// <summary>
/// Essential parameters for German SSN generation.
/// </summary>
public class Input
{
    /// <summary>
    /// First name of the person.
    /// </summary>
    /// <example>Hans</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("Max")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Last name (surname) of the person.
    /// </summary>
    /// <example>Mueller</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("Mustermann")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth in ISO 8601 format (yyyy-MM-dd) or DateTime string.
    /// </summary>
    /// <example>1985-03-15</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("1990-01-01")]
    public string DateOfBirth { get; set; } = string.Empty;

    /// <summary>
    /// Gender of the person.
    /// </summary>
    /// <example>Gender.Male</example>
    [DefaultValue(Gender.Male)]
    public Gender Gender { get; set; }
}
