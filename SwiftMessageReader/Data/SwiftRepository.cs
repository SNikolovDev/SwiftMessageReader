using System.Data.SQLite;

using SwiftMessageReader.Helpers;
using SwiftMessageReader.Models;

namespace SwiftMessageReader.Data
{
    public class SwiftRepository : ISwiftRepository
    {

        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public SwiftRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertIntoDatabase(MessageModel model)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = "INSERT INTO MyTable (Name, Age) VALUES (@Name, @Age)";

                // Define parameters for the values to be inserted
                //command.Parameters.AddWithValue("@Name", model.Name);
                //command.Parameters.AddWithValue("@Age", model.Age);

                //var insertCommant = InsertIntoTable(model, connection);
                //insertCommant.ExecuteNonQuery();
                command.ExecuteNonQuery();
                SwiftLogger.Info("Executed Non Query.");

                connection.Close();
            }
        }

        //private SQLiteCommand InsertIntoTable(MessageModel model, SQLiteConnection connection)
        //{
        //    using (SQLiteCommand command = new SQLiteCommand(connection))
        //    {
        //        // Define the SQL command to insert a new record into the 'Person' table
        //        command.CommandText = "INSERT INTO SecondTable (Name, Age) VALUES (@Name, @Age)";

        //        // Define parameters for the values to be inserted
        //        //command.Parameters.AddWithValue("@Name", model.Name);
        //        //command.Parameters.AddWithValue("@Age", model.Age);

        //        return command;
        //    }         
        //}
    }
}
