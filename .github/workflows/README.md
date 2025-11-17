# CI/CD Workflows

This project uses GitHub Actions for continuous integration and deployment. The workflows are located in `.github/workflows/`.

## Workflows

### 1. CI Workflow (`ci.yml`)

**Trigger:** Runs on pull requests and pushes to `main` and `develop` branches.

**Purpose:** Validates code changes by building and testing the project.

**Steps:**
- Checks out the code
- Sets up .NET 8.0
- Restores dependencies
- Builds the solution in Release configuration
- Runs all tests with code coverage
- Uploads coverage reports to Codecov (optional)

### 2. Release Workflow (`release.yml`)

**Trigger:** Runs when a version tag is pushed (e.g., `v1.0.3`).

**Purpose:** Creates a GitHub release with NuGet package artifacts.

**Steps:**
- Checks out the code
- Extracts version from the tag
- Updates the project version
- Builds and tests the solution
- Creates a NuGet package
- Creates a GitHub Release with the package attached
- Optionally publishes to NuGet.org (requires `NUGET_API_KEY` secret)
- Optionally publishes to GitHub Packages

**To create a release:**
```bash
git tag v1.0.3
git push origin v1.0.3
```

### 3. Publish Package Workflow (`publish.yml`)

**Trigger:** Manual workflow dispatch from GitHub Actions UI.

**Purpose:** Manually build and upload package artifacts without creating a release.

**Steps:**
- Allows you to specify a version number
- Builds and tests the solution
- Creates a NuGet package
- Uploads the package as a workflow artifact (retained for 90 days)

**To use:**
1. Go to Actions → Publish Package
2. Click "Run workflow"
3. Enter the desired version number
4. Download the artifact from the workflow run

## Setup Instructions

### Required Secrets (Optional)

To enable full functionality, configure these secrets in your GitHub repository settings:

1. **`NUGET_API_KEY`** (optional)
   - Required only if you want to publish to NuGet.org automatically
   - Get your API key from https://www.nuget.org/account/apikeys
   - Go to Settings → Secrets and variables → Actions → New repository secret

2. **`CODECOV_TOKEN`** (optional)
   - Required only if you want coverage reports uploaded to Codecov
   - Get your token from https://codecov.io/
   - Go to Settings → Secrets and variables → Actions → New repository secret

### Permissions

The release workflow requires the following permissions (already configured in the workflow):
- `contents: write` - To create releases
- `packages: write` - To publish to GitHub Packages

## Release Artifacts

Release artifacts are available in two places:

1. **GitHub Releases**: Attached to each release as `.nupkg` files
2. **Workflow Artifacts**: Available from the Actions tab for manual workflow runs

## Version Management

The project version is defined in `Frends.GermanSSN.Generate/Frends.GermanSSN.Generate.csproj`:

```xml
<Version>1.0.2</Version>
```

When creating a release via tag, the workflow automatically updates this version to match the tag.

## Testing Coverage

Test coverage is collected using Coverlet and can be viewed:
- In the workflow logs
- On Codecov (if configured)

## Troubleshooting

**Issue:** Release workflow fails to create GitHub release
- **Solution:** Ensure the repository has the "Actions" permissions set correctly in Settings → Actions → General → Workflow permissions

**Issue:** NuGet publish fails
- **Solution:** Verify that `NUGET_API_KEY` secret is set correctly and the API key has push permissions

**Issue:** Tests fail in CI but pass locally
- **Solution:** Ensure all dependencies are properly restored and the code is committed
