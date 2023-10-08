using System.Data.SQLite;
using SwiftMessageReader.Data.Interfaces;
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

                command.CommandText = "INSERT INTO SwiftDatabase (" +
                    "CreatedOn, " +
                    "SendersBankIdentifierCode, " +
                    "MessageReferenceNumber, " +
                    "TransactionReferenceNumber, " +
                    "ReferenceAssinedByTheSender, " +
                    "MessageBody, " +
                    "MessageAuthenticationCode, " +
                    "CheckValue) " +
                    "VALUES (" +
                    "@CreatedOn, " +
                    "@SendersBankIdentifierCode, " +
                    "@MessageReferenceNumber, " +
                    "@TransactionReferenceNumber, " +
                    "@ReferenceAssinedByTheSender, " +
                    "@MessageBody, " +
                    "@MessageAuthenticationCode, " +
                    "@CheckValue)";

                // Define parameters for the values to be inserted
                command.Parameters.AddWithValue("@CreatedOn", model.CreatedOn);
                command.Parameters.AddWithValue("@SendersBankIdentifierCode" , model.SendersBankIdentifierCode);
                command.Parameters.AddWithValue("@MessageReferenceNumber", model.MessageReferenceNumber);
                command.Parameters.AddWithValue("@TransactionReferenceNumber", model.TransactionReferenceNumber);
                command.Parameters.AddWithValue("@ReferenceAssinedByTheSender", model.ReferenceAssinedByTheSender);
                command.Parameters.AddWithValue("@MessageBody", model.MessageBody);
                command.Parameters.AddWithValue("@MessageAuthenticationCode", model.MessageAuthenticationCode);
                command.Parameters.AddWithValue("@CheckValue", model.CheckValue);

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
