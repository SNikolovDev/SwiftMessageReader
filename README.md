# SwiftMessageReader
Web api which can read Swift MT799 messages from a text file and store the information into SQLite DB.
---

# Getting started
1. Download the application from github and run it with Visual Studio.
2. The DatabaseCreater class will automaticly check if database file is present, if not - one will be created.
3. If you want, you can change the localhost port number in launchSettings.json file (it's not mandatory).
4. For convenience DBFileConnection and DefaultConnection strings in appsettings.json file are configured with relative paths, but the can be changed with once to the local storage, if needed.

# Sample SWIFT MT799 Message File
---
Sample file (Test Message .txt) is included in the project's main directory. Use it to test the application.

# API Endpoints
---
The main endpoint for the app is:
- /api/message/insert - upload file here;

# Swagger
---
Swagger tool is implemented in the project for ease of use. It's configured to run trough it.

# Contact
---
For questions, communication or feedback, my e-mail is:
[snikolovdev@outlook.com]

