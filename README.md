# SwiftMessageReader

SwiftMessageReader is a web API designed to read Swift MT799 messages from a text file and store the information in an SQLite database.

## Getting Started

### Prerequisites
- Download the application from GitHub.
- Open the project with Visual Studio.

### Database Setup
- The `DatabaseCreater` class will automatically check if the database file is present; if not, it will create one.
- Optionally, you can modify the localhost port number in the `launchSettings.json` file (this is not mandatory).
- For your convenience, `DBFileConnection` and `DefaultConnection` strings in the `appsettings.json` file are configured with relative paths. However, you can change them to point to local storage if needed.

## Sample SWIFT MT799 Message File

A sample file (`TestMessage.txt`) is included in the project's main directory. You can use this file to test the application.

## API Endpoints

The primary endpoint for the application is:

- `/api/message/insert`: Upload a file here to process Swift MT799 messages.

## Swagger

Swagger, a helpful tool, is integrated into the project for ease of use. It's pre-configured and ready to run.

## Contact

For questions, communication, or feedback, feel free to reach out to me via email at [snikolovdev@outlook.com].
