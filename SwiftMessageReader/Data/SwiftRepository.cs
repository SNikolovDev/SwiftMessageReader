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

                AddToParametersWithValues(model, command);
                command.ExecuteNonQuery();

                SwiftLogger.Info(NLogMessages.SuccessfulDataInsert);

                connection.Close();
            }
        }

        private static void AddToParametersWithValues(MessageModel model, SQLiteCommand command)
        {
            command.Parameters.AddWithValue("@CreatedOn", model.CreatedOn);
            command.Parameters.AddWithValue("@SendersBankIdentifierCode", model.SendersBankIdentifierCode);
            command.Parameters.AddWithValue("@MessageReferenceNumber", model.MessageReferenceNumber);
            command.Parameters.AddWithValue("@TransactionReferenceNumber", model.TransactionReferenceNumber);
            command.Parameters.AddWithValue("@ReferenceAssinedByTheSender", model.ReferenceAssinedByTheSender);
            command.Parameters.AddWithValue("@MessageBody", model.MessageBody);
            command.Parameters.AddWithValue("@MessageAuthenticationCode", model.MessageAuthenticationCode);
            command.Parameters.AddWithValue("@CheckValue", model.CheckValue);
        }
    }
}
