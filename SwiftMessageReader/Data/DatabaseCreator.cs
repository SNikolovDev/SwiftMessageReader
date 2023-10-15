using System.Data.SQLite;

using SwiftMessageReader.Helpers;

namespace SwiftMessageReader.Data
{
    public class DatabaseCreator
    {
        private readonly IConfiguration configuration;
        private readonly string dbPath;
        private readonly string connectionString;

        public DatabaseCreator(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbPath = configuration.GetConnectionString("DBFileLocation");
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Create()
        {
            try
            {
                if (File.Exists(dbPath))
                {
                    return;
                }

                SQLiteConnection.CreateFile(dbPath);

                SwiftLogger.Info("Database created.");

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    var command = new SQLiteCommand(
                      "CREATE TABLE IF NOT EXISTS SwiftMessage (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "CreatedOn DATETIME NOT NULL, " +
                        "SendersBankIdentifierCode TEXT NOT NULL, " +
                        "MessageReferenceNumber TEXT NOT NULL, " +
                        "MessageAuthenticationCode TEXT NOT NULL, " +
                        "CheckValue TEXT NOT NULL" +
                        ")",
                        connection);

                    var secondCommand = new SQLiteCommand(
                      "CREATE TABLE IF NOT EXISTS TagsInformationTable (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "SwiftMessageId INTEGER NOT NULL, " +
                        "CreatedOn DATETIME NOT NULL, " +
                        "TransactionReferenceNumberTag TEXT NOT NULL, " +
                        "TransactionReferenceNumber TEXT NOT NULL, " +
                        "ReferenceAssinedByTheSenderTag TEXT NOT NULL, " +
                        "ReferenceAssinedByTheSender TEXT NOT NULL, " +
                        "MessageBodyTag TEXT NOT NULL, " +
                        "MessageBody TEXT NOT NULL, " +
                        "FOREIGN KEY (SwiftMessageId) REFERENCES SwiftMessage(Id)" +
                        ")",
                        connection);

                    command.ExecuteNonQuery();
                    secondCommand.ExecuteNonQuery();

                    SwiftLogger.Info(Messages.SuccessfulDatabaseCreation);
                    connection.Close();
                }
            }
            catch (Exception)
            {
                SwiftLogger.Error(Messages.DatabaseCreateError);
            }
        }
    }
}