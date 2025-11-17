using System;
using System.ComponentModel;
using System.Threading;
using Frends.GermanSSN.Generate.Definitions;
using Frends.GermanSSN.Generate.Helpers;

namespace Frends.GermanSSN.Generate;

/// <summary>
/// Task Class for German Social Security Number generation.
/// </summary>
public static class GermanSSN
{
    /// <summary>
    /// Generates a German Social Security Number (Sozialversicherungsnummer) based on person data.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-GermanSSN-Generate)
    /// </summary>
    /// <param name="input">Essential parameters (person data).</param>
    /// <param name="options">Additional parameters (formatting, validation).</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, string PersonalIdentityNumber, string FormattedSSN, object Components, object Error }</returns>
    public static Result Execute(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            // Check for cancellation
            cancellationToken.ThrowIfCancellationRequested();

            // Validate input parameters
            InputValidator.Validate(input, options.ValidationMode);

            // Generate SSN
            var (ssn, formatted, components) = GermanSSNGenerator.Generate(input);

            // Return appropriate format based on options
            var personalIdentityNumber = options.Format == SSNFormat.FormattedWithSpaces ? formatted : ssn;

            return new Result
            {
                Success = true,
                PersonalIdentityNumber = personalIdentityNumber,
                FormattedSSN = formatted,
                Components = components,
                Error = null,
            };
        }
        catch (Exception ex)
        {
            return ErrorHandler.Handle(ex, options.ThrowErrorOnFailure, options.ErrorMessageOnFailure);
        }
    }
}
