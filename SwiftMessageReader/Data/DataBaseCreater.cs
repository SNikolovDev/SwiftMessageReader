using System.Data.SQLite;

using SwiftMessageReader.Helpers;

namespace SwiftMessageReader.Data
{
    public class DataBaseCreater
    {
        private readonly IConfiguration configuration;
        private readonly string dbPath;
        private readonly string connectionString;

        public DataBaseCreater(IConfiguration configuration)
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

                    SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS MyTable (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Age INTEGER)", connection);

                    command.ExecuteNonQuery();

                  SwiftLogger.Info("Executed Non Query.");

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                SwiftLogger.Error("An error when creating the database occured: " + ex);
            }
        }
    }
}
