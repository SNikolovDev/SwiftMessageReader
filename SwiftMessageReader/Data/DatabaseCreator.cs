using System.Data.SQLite;

using SwiftMessageReader.Exceptions;
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

                    SQLiteCommand command = new SQLiteCommand(
                        "CREATE TABLE IF NOT EXISTS SwiftDatabase (" +
                            "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                            "CreatedOn DATETIME NOT NULL, " +
                            "SendersBankIdentifierCode TEXT NOT NULL, " +
                            "MessageReferenceNumber TEXT NOT NULL, " +
                            "TransactionReferenceNumber TEXT NOT NULL, " +
                            "ReferenceAssinedByTheSender TEXT NOT NULL, " +
                            "MessageBody TEXT NOT NULL, " +
                            "MessageAuthenticationCode TEXT NOT NULL, " +
                            "CheckValue TEXT NOT NULL" +
                            ")",
                        connection);

                    command.ExecuteNonQuery();

                  SwiftLogger.Info(NLogMessages.SuccessfulDatabaseCreation);

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                SwiftLogger.Error(NLogMessages.DatabaseCreateError + ex.Message);
                throw new DatabaseCreationException(ex.Message);
            }
        }
    }
}