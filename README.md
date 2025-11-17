# Frends.GermanSSN

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

Custom Frends Task for generating German Social Security Numbers (Sozialversicherungsnummer).

## Repository Structure

This repository contains the Frends.GermanSSN.Generate task for generating valid German Social Security Numbers with proper formatting and validation.

## Getting Started

### Prerequisites

- .NET SDK 8.0 or later
- Visual Studio 2022, VS Code, or JetBrains Rider (recommended)

### Building

```bash
# Clone the repository
git clone https://github.com/FrendsPlatform/Frends.GermanSSN.git
cd Frends.GermanSSN

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test

# Create NuGet package
dotnet pack --configuration Release
```

## Usage

Install the task in your Frends environment via the Task View UI or by importing the NuGet package.

See [Frends.GermanSSN.Generate/README.md](Frends.GermanSSN.Generate/README.md) for detailed documentation.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
