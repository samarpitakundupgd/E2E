# 4DAPI End-to-End Testing Suite

This repository contains an end-to-end testing suite for the 4DAPI and DataAPI services. The tests are written in C# using the xUnit framework and are designed to validate the functionality of the APIs, including case request processing, job status polling, and integration with external services like Azure Key Vault and Auth0.

---

## Table of Contents
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Running the Tests](#running-the-tests)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

---

## Features
- **Case Request Processing**: Validates the creation and processing of case requests via the 4DAPI.
- **Job Status Polling**: Ensures jobs are processed successfully and monitors their status.
- **Integration with Azure Key Vault**: Securely retrieves secrets for API endpoints and authentication.
- **Auth0 Token Management**: Handles token generation for API authentication.
- **Customizable Test Data**: Supports dynamic test data through JSON files.

---

## Prerequisites
- **.NET 9 SDK**: Ensure you have the .NET 9 SDK installed.
- **Azure Key Vault**: Set up an Azure Key Vault to store secrets required for the tests.
- **Auth0 Account**: Configure Auth0 for token generation.
- **Visual Studio**: Use Visual Studio for development and debugging.
- **xUnit**: The testing framework used for writing and running tests.

---

## Setup Instructions
1. **Clone the Repository**:


2. **Install Dependencies**:
Ensure all NuGet packages are restored. In Visual Studio, go to __Tools > NuGet Package Manager > Manage NuGet Packages for Solution__ and restore missing packages.

3. **Configure Azure Key Vault**:
- Add the required secrets to your Azure Key Vault:
  - `fourdpathapi-rooturl`
  - `dataapi-rooturl`
  - `auth0-token-endpoint`
  - `auth0-4dpath-public-api-clientid`
  - `auth0-4dpath-public-api-client-secret`
  - `auth0-4dpath-public-api-audience`
  - `auth0-4dpath-api-clientid`
  - `auth0-4dpath-api-client-secret`
  - `auth0-4dpath-api-audience`

4. **Update `appsettings.json`**:
- Ensure the `appsettings.json` file contains any additional configuration required for the tests.

5. **Prepare Test Data**:
- Place your test data in the `E2ETests\TestFiles\CaseRequest.json` file. This file should contain a valid `CaseRequest` object.

---

## Running the Tests
1. **Run All Tests**:
- Open the solution in Visual Studio.
- Go to __Test > Run All Tests__ or use the Test Explorer.

2. **Run Specific Tests**:
- Use the `[Fact]` attribute to identify individual tests.
- Run specific tests from the Test Explorer.

3. **Command Line**:
- Use the following command to run tests via the .NET CLI:
  ```bash
  dotnet test
  ```

---

## Project Structure
- **E2ETests**: Contains all test files and helpers.
- `FourDAPITests.cs`: Main test suite for 4DAPI and DataAPI.
- `Helpers`: Utility classes for token generation and case request handling.
- `Models`: Data models for `CaseRequest`, `Job`, and `JobSlide`.
- `TestFiles`: Contains JSON files for test data.
- **appsettings.json**: Configuration file for the project.

---

## Configuration
- **Azure Key Vault**: Secrets are retrieved dynamically using the `DefaultAzureCredential` class.
- **Auth0**: Tokens are generated using the `TokenService` helper.
- **Test Data**: Modify `CaseRequest.json` to customize test inputs.

---

## Contributing
1. Fork the repository.
2. Create a feature branch:

```
git checkout -b feature/your-feature
```

3. Commit your changes:
```
git commit -m "Add your feature"
```

4. Push to the branch:
```
git push origin feature/your-feature
```

5. Create a pull request detailing your changes.
6. Await code review and address any feedback.
7. Once approved, your changes will be merged into the main branch.

---

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---


## Notes
- Ensure your Azure Key Vault and Auth0 configurations are correct before running the tests.
- The `PollJobStatusAsync` method has a timeout of 20 minutes. Adjust the `maxRetries` and `delayInMilliseconds` values if needed.

