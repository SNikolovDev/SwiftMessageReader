using System.Data.SQLite;

using SwiftMessageReader.Exceptions;
using SwiftMessageReader.Helpers;

namespace SwiftMessageReader.Data
{
    public class DataBaseCreator
    {
        private readonly IConfiguration configuration;
        private readonly string dbPath;
        private readonly string connectionString;

        public DataBaseCreator(IConfiguration configuration)
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

                  SwiftLogger.Info("Executed Non Query.");

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                SwiftLogger.Error("An error when creating the database occured: " + ex);
                throw new DatabaseCreationException("There was a problem whene creating the database.");
            }
        }
    }
}