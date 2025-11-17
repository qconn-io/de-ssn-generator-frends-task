# Frends.GermanSSN.Generate

Frends Task for generating German Social Security Numbers (Sozialversicherungsnummer) with proper formatting, validation, and error handling.

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

## Installing

You can install the Task via Frends UI Task View or import the NuGet package to your Frends environment.

## Building

### Clone a copy of the repository

`git clone https://github.com/FrendsPlatform/Frends.GermanSSN.git`

### Build the project

`dotnet build`

### Run tests

Run the tests

`dotnet test`

### Create a NuGet package

`dotnet pack --configuration Release`

## Documentation

### Frends.GermanSSN.Generate

Generates a German Social Security Number (Sozialversicherungsnummer) based on person data.

#### Input

| Property | Type | Description | Example |
|----------|------|-------------|---------|
| FirstName | string | First name of the person | `Hans` |
| LastName | string | Last name (surname) of the person | `Mueller` |
| DateOfBirth | string | Date of birth in ISO 8601 format (yyyy-MM-dd) | `1985-03-15` |
| Gender | Gender | Gender of the person (Male/Female) | `Gender.Male` |

#### Options

| Property | Type | Description | Default | Example |
|----------|------|-------------|---------|---------|
| Format | SSNFormat | Format of the generated SSN | `Standard12Digit` | `Standard12Digit` or `FormattedWithSpaces` |
| ValidationMode | ValidationMode | Validation level for input parameters | `Basic` | `None`, `Basic`, or `Strict` |
| ThrowErrorOnFailure | bool | Whether to throw an error on failure | `true` | `true` |
| ErrorMessageOnFailure | string | Custom error message on failure | `""` | `"SSN generation failed"` |

#### Result

| Property | Type | Description | Example |
|----------|------|-------------|---------|
| Success | bool | Indicates if the task completed successfully | `true` |
| PersonalIdentityNumber | string | Generated German Social Security Number | `65150315M12345` |
| FormattedSSN | string | Formatted SSN with spaces for readability | `65 150315 M 123 4` |
| Components | SSNComponents | Breakdown of SSN components | See SSNComponents below |
| Error | Error | Error information if task failed | `null` |

#### SSNComponents

| Property | Type | Description | Example |
|----------|------|-------------|---------|
| AreaNumber | string | Area number (2 digits, 01-99) | `65` |
| BirthDate | string | Birth date in DDMMYY format (6 digits) | `150315` |
| SurnameCode | string | First letter of surname as code (01-26) | `13` |
| SerialNumber | string | Serial number (3 digits) | `123` |
| CheckDigit | string | Check digit (1 digit) | `4` |

### Usage Example

```csharp
var input = new Input
{
    FirstName = "Max",
    LastName = "Mustermann",
    DateOfBirth = "1990-01-15",
    Gender = Gender.Male
};

var options = new Options
{
    Format = SSNFormat.FormattedWithSpaces,
    ValidationMode = ValidationMode.Strict,
    ThrowErrorOnFailure = true
};

var result = GermanSSN.Execute(input, options, cancellationToken);

// Result.PersonalIdentityNumber: "99 150190 13 456 7"
// Result.FormattedSSN: "99 150190 13 456 7"
// Result.Success: true
```

## German SSN Format

The generated Sozialversicherungsnummer follows this structure:

**AA BBBBBB C DDD**

- **AA**: Area number (01-99) - Insurance institution region
- **BBBBBB**: Birth date in DDMMYY format
- **C**: First letter of surname encoded as number (A=01, B=02...Z=26)
- **DDD**: Serial number with check digit

### Features

- **Deterministic Generation**: Same person data always generates the same SSN
- **Validation Modes**:
  - `None`: No validation
  - `Basic`: Required fields and date format validation
  - `Strict`: Age limits, name format, and comprehensive validation
- **Flexible Formatting**: Output with or without spaces
- **Error Handling**: Configurable error throwing and custom error messages
- **Check Digit**: Uses modified Luhn algorithm for validation

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Third-party licenses

StyleCop.Analyzer version (unmodified version 1.1.118) used to analyze code uses Apache-2.0 license, full text and source code can be found at https://github.com/DotNetAnalyzers/StyleCopAnalyzers
