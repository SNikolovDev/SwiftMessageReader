# SwiftMessageReader

SwiftMessageReader is a web API designed to read Swift MT799 messages from a text file and store the information in an SQLite database.

## Getting Started

### Prerequisites
- Download the application from GitHub.
- Open the project with Visual Studio.

### Database Setup
- The `DatabaseCreater` class will automatically check if the database file is present; if not, it will create one. The file will be in "Data" folder in your projects directory on to the hard drive.
- Optionally, you can modify the localhost port number in the `launchSettings.json` file (this is not mandatory).
- For your convenience, `DBFileConnection` and `DefaultConnection` strings in the `appsettings.json` file are configured with relative paths. However, you can change them to point to local storage if needed.

## Sample SWIFT MT799 Message File

A sample file (`TestMessage.txt`) is included in the project's main directory. You can use this file to test the application.

## Accessing the Database

To access the database file, navigate to ProjectFolder/Data/ and locate SwiftDatabase.db on your hard drive.
To view and interact with the database, you can download DB Browser for SQLite from the official website: https://sqlitebrowser.org/dl/. No installation is required.
Please note that two tables will be created in the database. To access the table containing tags and their associated information, select the second table (TagsInformationTable) from the dropdown menu in the DB Browser interface.

## API Endpoints

The primary endpoint for the application is:

- `/api/message/insert`: Upload a file here to process Swift MT799 messages.

## Swagger

Swagger, a helpful tool, is integrated into the project for ease of use. It's pre-configured and ready to run.

## Contact

For questions, communication, or feedback, feel free to reach out to me via email at [snikolovdev@outlook.com].
